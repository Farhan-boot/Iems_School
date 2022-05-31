namespace EMS.Reporting.Admission.ReportFiles
{
    partial class rptAppliedApplicant
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptAppliedApplicant));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.applicantIdGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.applicantIdGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.quotaIdGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.quotaIdGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.formSerialCaptionTextBox = new Telerik.Reporting.TextBox();
            this.formSerialDataTextBox = new Telerik.Reporting.TextBox();
            this.addressCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentDistrictCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentDistrictDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentCareOfCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentCareOfDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentPoliceStationCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentPoliceStationDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentPostCodeCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPermanentPostCodeDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentCareOfCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentCareOfDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentDistrictCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentDistrictDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentPoliceStationCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentPoliceStationDataTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentPostCodeCaptionTextBox = new Telerik.Reporting.TextBox();
            this.addressPresentPostCodeDataTextBox = new Telerik.Reporting.TextBox();
            this.emailCaptionTextBox = new Telerik.Reporting.TextBox();
            this.emailDataTextBox = new Telerik.Reporting.TextBox();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Data Source=192.168.200.2;Initial Catalog=ems;Persist Security Info=True;User ID=" +
    "sa;Password=M@$tdbT3st;MultipleActiveResultSets=True;Application Name=EntityFram" +
    "ework";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.ProviderName = "System.Data.SqlClient";
            this.sqlDataSource1.SelectCommand = resources.GetString("sqlDataSource1.SelectCommand");
            // 
            // applicantIdGroupHeaderSection
            // 
            this.applicantIdGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            this.applicantIdGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2});
            this.applicantIdGroupHeaderSection.Name = "applicantIdGroupHeaderSection";
            // 
            // applicantIdGroupFooterSection
            // 
            this.applicantIdGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.applicantIdGroupFooterSection.Name = "applicantIdGroupFooterSection";
            // 
            // quotaIdGroupHeaderSection
            // 
            this.quotaIdGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            this.quotaIdGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox4});
            this.quotaIdGroupHeaderSection.Name = "quotaIdGroupHeaderSection";
            // 
            // quotaIdGroupFooterSection
            // 
            this.quotaIdGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.quotaIdGroupFooterSection.Name = "quotaIdGroupFooterSection";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Applicant Id:";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2416666746139526D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9427165985107422D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "= Fields.ApplicantId";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "Quota Id:";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2416666746139526D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9427165985107422D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox4.StyleName = "Data";
            this.textBox4.Value = "= Fields.QuotaId";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.reportNameTextBox});
            this.pageHeader.Name = "pageHeader";
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1843833923339844D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = "AppliedApplicantReport";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0817749500274658D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1234416961669922D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0817749500274658D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.2290682792663574D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.formSerialCaptionTextBox,
            this.formSerialDataTextBox,
            this.addressCaptionTextBox,
            this.addressDataTextBox,
            this.addressPermanentCaptionTextBox,
            this.addressPermanentDataTextBox,
            this.addressPermanentDistrictCaptionTextBox,
            this.addressPermanentDistrictDataTextBox,
            this.addressPermanentCareOfCaptionTextBox,
            this.addressPermanentCareOfDataTextBox,
            this.addressPermanentPoliceStationCaptionTextBox,
            this.addressPermanentPoliceStationDataTextBox,
            this.addressPermanentPostCodeCaptionTextBox,
            this.addressPermanentPostCodeDataTextBox,
            this.addressPresentCaptionTextBox,
            this.addressPresentDataTextBox,
            this.addressPresentCareOfCaptionTextBox,
            this.addressPresentCareOfDataTextBox,
            this.addressPresentDistrictCaptionTextBox,
            this.addressPresentDistrictDataTextBox,
            this.addressPresentPoliceStationCaptionTextBox,
            this.addressPresentPoliceStationDataTextBox,
            this.addressPresentPostCodeCaptionTextBox,
            this.addressPresentPostCodeDataTextBox,
            this.emailCaptionTextBox,
            this.emailDataTextBox});
            this.reportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2260499000549316D), Telerik.Reporting.Drawing.Unit.Inch(0.787401556968689D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "AppliedApplicantReport";
            // 
            // formSerialCaptionTextBox
            // 
            this.formSerialCaptionTextBox.CanGrow = true;
            this.formSerialCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.formSerialCaptionTextBox.Name = "formSerialCaptionTextBox";
            this.formSerialCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.formSerialCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.formSerialCaptionTextBox.StyleName = "Caption";
            this.formSerialCaptionTextBox.Value = "Form Serial:";
            // 
            // formSerialDataTextBox
            // 
            this.formSerialDataTextBox.CanGrow = true;
            this.formSerialDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.25949549674987793D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.formSerialDataTextBox.Name = "formSerialDataTextBox";
            this.formSerialDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.formSerialDataTextBox.StyleName = "Data";
            this.formSerialDataTextBox.Value = "= Fields.FormSerial";
            // 
            // addressCaptionTextBox
            // 
            this.addressCaptionTextBox.CanGrow = true;
            this.addressCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.49815768003463745D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressCaptionTextBox.Name = "addressCaptionTextBox";
            this.addressCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressCaptionTextBox.StyleName = "Caption";
            this.addressCaptionTextBox.Value = "Address:";
            // 
            // addressDataTextBox
            // 
            this.addressDataTextBox.CanGrow = true;
            this.addressDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.736819863319397D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressDataTextBox.Name = "addressDataTextBox";
            this.addressDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressDataTextBox.StyleName = "Data";
            this.addressDataTextBox.Value = "= Fields.Address";
            // 
            // addressPermanentCaptionTextBox
            // 
            this.addressPermanentCaptionTextBox.CanGrow = true;
            this.addressPermanentCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.97548204660415649D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentCaptionTextBox.Name = "addressPermanentCaptionTextBox";
            this.addressPermanentCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPermanentCaptionTextBox.StyleName = "Caption";
            this.addressPermanentCaptionTextBox.Value = "Address Permanent:";
            // 
            // addressPermanentDataTextBox
            // 
            this.addressPermanentDataTextBox.CanGrow = true;
            this.addressPermanentDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.214144229888916D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentDataTextBox.Name = "addressPermanentDataTextBox";
            this.addressPermanentDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentDataTextBox.StyleName = "Data";
            this.addressPermanentDataTextBox.Value = "= Fields.AddressPermanent";
            // 
            // addressPermanentDistrictCaptionTextBox
            // 
            this.addressPermanentDistrictCaptionTextBox.CanGrow = true;
            this.addressPermanentDistrictCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4528063535690308D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentDistrictCaptionTextBox.Name = "addressPermanentDistrictCaptionTextBox";
            this.addressPermanentDistrictCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentDistrictCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPermanentDistrictCaptionTextBox.StyleName = "Caption";
            this.addressPermanentDistrictCaptionTextBox.Value = "Address Permanent District:";
            // 
            // addressPermanentDistrictDataTextBox
            // 
            this.addressPermanentDistrictDataTextBox.CanGrow = true;
            this.addressPermanentDistrictDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6914685964584351D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentDistrictDataTextBox.Name = "addressPermanentDistrictDataTextBox";
            this.addressPermanentDistrictDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentDistrictDataTextBox.StyleName = "Data";
            this.addressPermanentDistrictDataTextBox.Value = "= Fields.AddressPermanentDistrict";
            // 
            // addressPermanentCareOfCaptionTextBox
            // 
            this.addressPermanentCareOfCaptionTextBox.CanGrow = true;
            this.addressPermanentCareOfCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9301307201385498D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentCareOfCaptionTextBox.Name = "addressPermanentCareOfCaptionTextBox";
            this.addressPermanentCareOfCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentCareOfCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPermanentCareOfCaptionTextBox.StyleName = "Caption";
            this.addressPermanentCareOfCaptionTextBox.Value = "Address Permanent Care Of:";
            // 
            // addressPermanentCareOfDataTextBox
            // 
            this.addressPermanentCareOfDataTextBox.CanGrow = true;
            this.addressPermanentCareOfDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.1687929630279541D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentCareOfDataTextBox.Name = "addressPermanentCareOfDataTextBox";
            this.addressPermanentCareOfDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentCareOfDataTextBox.StyleName = "Data";
            this.addressPermanentCareOfDataTextBox.Value = "= Fields.AddressPermanentCareOf";
            // 
            // addressPermanentPoliceStationCaptionTextBox
            // 
            this.addressPermanentPoliceStationCaptionTextBox.CanGrow = true;
            this.addressPermanentPoliceStationCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.4074552059173584D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentPoliceStationCaptionTextBox.Name = "addressPermanentPoliceStationCaptionTextBox";
            this.addressPermanentPoliceStationCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentPoliceStationCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPermanentPoliceStationCaptionTextBox.StyleName = "Caption";
            this.addressPermanentPoliceStationCaptionTextBox.Value = "Address Permanent Police Station:";
            // 
            // addressPermanentPoliceStationDataTextBox
            // 
            this.addressPermanentPoliceStationDataTextBox.CanGrow = true;
            this.addressPermanentPoliceStationDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.6461172103881836D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentPoliceStationDataTextBox.Name = "addressPermanentPoliceStationDataTextBox";
            this.addressPermanentPoliceStationDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentPoliceStationDataTextBox.StyleName = "Data";
            this.addressPermanentPoliceStationDataTextBox.Value = "= Fields.AddressPermanentPoliceStation";
            // 
            // addressPermanentPostCodeCaptionTextBox
            // 
            this.addressPermanentPostCodeCaptionTextBox.CanGrow = true;
            this.addressPermanentPostCodeCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.8847794532775879D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentPostCodeCaptionTextBox.Name = "addressPermanentPostCodeCaptionTextBox";
            this.addressPermanentPostCodeCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentPostCodeCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPermanentPostCodeCaptionTextBox.StyleName = "Caption";
            this.addressPermanentPostCodeCaptionTextBox.Value = "Address Permanent Post Code:";
            // 
            // addressPermanentPostCodeDataTextBox
            // 
            this.addressPermanentPostCodeDataTextBox.CanGrow = true;
            this.addressPermanentPostCodeDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1234416961669922D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPermanentPostCodeDataTextBox.Name = "addressPermanentPostCodeDataTextBox";
            this.addressPermanentPostCodeDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPermanentPostCodeDataTextBox.StyleName = "Data";
            this.addressPermanentPostCodeDataTextBox.Value = "= Fields.AddressPermanentPostCode";
            // 
            // addressPresentCaptionTextBox
            // 
            this.addressPresentCaptionTextBox.CanGrow = true;
            this.addressPresentCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3621037006378174D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentCaptionTextBox.Name = "addressPresentCaptionTextBox";
            this.addressPresentCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPresentCaptionTextBox.StyleName = "Caption";
            this.addressPresentCaptionTextBox.Value = "Address Present:";
            // 
            // addressPresentDataTextBox
            // 
            this.addressPresentDataTextBox.CanGrow = true;
            this.addressPresentDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.6007659435272217D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentDataTextBox.Name = "addressPresentDataTextBox";
            this.addressPresentDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentDataTextBox.StyleName = "Data";
            this.addressPresentDataTextBox.Value = "= Fields.AddressPresent";
            // 
            // addressPresentCareOfCaptionTextBox
            // 
            this.addressPresentCareOfCaptionTextBox.CanGrow = true;
            this.addressPresentCareOfCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.839428186416626D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentCareOfCaptionTextBox.Name = "addressPresentCareOfCaptionTextBox";
            this.addressPresentCareOfCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentCareOfCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPresentCareOfCaptionTextBox.StyleName = "Caption";
            this.addressPresentCareOfCaptionTextBox.Value = "Address Present Care Of:";
            // 
            // addressPresentCareOfDataTextBox
            // 
            this.addressPresentCareOfDataTextBox.CanGrow = true;
            this.addressPresentCareOfDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0780901908874512D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentCareOfDataTextBox.Name = "addressPresentCareOfDataTextBox";
            this.addressPresentCareOfDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentCareOfDataTextBox.StyleName = "Data";
            this.addressPresentCareOfDataTextBox.Value = "= Fields.AddressPresentCareOf";
            // 
            // addressPresentDistrictCaptionTextBox
            // 
            this.addressPresentDistrictCaptionTextBox.CanGrow = true;
            this.addressPresentDistrictCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3167524337768555D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentDistrictCaptionTextBox.Name = "addressPresentDistrictCaptionTextBox";
            this.addressPresentDistrictCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentDistrictCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPresentDistrictCaptionTextBox.StyleName = "Caption";
            this.addressPresentDistrictCaptionTextBox.Value = "Address Present District:";
            // 
            // addressPresentDistrictDataTextBox
            // 
            this.addressPresentDistrictDataTextBox.CanGrow = true;
            this.addressPresentDistrictDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.55541467666626D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentDistrictDataTextBox.Name = "addressPresentDistrictDataTextBox";
            this.addressPresentDistrictDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentDistrictDataTextBox.StyleName = "Data";
            this.addressPresentDistrictDataTextBox.Value = "= Fields.AddressPresentDistrict";
            // 
            // addressPresentPoliceStationCaptionTextBox
            // 
            this.addressPresentPoliceStationCaptionTextBox.CanGrow = true;
            this.addressPresentPoliceStationCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7940769195556641D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentPoliceStationCaptionTextBox.Name = "addressPresentPoliceStationCaptionTextBox";
            this.addressPresentPoliceStationCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentPoliceStationCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPresentPoliceStationCaptionTextBox.StyleName = "Caption";
            this.addressPresentPoliceStationCaptionTextBox.Value = "Address Present Police Station:";
            // 
            // addressPresentPoliceStationDataTextBox
            // 
            this.addressPresentPoliceStationDataTextBox.CanGrow = true;
            this.addressPresentPoliceStationDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.0327391624450684D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentPoliceStationDataTextBox.Name = "addressPresentPoliceStationDataTextBox";
            this.addressPresentPoliceStationDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentPoliceStationDataTextBox.StyleName = "Data";
            this.addressPresentPoliceStationDataTextBox.Value = "= Fields.AddressPresentPoliceStation";
            // 
            // addressPresentPostCodeCaptionTextBox
            // 
            this.addressPresentPostCodeCaptionTextBox.CanGrow = true;
            this.addressPresentPostCodeCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2714014053344727D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentPostCodeCaptionTextBox.Name = "addressPresentPostCodeCaptionTextBox";
            this.addressPresentPostCodeCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentPostCodeCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.addressPresentPostCodeCaptionTextBox.StyleName = "Caption";
            this.addressPresentPostCodeCaptionTextBox.Value = "Address Present Post Code:";
            // 
            // addressPresentPostCodeDataTextBox
            // 
            this.addressPresentPostCodeDataTextBox.CanGrow = true;
            this.addressPresentPostCodeDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.510063648223877D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.addressPresentPostCodeDataTextBox.Name = "addressPresentPostCodeDataTextBox";
            this.addressPresentPostCodeDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.addressPresentPostCodeDataTextBox.StyleName = "Data";
            this.addressPresentPostCodeDataTextBox.Value = "= Fields.AddressPresentPostCode";
            // 
            // emailCaptionTextBox
            // 
            this.emailCaptionTextBox.CanGrow = true;
            this.emailCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.748725414276123D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.emailCaptionTextBox.Name = "emailCaptionTextBox";
            this.emailCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.emailCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.emailCaptionTextBox.StyleName = "Caption";
            this.emailCaptionTextBox.Value = "Email:";
            // 
            // emailDataTextBox
            // 
            this.emailDataTextBox.CanGrow = true;
            this.emailDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9873876571655273D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
            this.emailDataTextBox.Name = "emailDataTextBox";
            this.emailDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.21782884001731873D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.emailDataTextBox.StyleName = "Data";
            this.emailDataTextBox.Value = "= Fields.Email";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.reportFooter.Name = "reportFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            this.detail.Name = "detail";
            // 
            // AppliedApplicantReport
            // 
            this.DataSource = this.sqlDataSource1;
            group1.GroupFooter = this.applicantIdGroupFooterSection;
            group1.GroupHeader = this.applicantIdGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.ApplicantId"));
            group1.Name = "applicantIdGroup";
            group2.GroupFooter = this.quotaIdGroupFooterSection;
            group2.GroupHeader = this.quotaIdGroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.QuotaId"));
            group2.Name = "quotaIdGroup";
            group3.GroupFooter = this.labelsGroupFooterSection;
            group3.GroupHeader = this.labelsGroupHeaderSection;
            group3.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.applicantIdGroupHeaderSection,
            this.applicantIdGroupFooterSection,
            this.quotaIdGroupHeaderSection,
            this.quotaIdGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "AppliedApplicantReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Bold = true;
            styleRule2.Style.Font.Italic = false;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Style.Font.Strikeout = false;
            styleRule2.Style.Font.Underline = false;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule3.Style.Color = System.Drawing.Color.Black;
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule5.Style.Font.Name = "Tahoma";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.2260499000549316D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection applicantIdGroupHeaderSection;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.GroupFooterSection applicantIdGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection quotaIdGroupHeaderSection;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.GroupFooterSection quotaIdGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox formSerialCaptionTextBox;
        private Telerik.Reporting.TextBox formSerialDataTextBox;
        private Telerik.Reporting.TextBox addressCaptionTextBox;
        private Telerik.Reporting.TextBox addressDataTextBox;
        private Telerik.Reporting.TextBox addressPermanentCaptionTextBox;
        private Telerik.Reporting.TextBox addressPermanentDataTextBox;
        private Telerik.Reporting.TextBox addressPermanentDistrictCaptionTextBox;
        private Telerik.Reporting.TextBox addressPermanentDistrictDataTextBox;
        private Telerik.Reporting.TextBox addressPermanentCareOfCaptionTextBox;
        private Telerik.Reporting.TextBox addressPermanentCareOfDataTextBox;
        private Telerik.Reporting.TextBox addressPermanentPoliceStationCaptionTextBox;
        private Telerik.Reporting.TextBox addressPermanentPoliceStationDataTextBox;
        private Telerik.Reporting.TextBox addressPermanentPostCodeCaptionTextBox;
        private Telerik.Reporting.TextBox addressPermanentPostCodeDataTextBox;
        private Telerik.Reporting.TextBox addressPresentCaptionTextBox;
        private Telerik.Reporting.TextBox addressPresentDataTextBox;
        private Telerik.Reporting.TextBox addressPresentCareOfCaptionTextBox;
        private Telerik.Reporting.TextBox addressPresentCareOfDataTextBox;
        private Telerik.Reporting.TextBox addressPresentDistrictCaptionTextBox;
        private Telerik.Reporting.TextBox addressPresentDistrictDataTextBox;
        private Telerik.Reporting.TextBox addressPresentPoliceStationCaptionTextBox;
        private Telerik.Reporting.TextBox addressPresentPoliceStationDataTextBox;
        private Telerik.Reporting.TextBox addressPresentPostCodeCaptionTextBox;
        private Telerik.Reporting.TextBox addressPresentPostCodeDataTextBox;
        private Telerik.Reporting.TextBox emailCaptionTextBox;
        private Telerik.Reporting.TextBox emailDataTextBox;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
    }
}