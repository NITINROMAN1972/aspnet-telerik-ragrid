<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableViewState="true" EnableEventValidation="true" %>


<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_Default" %>--%>
<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>
<%--<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .borderLessDialog {
            margin-top: 76px;
        }

        .text-center {
        }
    </style>
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>
    <style>
        .form-row {
            margin: 0 !important;
        }

        .chzn-container-single .chzn-single {
            height: 39px !important;
        }

        .search-choice-close {
            top: 5px !important;
        }
    </style>
    <script type="text/javascript">
        Sys.UI.Point = function Sys$UI$Point(x, y) {

            x = Math.round(x);
            y = Math.round(y);

            var e = Function._validateParams(arguments, [
                { name: "x", type: Number, integer: true },
                { name: "y", type: Number, integer: true }
            ]);
            if (e) throw e;
            this.x = x;
            this.y = y;
        }
    </script>
    <script type="text/javascript">
        //<![CDATA[
        function ParameterMap(sender, args) {
            //If you want to send a parameter to the select call you can modify the if 
            //statement to check whether the request type is 'read':
            //if (args.get_type() == "read" && args.get_data()) {
            if (args.get_type() != "read" && args.get_data()) {
                args.set_parameterFormat({ customersJSON: kendo.stringify(args.get_data().models) });
            }
        }

        function Parse(sender, args) {
            var response = args.get_response().d;
            if (response) {
                args.set_parsedData(response.Data);
            }
        }

        function UserAction(sender, args) {
            if (sender.get_batchEditingManager().hasChanges(sender.get_masterTableView()) &&
                !confirm("Any changes will be cleared. Are you sure you want to perform this action?")) {
                args.set_cancel(true);
            }
        }
        //]]>
    </script>
    <script type="text/javascript">
        function myFunction() {
            var element = document.getElementById("Radgrid1");
            element.className = element.className.replace('.no-b th', "");
        }
    </script>
    <script>        
        function OnClientClick(value) {
            var oWnd = radopen("Window.aspx?CustomerID=" + value, "RadWindow1");
        }
    </script>
    <script type="text/javascript">
        function OnClientClicking(button, args) {
            window.location = button.get_navigateUrl();
            args.set_cancel(true);
        }
    </script>
    <style type="text/css">
        .imga {
            width: 36px;
            height: 36px;
        }
    </style>
    <script>        
        function postBack() {
            __doPostBack('LinkButton1', '');
            return true;
        }
    </script>

    <script>
        function base64ToArrayBuffer(data) {
            var binaryString = window.atob(data);
            var binaryLen = binaryString.length;
            var bytes = new Uint8Array(binaryLen);
            for (var i = 0; i < binaryLen; i++) {
                var ascii = binaryString.charCodeAt(i);
                bytes[i] = ascii;
            }
            return bytes;
        };

        $.ajax({
            method: "GET",
            url: '@Url.Action("GenerateReportPDF","ControllerName")',
            async: false,
            cache: false,
            data: { reportName }
        }).done(function (result) {
            //if the call was successful
            if (result) {
                buffer = base64ToArrayBuffer(result)
                var file = new Blob([buffer], { type: "application/pdf" })

                var link = document.createElement('a')
                link.href = window.URL.createObjectURL(file)
                link.download = "Report.pdf";
                document.body.appendChild(link)
                link.click()
            }
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



        <div class="card no-b  no-r">
            <telerik:RadGrid ID="Radgrid1" runat="server" OnNeedDataSource="Radgrid1_NeedDataSource" OnInsertCommand="Radgrid1_InsertCommand" Skin="Office2007" CssClass="borderLessDialog" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
                ShowGroupPanel="true" ShowFooter="true"
                PageSize="30" AutoGenerateColumns="false" Visible="true" border-spacing="false">
                <HeaderStyle VerticalAlign="Top" BorderColor="#9eb6ce" BorderStyle="Groove" />
                <MasterTableView CommandItemDisplay="TopAndBottom" ShowGroupFooter="true" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
                    ClientDataKeyNames="EmployeeID" AllowAutomaticInserts="true">
                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowExportToPdfButton="true" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FirstName" HeaderText="EmployeeFirstName" SortExpression="EmployeeFirstName" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LastName" HeaderText="EmployeeLastName" SortExpression="EmployeeLastName" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Salary" HeaderText="EmployeeSalary" SortExpression="EmployeeSalary" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HireDate" HeaderText="HiringDate" SortExpression="HireDate" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Department" HeaderText="Department" SortExpression="Department" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BirthDate" HeaderText="BirthDate" SortExpression="BirthDate" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" SortExpression="Address" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <PagerStyle AlwaysVisible="true" Mode="NumericPages"></PagerStyle>
                <ExportSettings ExportOnlyData="true" IgnorePaging="true" Excel-Format="ExcelML" OpenInNewWindow="true" >
                    <Pdf PageWidth="1500px" PaperSize="A4" DefaultFontFamily="Arial" BorderStyle="Thin" BorderType="AllBorders"  ></Pdf>  
                </ExportSettings>
                <%--<ExportSettings FileName="summary_SystemUsers" OpenInNewWindow="True" ExportOnlyData="True" Excel-Format="ExcelML">
                                      
                    <Pdf PageWidth="1500px" PaperSize="A4" DefaultFontFamily="Arial" BorderStyle="Thin" BorderType="AllBorders"></Pdf>
                                      
                    <Excel Format="ExcelML" />
                                  
                </ExportSettings>--%>
                <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true">
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
            </telerik:RadGrid>

        </div>

    </form>




</body>
</html>
