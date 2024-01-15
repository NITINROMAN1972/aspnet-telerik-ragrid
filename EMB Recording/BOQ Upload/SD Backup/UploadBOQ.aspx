<%@ Page Language="C#" UnobtrusiveValidationMode="None" AutoEventWireup="true" CodeFile="UploadBOQ.aspx.cs" Inherits="Emp_Calculation_UploadBOQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload BoQ</title>

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

    <script src="UploadBOQ.js"></script>

    <style>
        .text-bg-custom {
            background-color: #425ad1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card no-b no-r px-4 mt-3 shadow-sm mx-auto col-md-11">
            <div class="mt-3">
                <label for="email" class="col-form-label badge text-bg-custom text-light fw-normal fs-6 col-sm-2 shadow border border-1 py-2">
                    <i class="icon-wpforms mr-2"></i>
                    <asp:Literal ID="Literal2" Text="Upload BoQ" runat="server"></asp:Literal>
                </label>
            </div>
            <div class="card-body">
                <div class="card-body row">
                    <div class="form-row mt-1 col-md-6">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal4" Text="Project Master" runat="server"></asp:Literal>
                            </div>
                            <div class="border border-light-subtle bg-light shadow-sm">
                                <asp:DropDownList ID="ddProjectMaster" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddProjectMaster_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-row mt-1 col-md-6">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal5" Text="AO No." runat="server"></asp:Literal>
                            </div>
                            <div class="border border-light-subtle bg-light shadow-sm">
                                <asp:DropDownList ID="ddAODetails" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddAODetails_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body row">
                    <div class="form-row mt-1 col-md-6">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal6" Text="Work Order/Tender No" runat="server"></asp:Literal>
                            </div>
                            <div class="border border-light-subtle bg-light shadow-sm">
                                <asp:DropDownList ID="ddWorkOrder" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddWorkOrder_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-row mt-1 col-md-6">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal1" Text="Work Order/Tender Value" runat="server"></asp:Literal>
                            </div>
                            <div class="border border-light-subtle bg-light shadow-sm">
                                <asp:DropDownList ID="ddWoTendorValue" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddWoTendorValue_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>





        <div class="container mt-5" style="width: 50%">
            <label for="fileExcel" class="form-label">Upload Excel</label>
            <div class="mb-3 d-flex justify-content-center">
                <div class="input-group has-validation">
                    <%--<span class="input-group-text" id="inputGroupPrepend">Choose Excel</span>--%>
                    <asp:FileUpload ID="fileExcel" runat="server" CssClass="form-control" aria-describedby="inputGroupPrepend" required />
                    <asp:Button ID="btnExclUpload" runat="server" OnClick="btnExclUpload_Click" Text="Upload File" CssClass="btn btn-outline-secondary" />
                    <div class="invalid-feedback">
                        Please choose a excel file
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
