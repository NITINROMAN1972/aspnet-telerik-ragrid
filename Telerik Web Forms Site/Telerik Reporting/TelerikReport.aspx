<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TelerikReport.aspx.cs" Inherits="Telerik_Reporting_TelerikReport" EnableViewState="true" EnableEventValidation="true" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>
<%--<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Telerik</title>

    <!--Bootstrap CSS-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <!--jQuery-->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-KyZXEAg3QhqLMpG8r+J2Wk5vqXn3Fm/z2N1r8f6VZJ4T3Hdvh4kXG1j4fZ6IsU2f5" crossorigin="anonymous"></script>
    <!--AJAX JS-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <!--Bootstrap JS-->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>

    <script src="RABillNew.js"></script>
    <link rel="stylesheet" type="text/css" href="RABillNew.css" />

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="card col-md-11 mx-auto mt-5">
            <div class="card">

                <!-- skin - Default,Simple,WebBlue,Black,Vista,Windows7,Office2007,Office2010Black,Office2010Blue,Office2010Silver,Outlook,Silk,Glow,Metro,MetroTouch,Sunset,Telerik,Web20,WebBlue,  -->
                <telerik:RadGrid ID="RadGridID" runat="server" OnNeedDataSource="RadGridID_NeedDataSource" OnInsertCommand="RadGridID_InsertCommand" Skin="Office2007" CssClass="border"
                    AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true" ShowGroupPanel="true" ShowFooter="true" PageSize="10" AutoGenerateColumns="false" Visible="true" border-spacing="false">

                    <HeaderStyle VerticalAlign="Top" BorderColor="#9eb6ce" BorderStyle="Groove" CssClass="border text-center"/>

                    <%-- PagerStyle-Position="TopAndBottom" --%>
                    <MasterTableView CommandItemDisplay="TopAndBottom" ShowGroupFooter="true" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" ClientDataKeyNames="id" AllowAutomaticInserts="true"
                        AlternatingItemStyle-BackColor="Snow" AlternatingItemStyle-ForeColor="Maroon" Caption="Caption Text" HeaderStyle-Width="200px">

                        <CommandItemSettings ShowAddNewRecordButton="true" AddNewRecordText="ADD RECORD" ShowExportToPdfButton="true" ShowExportToExcelButton="true" ShowExportToWordButton="true"
                            ExportToPdfText="export to PPDF" ExportToExcelText="export to .xls" ExportToWordText="export to WORD" RefreshText="Please dont refresh" ShowRefreshButton="true" />

                        <Columns>
                            <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" HeaderStyle-Font-Bold="true" FooterStyle-Height="" ItemStyle-CssClass="text-center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SlotID" HeaderText="SlotID" SortExpression="SlotID" HeaderStyle-Font-Bold="true" FooterStyle-Height="" ItemStyle-CssClass="text-center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SlDate" HeaderText="SlDate" SortExpression="SlDate" HeaderStyle-Font-Bold="true" FooterStyle-Height=""></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SlTime1" HeaderText="SlTime1" SortExpression="SlTime1" HeaderStyle-Font-Bold="true" FooterStyle-Height=""></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SlTime2" HeaderText="SlTime2" SortExpression="SlTime2" HeaderStyle-Font-Bold="true" FooterStyle-Height=""></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SlCount" HeaderText="SlCount" SortExpression="SlCount" HeaderStyle-Font-Bold="true" FooterStyle-Height="" ItemStyle-CssClass="text-center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SlRole" HeaderText="SlRole" SortExpression="SlRole" HeaderStyle-Font-Bold="true" FooterStyle-Height=""></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SlDist" HeaderText="SlDist" SortExpression="SlDist" HeaderStyle-Font-Bold="true" FooterStyle-Height=""></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SlSavedOn" HeaderText="SlSavedOn" SortExpression="SlSavedOn" HeaderStyle-Font-Bold="true" FooterStyle-Height=""></telerik:GridBoundColumn>
                        </Columns>

                    </MasterTableView>

                    <PagerStyle AlwaysVisible="true" Mode="NextPrevNumericAndAdvanced"></PagerStyle>

                    <ExportSettings FileName="ExportFile_Name" OpenInNewWindow="true" ExportOnlyData="true" IgnorePaging="true" Excel-Format="ExcelML">
                        <Pdf PageWidth="1500px" PaperSize="A4" PageTitle="" PageTopMargin="50px" PageBottomMargin="100px" UserPassword="101" DefaultFontFamily="Arial" BorderStyle="Thin" BorderType="AllBorders"></Pdf>
                        <Excel Format="ExcelML" FileExtension=".xls" />
                    </ExportSettings>

                    <ValidationSettings EnableValidation="true" />

                    <PagerStyle BackColor="Yellow" BorderStyle="Dashed" />

                    <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true">
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>

                </telerik:RadGrid>

            </div>
        </div>


    </form>
</body>
</html>
