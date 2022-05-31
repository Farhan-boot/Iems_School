namespace EMS.Reporting.Library.ReportFiles
{
    partial class rptBarcodeLabel
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Barcodes.CodabarEncoder codabarEncoder1 = new Telerik.Reporting.Barcodes.CodabarEncoder();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.detail = new Telerik.Reporting.DetailSection();
            this.barcodeBook = new Telerik.Reporting.Barcode();
            this.textBoxLibraryName = new Telerik.Reporting.TextBox();
            this.textBoxTitle = new Telerik.Reporting.TextBox();
            this.objectDataSourceBook = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.barcodeBook,
            this.textBoxLibraryName,
            this.textBoxTitle});
            this.detail.Name = "detail";
            // 
            // barcodeBook
            // 
            this.barcodeBook.Anchoring = Telerik.Reporting.AnchoringStyles.None;
            this.barcodeBook.BarAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.barcodeBook.Encoder = codabarEncoder1;
            this.barcodeBook.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Inch(0.33000001311302185D));
            this.barcodeBook.Module = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.barcodeBook.Name = "barcodeBook";
            this.barcodeBook.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(0.41999998688697815D));
            this.barcodeBook.Stretch = true;
            this.barcodeBook.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.barcodeBook.Style.Font.Name = "OCR A Std";
            this.barcodeBook.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.barcodeBook.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.barcodeBook.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.barcodeBook.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.barcodeBook.Value = "= Fields.Barcode";
            // 
            // textBoxLibraryName
            // 
            this.textBoxLibraryName.CanGrow = false;
            this.textBoxLibraryName.CanShrink = true;
            this.textBoxLibraryName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D), Telerik.Reporting.Drawing.Unit.Inch(-1.2417634032146907E-08D));
            this.textBoxLibraryName.Name = "textBoxLibraryName";
            this.textBoxLibraryName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBoxLibraryName.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBoxLibraryName.Style.Font.Bold = false;
            this.textBoxLibraryName.Style.Font.Name = "Arial Black";
            this.textBoxLibraryName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBoxLibraryName.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBoxLibraryName.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBoxLibraryName.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBoxLibraryName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBoxLibraryName.Value = "= Fields.LibraryName";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.CanGrow = false;
            this.textBoxTitle.CanShrink = true;
            this.textBoxTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D), Telerik.Reporting.Drawing.Unit.Inch(0.15007869899272919D));
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(0.14992134273052216D));
            this.textBoxTitle.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBoxTitle.Style.Font.Bold = true;
            this.textBoxTitle.Style.Font.Name = "Arial Narrow";
            this.textBoxTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBoxTitle.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBoxTitle.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBoxTitle.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBoxTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBoxTitle.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBoxTitle.TextWrap = false;
            this.textBoxTitle.Value = "= Fields.Title";
            // 
            // objectDataSourceBook
            // 
            this.objectDataSourceBook.DataSource = typeof(EMS.Reporting.Library.Source.dsBook);
            this.objectDataSourceBook.Name = "objectDataSourceBook";
            // 
            // rptBarcodeLabel
            // 
            this.DataSource = this.objectDataSourceBook;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "rptBarcodeLabel";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612D), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612D), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612D), Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Inch(0D);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(2.7999999523162842D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.ObjectDataSource objectDataSourceBook;
        private Telerik.Reporting.TextBox textBoxLibraryName;
        private Telerik.Reporting.TextBox textBoxTitle;
        private Telerik.Reporting.Barcode barcodeBook;
    }
}