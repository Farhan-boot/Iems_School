namespace HIMS.UMS.Reports.AdmissionReport.ReportFiles
{
    partial class rptApplicantCount
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptApplicantCount));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.labelsGroupFooter = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeader = new Telerik.Reporting.GroupHeaderSection();
            this.typeGroupFooter = new Telerik.Reporting.GroupFooterSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.typeGroupHeader = new Telerik.Reporting.GroupHeaderSection();
            this.typeDataTextBox = new Telerik.Reporting.TextBox();
            this.typeCaptionTextBox = new Telerik.Reporting.TextBox();
            this.titleCaptionTextBox = new Telerik.Reporting.TextBox();
            this.totalSoldCaptionTextBox = new Telerik.Reporting.TextBox();
            this.totalSubmittedCaptionTextBox = new Telerik.Reporting.TextBox();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            this.examSlotCaptionTextBox = new Telerik.Reporting.TextBox();
            this.printedByCaptionTextBox = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.examSlotDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.printedByDataTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.titleDataTextBox = new Telerik.Reporting.TextBox();
            this.totalSoldDataTextBox = new Telerik.Reporting.TextBox();
            this.totalSubmittedDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // labelsGroupFooter
            // 
            this.labelsGroupFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699D);
            this.labelsGroupFooter.Name = "labelsGroupFooter";
            this.labelsGroupFooter.Style.Visible = false;
            // 
            // labelsGroupHeader
            // 
            this.labelsGroupHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.labelsGroupHeader.Name = "labelsGroupHeader";
            this.labelsGroupHeader.PrintOnEveryPage = true;
            // 
            // typeGroupFooter
            // 
            this.typeGroupFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003943145275116D);
            this.typeGroupFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox5});
            this.typeGroupFooter.Name = "typeGroupFooter";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4792461395263672D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox4.StyleName = "Data";
            this.textBox4.Value = "= Sum(Fields.TotalSold)";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7792460918426514D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.StyleName = "Data";
            this.textBox5.Value = "= Sum(Fields.TotalSubmitted)";
            // 
            // typeGroupHeader
            // 
            this.typeGroupHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.55634850263595581D);
            this.typeGroupHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.typeDataTextBox,
            this.typeCaptionTextBox,
            this.titleCaptionTextBox,
            this.totalSoldCaptionTextBox,
            this.totalSubmittedCaptionTextBox});
            this.typeGroupHeader.Name = "typeGroupHeader";
            // 
            // typeDataTextBox
            // 
            this.typeDataTextBox.CanGrow = true;
            this.typeDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.typeDataTextBox.Name = "typeDataTextBox";
            this.typeDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.2791671752929688D), Telerik.Reporting.Drawing.Unit.Inch(0.35623028874397278D));
            this.typeDataTextBox.Style.Font.Bold = true;
            this.typeDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.typeDataTextBox.Style.Font.Underline = true;
            this.typeDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.typeDataTextBox.StyleName = "Data";
            this.typeDataTextBox.Value = "=Fields.Type";
            // 
            // typeCaptionTextBox
            // 
            this.typeCaptionTextBox.CanGrow = true;
            this.typeCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.35634851455688477D));
            this.typeCaptionTextBox.Name = "typeCaptionTextBox";
            this.typeCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40694776177406311D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.typeCaptionTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.typeCaptionTextBox.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.typeCaptionTextBox.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.typeCaptionTextBox.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.typeCaptionTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.typeCaptionTextBox.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.typeCaptionTextBox.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.typeCaptionTextBox.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.typeCaptionTextBox.Style.Font.Bold = true;
            this.typeCaptionTextBox.StyleName = "Caption";
            this.typeCaptionTextBox.Value = "SL #";
            // 
            // titleCaptionTextBox
            // 
            this.titleCaptionTextBox.CanGrow = true;
            this.titleCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.207026481628418D), Telerik.Reporting.Drawing.Unit.Inch(0.35634851455688477D));
            this.titleCaptionTextBox.Name = "titleCaptionTextBox";
            this.titleCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2721401453018189D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.titleCaptionTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.titleCaptionTextBox.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.titleCaptionTextBox.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.titleCaptionTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.titleCaptionTextBox.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.titleCaptionTextBox.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.titleCaptionTextBox.Style.Font.Bold = true;
            this.titleCaptionTextBox.StyleName = "Caption";
            this.titleCaptionTextBox.Value = "Title";
            // 
            // totalSoldCaptionTextBox
            // 
            this.totalSoldCaptionTextBox.CanGrow = true;
            this.totalSoldCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4792461395263672D), Telerik.Reporting.Drawing.Unit.Inch(0.35634851455688477D));
            this.totalSoldCaptionTextBox.Name = "totalSoldCaptionTextBox";
            this.totalSoldCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.totalSoldCaptionTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSoldCaptionTextBox.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSoldCaptionTextBox.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSoldCaptionTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSoldCaptionTextBox.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSoldCaptionTextBox.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSoldCaptionTextBox.Style.Font.Bold = true;
            this.totalSoldCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.totalSoldCaptionTextBox.StyleName = "Caption";
            this.totalSoldCaptionTextBox.Value = "Total Sold";
            // 
            // totalSubmittedCaptionTextBox
            // 
            this.totalSubmittedCaptionTextBox.CanGrow = true;
            this.totalSubmittedCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7792460918426514D), Telerik.Reporting.Drawing.Unit.Inch(0.35634851455688477D));
            this.totalSubmittedCaptionTextBox.Name = "totalSubmittedCaptionTextBox";
            this.totalSubmittedCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.totalSubmittedCaptionTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSubmittedCaptionTextBox.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSubmittedCaptionTextBox.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSubmittedCaptionTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSubmittedCaptionTextBox.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSubmittedCaptionTextBox.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSubmittedCaptionTextBox.Style.Font.Bold = true;
            this.totalSubmittedCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.totalSubmittedCaptionTextBox.StyleName = "Caption";
            this.totalSubmittedCaptionTextBox.Value = "Total Submitted";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(HIMS.UMS.Reports.AdmissionReport.Source.dsProgramsTotalFormSaleCount);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // examSlotCaptionTextBox
            // 
            this.examSlotCaptionTextBox.CanGrow = true;
            this.examSlotCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.97307425737380981D));
            this.examSlotCaptionTextBox.Name = "examSlotCaptionTextBox";
            this.examSlotCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.examSlotCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.examSlotCaptionTextBox.StyleName = "Caption";
            this.examSlotCaptionTextBox.Value = "Admission Exam :";
            // 
            // printedByCaptionTextBox
            // 
            this.printedByCaptionTextBox.CanGrow = true;
            this.printedByCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5999999046325684D), Telerik.Reporting.Drawing.Unit.Inch(0.0041557946242392063D));
            this.printedByCaptionTextBox.Name = "printedByCaptionTextBox";
            this.printedByCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.78385418653488159D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.printedByCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.printedByCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.printedByCaptionTextBox.StyleName = "Caption";
            this.printedByCaptionTextBox.Value = "Printed By :";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.4000000953674316D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.reportNameTextBox,
            this.textBox8,
            this.textBox1,
            this.examSlotCaptionTextBox,
            this.examSlotDataTextBox,
            this.textBox29,
            this.textBox25,
            this.textBox7,
            this.textBox6,
            this.textBox2,
            this.textBox3,
            this.textBox11,
            this.textBox12});
            this.pageHeader.Name = "pageHeader";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.099999986588954926D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.pictureBox1.MimeType = "image/bmp";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.91410732269287109D), Telerik.Reporting.Drawing.Unit.Inch(0.86476778984069824D));
            this.pictureBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.pictureBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.27916666865348816D));
            this.reportNameTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16D);
            this.reportNameTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = "American International University - Bangladesh";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(0.47924533486366272D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Value = "\"Where Leaders are Created\"";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.0999997854232788D), Telerik.Reporting.Drawing.Unit.Inch(0.67932415008544922D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.2936713695526123D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.textBox1.Style.Font.Underline = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "Total Form Sale & Submitted";
            // 
            // examSlotDataTextBox
            // 
            this.examSlotDataTextBox.CanGrow = true;
            this.examSlotDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3000791072845459D), Telerik.Reporting.Drawing.Unit.Inch(0.97719067335128784D));
            this.examSlotDataTextBox.Name = "examSlotDataTextBox";
            this.examSlotDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.499921441078186D), Telerik.Reporting.Drawing.Unit.Inch(0.19588363170623779D));
            this.examSlotDataTextBox.StyleName = "Data";
            this.examSlotDataTextBox.Value = "=Fields.ExamSlot";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.7145838737487793D), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D), Telerik.Reporting.Drawing.Unit.Inch(0.19280052185058594D));
            this.textBox29.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox29.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox29.Style.Font.Name = "Tahoma";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "of";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.8708338737487793D), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.24375025928020477D), Telerik.Reporting.Drawing.Unit.Inch(0.19280052185058594D));
            this.textBox25.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox25.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox25.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.StyleName = "PageInfo";
            this.textBox25.Value = "= PageCount";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.7979167103767395D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40000024437904358D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox7.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Date:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40000024437904358D), Telerik.Reporting.Drawing.Unit.Inch(0.19280052185058594D));
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox6.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Page:";
            // 
            // textBox2
            // 
            this.textBox2.Format = "{0:d}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5062503814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.7979167103767395D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60833358764648438D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox2.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox2.StyleName = "PageInfo";
            this.textBox2.Value = "=NOW()";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5062503814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.2083333283662796D), Telerik.Reporting.Drawing.Unit.Inch(0.19280052185058594D));
            this.textBox3.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.StyleName = "PageInfo";
            this.textBox3.Value = "=PageNumber";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = true;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3000791072845459D), Telerik.Reporting.Drawing.Unit.Inch(1.1731530427932739D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.499921441078186D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox11.StyleName = "Data";
            this.textBox11.Value = "= Fields.ExamDate";
            // 
            // textBox12
            // 
            this.textBox12.CanGrow = true;
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(1.1731530427932739D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.StyleName = "Caption";
            this.textBox12.Value = "Exam Date :";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20415580272674561D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.printedByCaptionTextBox,
            this.printedByDataTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // printedByDataTextBox
            // 
            this.printedByDataTextBox.CanGrow = true;
            this.printedByDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3839325904846191D), Telerik.Reporting.Drawing.Unit.Inch(0.0041557946242392063D));
            this.printedByDataTextBox.Name = "printedByDataTextBox";
            this.printedByDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8837051391601563D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.printedByDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.printedByDataTextBox.StyleName = "Data";
            this.printedByDataTextBox.Value = "=Fields.PrintedBy";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699D);
            this.reportHeader.Name = "reportHeader";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003943145275116D);
            this.reportFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox10});
            this.reportFooter.Name = "reportFooter";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97916728258132935D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Underline = false;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.StyleName = "Data";
            this.textBox9.Value = "= Sum(Fields.TotalSold)";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0999999046325684D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.97916728258132935D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Underline = false;
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.StyleName = "Data";
            this.textBox10.Value = "= Sum(Fields.TotalSubmitted)";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleDataTextBox,
            this.totalSoldDataTextBox,
            this.totalSubmittedDataTextBox,
            this.textBox27});
            this.detail.Name = "detail";
            // 
            // titleDataTextBox
            // 
            this.titleDataTextBox.CanGrow = true;
            this.titleDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.207026481628418D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.titleDataTextBox.Name = "titleDataTextBox";
            this.titleDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2721401453018189D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.titleDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.titleDataTextBox.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.titleDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.titleDataTextBox.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.titleDataTextBox.StyleName = "Data";
            this.titleDataTextBox.Value = "=Fields.Title";
            // 
            // totalSoldDataTextBox
            // 
            this.totalSoldDataTextBox.CanGrow = true;
            this.totalSoldDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4792461395263672D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.totalSoldDataTextBox.Name = "totalSoldDataTextBox";
            this.totalSoldDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.totalSoldDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSoldDataTextBox.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSoldDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSoldDataTextBox.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSoldDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.totalSoldDataTextBox.StyleName = "Data";
            this.totalSoldDataTextBox.Value = "=Fields.TotalSold";
            // 
            // totalSubmittedDataTextBox
            // 
            this.totalSubmittedDataTextBox.CanGrow = true;
            this.totalSubmittedDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7792460918426514D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.totalSubmittedDataTextBox.Name = "totalSubmittedDataTextBox";
            this.totalSubmittedDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999210357666016D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.totalSubmittedDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSubmittedDataTextBox.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.totalSubmittedDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSubmittedDataTextBox.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.totalSubmittedDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.totalSubmittedDataTextBox.StyleName = "Data";
            this.totalSubmittedDataTextBox.Value = "=Fields.TotalSubmitted";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40694770216941833D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox27.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.textBox27.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.textBox27.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0.30000001192092896D);
            this.textBox27.Style.Font.Name = "Tahoma";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "=RowNumber()";
            // 
            // rptApplicantCount
            // 
            this.DataSource = this.objectDataSource1;
            group1.GroupFooter = this.labelsGroupFooter;
            group1.GroupHeader = this.labelsGroupHeader;
            group1.Name = "labelsGroup";
            group2.GroupFooter = this.typeGroupFooter;
            group2.GroupHeader = this.typeGroupHeader;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Type"));
            group2.Name = "typeGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeader,
            this.labelsGroupFooter,
            this.typeGroupHeader,
            this.typeGroupFooter,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "rptApplicantCount";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.2676773071289062D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource objectDataSource1;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeader;
        private Telerik.Reporting.TextBox typeCaptionTextBox;
        private Telerik.Reporting.TextBox examSlotCaptionTextBox;
        private Telerik.Reporting.TextBox printedByCaptionTextBox;
        private Telerik.Reporting.TextBox titleCaptionTextBox;
        private Telerik.Reporting.TextBox totalSoldCaptionTextBox;
        private Telerik.Reporting.TextBox totalSubmittedCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooter;
        private Telerik.Reporting.Group labelsGroup;
        private Telerik.Reporting.GroupHeaderSection typeGroupHeader;
        private Telerik.Reporting.TextBox typeDataTextBox;
        private Telerik.Reporting.GroupFooterSection typeGroupFooter;
        private Telerik.Reporting.Group typeGroup;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox examSlotDataTextBox;
        private Telerik.Reporting.TextBox printedByDataTextBox;
        private Telerik.Reporting.TextBox titleDataTextBox;
        private Telerik.Reporting.TextBox totalSoldDataTextBox;
        private Telerik.Reporting.TextBox totalSubmittedDataTextBox;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;

    }
}