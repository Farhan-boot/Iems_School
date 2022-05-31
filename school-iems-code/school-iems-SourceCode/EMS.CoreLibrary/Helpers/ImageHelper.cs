using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Image = System.Drawing.Image;

namespace EMS.CoreLibrary.Helpers
{
    public class ImageHelper : IDisposable
    {
        public Image ResizedImageByFixedWidthHeight(Image image, int width, int height)
        {
            //get the new size based on the percentage change
            int resizedW = width;
            int resizedH = height;

            //get the height and width of the image
            int originalW = image.Width;
            int originalH = image.Height;

            //create a new Bitmap the size of the new image
            //{
            //    Bitmap bmp = new Bitmap(resizedW, resizedH, image.PixelFormat);
            //    bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            //    //create a new graphic from the Bitmap
            //    using (Graphics graphic = Graphics.FromImage(bmp))
            //    {
            //        graphic.InterpolationMode = InterpolationMode.Default;//HighQualityBicubic;
            //        //draw the newly resized image
            //        graphic.DrawImage(image, 0, 0, resizedW, resizedH);
            //        //dispose and free up the resources
            //        graphic.Dispose();
            //    }
            //    //return the image
            //    return (Image)bmp;
            //}

            System.Drawing.Image NewImage = image.GetThumbnailImage(resizedW, resizedH, null, IntPtr.Zero);

            image.Dispose();
            return NewImage;

        }

        public Image Resize(Image image, int width)
        {

            double bw = image.Width;
            double bh = image.Height;
            int height = (int)((bh / bw) * width);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return image;
        }
        public Image CompressAndSaveToPath(Image image,string mimeType, int compressionPercent, string physicalPath, string imageName)
        {
            ImageCodecInfo jpgEncoder;
            if (mimeType.ToLower() == "image/jpeg" || mimeType.ToLower() == "image/jpg")
            {
                jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            }
            else if (mimeType.ToLower() == "image/png")
            {
                jpgEncoder = GetEncoder(ImageFormat.Png);
            }
            else if (mimeType.ToLower() == "image/bmp")
            {
                jpgEncoder = GetEncoder(ImageFormat.Bmp);
            }
            else if (mimeType.ToLower() == "image/gif")
            {
                jpgEncoder = GetEncoder(ImageFormat.Gif);
            }
            else
            {
                image.Save(physicalPath + "/" + imageName);
                return image;
            }
            
            
            // Create an Encoder object based on the GUID 
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            // An EncoderParameters object has an array of EncoderParameter 
            // objects. In this case, there is only one 
            // EncoderParameter object in the array.
            var myEncoderParameters = new EncoderParameters(1);

            var myEncoderParameter = new EncoderParameter(myEncoder, compressionPercent);
            myEncoderParameters.Param[0] = myEncoderParameter;
            image.Save(physicalPath + "/" + imageName, jpgEncoder, myEncoderParameters);
            return image;
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public bool CreateThumb(string sourceVirtualPath, string newThumbVirtualPath, Size size)
        {
            bool success = false;
            try
            {

                string physicalPath = HttpContext.Current.Server.MapPath(sourceVirtualPath);
                string thumbPhysicalPath = HttpContext.Current.Server.MapPath(newThumbVirtualPath);

                if (File.Exists(physicalPath))
                {
                    FileInfo file = new System.IO.FileInfo(physicalPath);
                    string st = Path.GetDirectoryName(thumbPhysicalPath);
                    if (!Directory.Exists(st))
                        Directory.CreateDirectory(st);

                    using (System.Drawing.Image originalImage = System.Drawing.Image.FromFile(physicalPath))
                    {
                        using (System.Drawing.Image thumbImage = ResizeImageAspectRatio(originalImage, size.Width, size.Height, true))
                        {
                            success = CompressAndSaveImageToFile(thumbImage, thumbPhysicalPath,70);
                        }
                    }
                }
                return success;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //this has problem
        public Image ResizeImageAspectRatio(Stream imageStream, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            System.Drawing.Image newImage = null;
            using (System.Drawing.Image receivedlImage = System.Drawing.Image.FromStream(imageStream))
            {
                newImage = ResizeImageAspectRatio(receivedlImage, NewWidth, MaxHeight, OnlyResizeIfWider);
                //newImage.Save(phycalFilePath);
            }
            //receivedlImage.Dispose();
            return newImage;
        }
        //this has problem
        public Image ResizeImageAspectRatio(Image image, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            //System.Drawing.Image image = System.Drawing.Image.FromFile(OriginalFile);

            // Prevent using images internal thumbnail
            image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (image.Width <= NewWidth)
                {
                    NewWidth = image.Width;
                }
            }

            int NewHeight = image.Height * NewWidth / image.Width;
            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = image.Width * MaxHeight / image.Height;
                NewHeight = MaxHeight;
            }

            System.Drawing.Image NewImage = ResizedImageByFixedWidthHeight(image, NewWidth, NewHeight); //image.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            image.Dispose();
            return NewImage;

            // Save resized picture
            //NewImage.Save(NewFile);
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = null; ;
            try
            {
                ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
            catch
            {
                return null;

            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }
            }
        }

        public bool SaveImageToFile(Image originalImage, string phycialFilePath)
        {
            try
            {
                if (originalImage != null)
                {
                    originalImage.Save(phycialFilePath, ImageFormat.Jpeg);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                }
            }
        }
        public bool CompressAndSaveImageToFile(Image originalImage, string phycialFilePath, long quality = 60)
        {
            try
            {
                if (originalImage != null)
                {
                    EncoderParameters parameters = new EncoderParameters(1);
                    parameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                    originalImage.Save(phycialFilePath, GetEncoderInfo("image/jpeg"), parameters);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                }
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            foreach (ImageCodecInfo encoder in ImageCodecInfo.GetImageEncoders())
                if (encoder.MimeType == mimeType)
                    return encoder;
            throw new ArgumentOutOfRangeException(
                string.Format("'{0}' not supported", mimeType));
        }
        /// <summary>
        /// Find the right codec
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetEncoderInfoByFileExt(string extension)
        {
            extension = extension.ToUpperInvariant();
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FilenameExtension.Contains(extension))
                {
                    return codec;
                }
            }
            return codecs[1];
        }
      
        public void Dispose()
        {

        }

        //private void WriteNewDescriptionInImage(string Filename, string NewDescription)
        //{
        //    Image Pic;
        //    PropertyItem[] PropertyItems;
        //    byte[] bDescription = new Byte[NewDescription.Length];
        //    int i;
        //    string FilenameTemp;
        //    Encoder Enc = Encoder.Transformation;
        //    EncoderParameters EncParms = new EncoderParameters(1);
        //    EncoderParameter EncParm;
        //    ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");

        //    // copy description into byte array
        //    for (i = 0; i < NewDescription.Length; i++) bDescription[i] = (byte)NewDescription[i];

        //    // load the image to change
        //    Pic = Image.FromFile(Filename);

        //    // put the new description into the right property item
        //    PropertyItems = Pic.PropertyItems;
        //    PropertyItems[0].Id = 0x010e; // 0x010e as specified in EXIF standard
        //    PropertyItems[0].Type = 2;
        //    PropertyItems[0].Len = NewDescription.Length;
        //    PropertyItems[0].Value = bDescription;
        //    Pic.SetPropertyItem(PropertyItems[0]);

        //    // we cannot store in the same image, so use a temporary image instead
        //    FilenameTemp = Filename + ".temp";

        //    // for lossless rewriting must rotate the image by 90 degrees!
        //    EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate90);
        //    EncParms.Param[0] = EncParm;

        //    // now write the rotated image with new description
        //    Pic.Save(FilenameTemp, CodecInfo, EncParms);

        //    // for computers with low memory and large pictures: release memory now
        //    Pic.Dispose();
        //    Pic = null;
        //    GC.Collect();

        //    // delete the original file, will be replaced later
        //    System.IO.File.Delete(Filename);

        //    // now must rotate back the written picture
        //    Pic = Image.FromFile(FilenameTemp);
        //    EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate270);
        //    EncParms.Param[0] = EncParm;
        //    Pic.Save(Filename, CodecInfo, EncParms);

        //    // release memory now
        //    Pic.Dispose();
        //    Pic = null;
        //    GC.Collect();

        //    // delete the temporary picture
        //    System.IO.File.Delete(FilenameTemp);
        //}
    }
}
