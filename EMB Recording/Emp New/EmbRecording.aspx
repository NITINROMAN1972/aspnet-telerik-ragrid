<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="EmbRecording.aspx.cs" Inherits="Emp_Calculation_EmbRecording" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMB Recording</title>

    <!--Bootstrap CSS-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!--Bootstrap JS-->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <!--jQuery-->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!--Using JavaScript library such as Select2-->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>

    <!-- SweetAlert2 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11.10.3/dist/sweetalert2.min.css" rel="stylesheet" />
    <!-- SweetAlert2 JS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.10.3/dist/sweetalert2.all.min.js"></script>


    <script src="EMBRecording.js"></script>
    <link rel="stylesheet" type="text/css" href="EMBRecording.css" />

    <style>
        
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="mx-2 my-2 mx-auto col-md-11">
            <div class="fw-semibold fs-5 text-dark">
                <asp:Literal ID="lit" Text="EMB Recording" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="card no-b no-r px-1 mt-1 shadow-sm mx-auto col-md-11">
            <div class="card-body">
                <div class="row mb-2">
                    <div class="form-row col-md-6 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold">
                                <asp:Literal ID="Literal1" Text="Category" runat="server"></asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddCat" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="(Please select the category)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                            <%--<div class="border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">--%>
                            <div class="py-1">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddCat" runat="server" OnSelectedIndexChanged="ddCat_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="form-row col-md-6 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold">
                                <asp:Literal ID="Literal3" Text="Sub Category" runat="server"></asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddSubCat" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="(Please select the sub category)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="py-1">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddSubCat" runat="server" OnSelectedIndexChanged="ddSubCat_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=" row mb-2">
                    <div class="form-row col-md-12 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal4" Text="Project Master" runat="server"></asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddProjectMaster" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="(Please select the project)" SetFocusOnError="True" Display="Dynamic" ToolTip="ddProjectMaster"></asp:RequiredFieldValidator>
                            </div>
                            <div class="py-1">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddProjectMaster" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddProjectMaster_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                </div>
                <div class=" row mb-2">
                    <div class="form-row col-md-6 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal6" Text="Work Order No." runat="server"></asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddWorkOrder" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="(Please select the work order)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="py-1">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddWorkOrder" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddWorkOrder_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="form-row col-md-6 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal7" Text="Vendor Name" runat="server"></asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddVender" CssClass="invalid-feedback" InitialValue="0" runat="server" ErrorMessage="(Please select the vendor)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="py-1">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddVender" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddVender_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class=" row mb-2">
                    <div class="form-row col-md-6 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal2" Text="Work Order Amount" runat="server"></asp:Literal>
                            </div>
                            <asp:TextBox runat="server" ID="txtWoAmnt" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row col-md-6 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal8" Text="Total Upto Previous RA Amount" runat="server"></asp:Literal>
                            </div>
                            <asp:TextBox runat="server" ID="txtUpToTotalRaAmnt" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class=" row mb-2">
                    <div class="form-row col-md-12">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal9" Text="Remarks" runat="server"></asp:Literal>
                            </div>
                            <textarea runat="server" id="txtRemarks" class="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></textarea>
                        </div>
                    </div>
                </div>
                <div class=" row mb-2">
                    <div class="form-row col-md-4 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal10" Text="Abstract (EMB Recording) Date" runat="server"></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="dateAbstract" CssClass="invalid-feedback" CultureInvariantValues="true" InitialValue="" runat="server" ErrorMessage="(Please select the date)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <asp:TextBox runat="server" ID="dateAbstract" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row col-md-4 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal11" Text="Measurement From Date" runat="server"></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="dateMeasuredFrom" CssClass="invalid-feedback" CultureInvariantValues="true" InitialValue="" runat="server" ErrorMessage="(Please select the date)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="dateMeasuredFrom" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row col-md-4 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal12" Text="Measurement To Date" runat="server"></asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="dateMeasuredTo" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="(Please select the date)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="dateMeasuredTo" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="gridEmbDiv" runat="server" visible="false" class="mt-5 mx-auto col-md-11">
            <div class="">
                <asp:GridView ShowHeaderWhenEmpty="true" ID="gridDynamicBOQ" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridDyanmic_RowDataBound"
                    CssClass="table table-bordered  border border-1 border-secondary-subtle table-hover text-center grid-custom">
                    <HeaderStyle CssClass="align-middle"/>
                    <Columns>
                        <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                            <ItemTemplate>
                                <asp:HiddenField ID="id" runat="server" Value="id" />
                                <span>
                                    <%#Container.DataItemIndex + 1%>
                                </span>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-md-1" />
                            <ItemStyle Font-Size="15px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="BoqItem" HeaderText="Item Description" SortExpression="BoqItem" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                        <asp:BoundField DataField="BoqUOM" HeaderText="UOM" SortExpression="BoqUOM" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        <asp:BoundField DataField="BoqQty" HeaderText="BoQ Qty" SortExpression="BoqQty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        <asp:BoundField DataField="BoqPenQty" HeaderText="Pending Qty" SortExpression="BoqPenQty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />

                        <asp:TemplateField HeaderText="Qty Measured" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle">
                            <ItemTemplate>
                                <asp:TextBox ID="QtyMeasure" runat="server" Enabled="true" CssClass="col-md-12 fw-normal border border-dark-subtle shadow-sm rounded-1 px-2" type="number" Text='<%# Bind("QtyMeasure") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="UptoPreRaQty" HeaderText="Upto Previous RA Qty" SortExpression="UptoPreRaQty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        <asp:BoundField DataField="QtyDiff" HeaderText="Diff In Qty" SortExpression="QtyDiff" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        <asp:BoundField DataField="BoqRate" HeaderText="BoQ Unit Rate" SortExpression="BoqRate" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
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
                        <asp:Button ID="btnSubmitBasicAmount" Enabled="false" runat="server" Text="Submit Basic Amount" OnClick="btnSubmitBasicAmount_Click" CssClass="btn btn-primary mb-3" />
                    </div>
                </div>

            </div>
        </div>

    </form>
</body>
</html>
