<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


    <%@ Import Namespace="Telerik.Reporting" %>

        <%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=9.1.15.624, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

            <!DOCTYPE html>


            <html xmlns="http://www.w3.org/1999/xhtml">

            <head runat="server">
                <title>
                    <asp:Literal ID="PageTitle" runat="server"></asp:Literal>
                    | Scholer Pipilika Soft</title>

                <meta http-equiv="X-UA-Compatible" content="IE=edge" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0" />
                <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

                <style type="text/css">
                    html,
                    body,
                    form {
                        height: 100%;
                        width: 100%;
                        border: 0;
                        margin: 0;
                        padding: 0;
                        background: #eee;
                    }
                    
                    iframe body .print-page>.sheet {
                        border: 1px solid #A59C9C;
                    }
                    
                    .alert {
                        padding: 35px;
                        margin: 35px;
                        margin-bottom: 20px;
                        color: #fff;
                        border-width: 0;
                        border-left-width: 5px;
                        padding: 10px;
                        border-radius: 0;
                        text-align: center;
                        -webkit-box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
                        -moz-box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
                        box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
                        -webkit-border-radius: 3px;
                        -webkit-background-clip: padding-box;
                        -moz-border-radius: 3px;
                        -moz-background-clip: padding;
                        border-radius: 3px;
                    }
                    
                    .alert.alert-danger {
                        border-left: 5px solid #df5138;
                        background: #e46f61;
                    }
                    
                    .alert.alert-success {
                        border-left: 5px solid #8cc474;
                        background: #a0d468;
                    }
                    
                    strong {
                        font-weight: 700;
                    }
                </style>
            </head>

            <body>
                <form id="HtmlForm" runat="server">
                    <script runat="server">
                        public override void VerifyRenderingInServerForm(Control control) {
                            // to avoid the server form (<form runat="server"> requirement
                        }

                        protected override void OnLoad(EventArgs e) {
                            base.OnLoad(e);
                            PageTitle.Text = "Report Viewer";
                            lblError.Text = "Error! Oops Something went wrong!";

                            if (!string.IsNullOrEmpty(ViewBag.Title))
                                PageTitle.Text = ViewBag.Title;
                            if (!string.IsNullOrEmpty(ViewBag.Message))
                                lblError.Text = ViewBag.Message;;

                            InstanceReportSource report = (InstanceReportSource) ViewBag.Report;
                            //convert Report to InstanceReportSource 
                            //var instanceReportSource = new Telerik.Reporting.InstanceReportSource { ReportDocument = report };

                            if (report != null) {
                                pnlError.Visible = false;
                                if (ReportViewer1 != null) {
                                    ReportViewer1.Visible = true;
                                    ReportViewer1.ReportSource = report;
                                    //ReportViewer1.RefreshData();
                                    ReportViewer1.RefreshReport();
                                }
                            } else {
                                ReportViewer1.Visible = false;
                                pnlError.Visible = true;
                            }

                        }
                    </script>

                    <telerik:ReportViewer ID="ReportViewer1" ViewMode="PrintPreview" runat="server" Width="99%" Height="99%" Resources-SessionHasExpiredError="Plese reload, your session has expired!"></telerik:ReportViewer>
                    <asp:Panel runat="server" ID="pnlError" Visible="False" Style="">
                        <div class="alert alert-danger fade in">

                            <asp:Literal ID="lblError" runat="server"></asp:Literal>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlSuccess" Visible="False" Style="">
                        <div class="alert alert-danger fade in">

                            <asp:Literal ID="lblSuccess" runat="server"></asp:Literal>
                        </div>
                    </asp:Panel>


                </form>
            </body>

            </html>