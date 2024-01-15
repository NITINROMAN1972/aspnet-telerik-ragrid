<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="UpdateEMB.aspx.cs" Inherits="Emp_Calculation_UpdateEMB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMB Update</title>

    <!--Bootstrap CSS-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <!--jQuery-->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-KyZXEAg3QhqLMpG8r+J2Wk5vqXn3Fm/z2N1r8f6VZJ4T3Hdvh4kXG1j4fZ6IsU2f5" crossorigin="anonymous"></script>
    <!--AJAX JS-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <!--Bootstrap JS-->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>

    <!--Using JavaScript library such as Select2-->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>

    <script src="UpdateEMB.js"></script>
    <link rel="stylesheet" type="text/css" href="UpdateEMB.css" />

</head>
<body class="">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div id="divTopSearch" runat="server" visible="true">
            <div class="justify-content-end d-flex mb-2 mt-4 px-0 mx-auto col-md-11">
                <div class="col-md-6">
                    <div class="fw-semibold fs-5 text-dark">
                        <asp:Literal ID="Literal14" Text="EMB Recording" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="col-md-6 text-end">
                    <div class="fw-semibold fs-5">
                        <asp:Button ID="btnNewEmb" runat="server" Text="New EMB +" OnClick="btnNewEmb_Click" CssClass="btn btn-primary shadow" />
                    </div>
                </div>
            </div>

            <div id="divSearchEmb" runat="server" visible="true">
                <div class="card mt-1 shadow-sm mx-auto col-md-11">
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="form-row col-md-6 align-self-end">
                                <div class="form-group m-0">
                                    <div class="mb-1 text-body-tertiary fw-semibold">
                                        <asp:Literal ID="Literal13" Text="Project" runat="server"></asp:Literal>
                                    </div>
                                    <div class="border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddProject" runat="server" OnSelectedIndexChanged="ddProject_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row col-md-6 align-self-end">
                                <div class="form-group m-0">
                                    <div class="mb-1 text-body-tertiary fw-semibold">
                                        <asp:Literal ID="Literal16" Text="Work Order" runat="server"></asp:Literal>
                                    </div>
                                    <div class="border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddWOName" runat="server" OnSelectedIndexChanged="ddWOName_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="form-row col-md-6 align-self-end">
                                <div class="form-group m-0">
                                    <div class="mb-1 text-body-tertiary fw-semibold">
                                        <asp:Literal ID="Literal15" Text="Vendor Name & Code" runat="server"></asp:Literal>
                                    </div>
                                    <div class="border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddVendorName" runat="server" OnSelectedIndexChanged="ddVendorName_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-2 mt-3">
                            <div class="form-row col-md-6 align-self-end">
                                 <asp:Button ID="btnTruncate" runat="server" Text="Truncate EMB" OnClick="btnTruncate_Click" CssClass="btn btn-danger shadow" />
                            </div>
                            <div class="form-row col-md-6 align-self-end text-end">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary col-md-2 shadow" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="mx-auto col-md-11">
            <div id="gridEmbDiv" visible="false" runat="server" class="mt-5">
                <div class="">
                    <asp:GridView ShowHeaderWhenEmpty="true" ID="gridEMBHeader" runat="server" AutoGenerateColumns="false" OnRowCommand="GrdUser_RowCommand" AllowPaging="true" PageSize="10"
                        CssClass="table table-bordered border border-1 border-dark-subtle table-hover text-center" OnPageIndexChanging="GridEmbHeader_PageIndexChanging" PagerStyle-CssClass="gridview-pager">
                        <HeaderStyle CssClass="align-middle table-primary" />
                        <Columns>
                            <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="id" runat="server" Value="id" />
                                    <span>
                                        <%#Container.DataItemIndex + 1%>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle CssClass="col-md-1" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmbWO" HeaderText="Work Order No." SortExpression="EmbWO" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="EmbPM" HeaderText="Project Name" SortExpression="EmbPM" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="EmbVenN" HeaderText="Vendor" SortExpression="EmbVenN" ItemStyle-Width="" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="EmbAbstractDt" HeaderText="Recording Date" SortExpression="EmbAbstractDt" ItemStyle-Width="100px" ItemStyle-CssClass="text-center fw-light" />
                            <asp:BoundField DataField="EmbMeaFromDt" HeaderText="Measured From Date" SortExpression="EmbMeaFromDt" ItemStyle-Width="100px" ItemStyle-CssClass="text-center fw-light " />
                            <asp:BoundField DataField="EmbMeaToDt" HeaderText="Measured To Date" SortExpression="EmbMeaToDt" ItemStyle-Width="100px" ItemStyle-CssClass="text-center fw-light " />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnedit" CommandArgument='<%# Eval("EmbMasRefId") %>' CommandName="lnkView" ToolTip="Edit" CssClass="shadow-sm">
                                        <asp:Image runat="server" ImageUrl="../img/edit.png" AlternateText="Edit" style="width: 16px; height: 16px;"/>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div id="divEMBUpdate" runat="server" visible="false">
            <div class="mx-2 my-2 mx-auto col-md-11">
                <div class="fw-semibold fs-5 text-dark">
                    <asp:Literal ID="lit" Text="EMB Recording Update" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="card no-b no-r px-1 mt-1 shadow-sm mx-auto col-md-11">
                <div class="card-body">
                    <div class=" row mb-2">
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal2" Text="Category" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="ddCat" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal8" Text="Sub Category" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="ddSubCat" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class=" row mb-2">
                        <div class="form-row col-md-12 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal1" Text="Project" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="ddProjectMaster" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal3" Text="AO No." runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="ddAODetails" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>--%>
                    </div>
                    <div class=" row mb-2">
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal4" Text="Work Order No." runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="ddWorkOrder" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal5" Text="Vendor Name" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="ddVender" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class=" row mb-2">
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal6" Text="Work Order Amount" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="txtWoAmnt" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal7" Text="Total Upto Previous RA Amount" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="txtUpToTotalRaAmnt" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class=" row mb-2">
                        <div class="form-row col-md-12">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal9" Text="Remarks" runat="server"></asp:Literal>
                                </div>
                                <textarea runat="server" id="txtRemarks" disabled="disabled" class="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class=" row mb-2">
                        <div class="form-row col-md-4 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal10" Text="Abstract (EMB Recording) Date" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="dateAbstract" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row col-md-4 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal11" Text="Measurement From Date" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="dateMeasuredFrom" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row col-md-4 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal12" Text="Measurement To Date" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="dateMeasuredTo" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="embDetailsUpdate" runat="server" visible="false" class="mt-5 mx-auto col-md-11">
                <div class="">
                    <asp:GridView ShowHeaderWhenEmpty="true" ID="gridDynamicBOQ" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridDyanmic_RowDataBound"
                        CssClass="table table-bordered border border-1 border-dark-subtle table-hover text-center">
                        <HeaderStyle CssClass="align-middle table-primary" />
                        <Columns>
                            <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="id" runat="server" Value="id" />
                                    <span class="col-md-1">
                                        <%#Container.DataItemIndex + 1%>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle CssClass="col-md-1" />
                                <ItemStyle Font-Size="15px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="BoQItemName" HeaderText="Item Description" SortExpression="BoQItemName" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" ControlStyle-Font-Size="Smaller" />
                            <asp:BoundField DataField="BoQUOM" HeaderText="UOM" SortExpression="BoQUOM" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                            <asp:BoundField DataField="BoqQty" HeaderText="BoQ Total Qty" SortExpression="BoqQty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                            <asp:BoundField DataField="BoqPenQty" HeaderText="BoQ Pending Qty" SortExpression="BoqPenQty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />

                            <asp:TemplateField HeaderText="Qty Measured" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle">
                                <ItemTemplate>
                                    <asp:TextBox ID="BoqQtyMeas" runat="server" Enabled="true" CssClass="col-md-12 fw-normal border border-dark-subtle shadow-sm rounded-1 px-2 py-1" type="number" Text='<%# Bind("BoqQtyMeas") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="BoQUptoPreRaQty" HeaderText="Upto Previous RA Qty" SortExpression="BoQUptoPreRaQty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                            <asp:BoundField DataField="BoqQtyDIff" HeaderText="Diff In Qty" SortExpression="BoqQtyDIff" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                            <asp:BoundField DataField="BoQItemRate" HeaderText="BoQ Unit Rate" SortExpression="BoQItemRate" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        </Columns>
                    </asp:GridView>

                    <div class="mt-5 mb-2">
                        <div class="text-end">
                            <asp:Button ID="btnBasicAmount" runat="server" Text="Calculate (Basic Amount)" OnClick="btnBasicAmount_Click" CssClass="btn btn-info mb-3" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-9"></div>
                        <div class="col-md-3">
                            <div class="input-group shadow-sm">
                                <span class="input-group-text fs-5 fw-semibold">₹</span>
                                <asp:TextBox runat="server" ID="txtBasicAmt" CssClass="form-control fw-lighter border border-2" ReadOnly="true" placeholder="Total Basic Amount"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mt-5 mb-3">
                        <div class="text-end">
                            <asp:Button ID="btnSubmitBasicAmount" runat="server" Text="Submit Basic Amount" OnClick="btnSubmitBasicAmount_Click" CssClass="btn btn-primary mb-3" />
                        </div>
                    </div>

                </div>
            </div>
        </div>





        <div class="mt-5 mx-auto col-md-11">
            <div class="">
                <asp:GridView ShowHeaderWhenEmpty="true" ID="GridTest" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered border border-1 border-dark-subtle table-hover text-center">
                    <HeaderStyle CssClass="align-middle custom-header-style" />
                    <Columns>
                        <asp:TemplateField ControlStyle-CssClass="col-xs-1" HeaderText="Sr.No">
                            <ItemTemplate>
                                <asp:HiddenField ID="id" runat="server" Value="id" />
                                <span>
                                    <%#Container.DataItemIndex + 1%>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </form>
</body>
</html>
