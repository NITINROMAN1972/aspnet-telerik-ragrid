using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Emp_Calculation_EmbRecording : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    string selectedCategoryRefID;
    string selectedSubCategoryRefID;
    string selectedAODetails;
    string selectedProjectMasterRefID;
    string selectedWorkOrderRefID;
    int selectedWorkOrderAmount;
    string selectedVendorRefID;

    protected void Page_Load(object sender, EventArgs e)
    {
        // PMS2 - 874

        if (!IsPostBack)
        {
            Bind_Role_Category();
        }
    }

    //=============================={ Sweet Alert }============================================

    // sweet alert - success only
    private void getSweetAlertSuccessOnly()
    {
        string title = "Saved!";
        string message = "Record saved successfully!";
        string icon = "success";
        string confirmButtonText = "OK";

        string sweetAlertScript =
            $@"<script>
                Swal.fire({{ 
                    title: '{title}', 
                    text: '{message}', 
                    icon: '{icon}', 
                    confirmButtonText: '{confirmButtonText}' 
                }});
            </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    // sweet alert - success redirect
    private void getSweetAlertSuccessRedirect(string redirectUrl)
    {
        string title = "Saved!";
        string message = "Record saved successfully!";
        string icon = "success";
        string confirmButtonText = "OK";

        string sweetAlertScript =
            $@"<script>
                Swal.fire({{ 
                    title: '{title}', 
                    text: '{message}', 
                    icon: '{icon}', 
                    confirmButtonText: '{confirmButtonText}' 
                }}).then((result) => {{
                    if (result.isConfirmed) {{
                        window.location.href = '{redirectUrl}';
                    }}
                }});
            </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    // sweet alert - success redirect block
    private void getSweetAlertSuccessRedirectMandatory(string titles, string mssg, string redirectUrl)
    {
        string title = titles;
        string message = mssg;
        string icon = "success";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        string sweetAlertScript =
        $@"<script>
            Swal.fire({{ 
                title: '{title}', 
                text: '{message}', 
                icon: '{icon}', 
                confirmButtonText: '{confirmButtonText}', 
                allowOutsideClick: {allowOutsideClick}
            }}).then((result) => {{
                if (result.isConfirmed) {{
                    window.location.href = '{redirectUrl}';
                }}
            }});
        </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    // sweet alert - error only block
    private void getSweetAlertErrorMandatory(string titles, string mssg)
    {
        string title = titles;
        string message = mssg;
        string icon = "error";
        string confirmButtonText = "OK";
        string allowOutsideClick = "false"; // Prevent closing on outside click

        string sweetAlertScript =
        $@"<script>
            Swal.fire({{ 
                title: '{title}', 
                text: '{message}', 
                icon: '{icon}', 
                confirmButtonText: '{confirmButtonText}', 
                allowOutsideClick: {allowOutsideClick}
            }});
        </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "sweetAlert", sweetAlertScript, false);
    }

    //=============================={ Bind Drop Downs }============================================

    public void Bind_Role_Category()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select CatyName, CatyCode, RefID from Caty874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddCat.DataSource = dt;
            ddCat.DataTextField = "CatyName";
            ddCat.DataValueField = "RefID";
            ddCat.DataBind();
            ddCat.Items.Insert(0, new ListItem("------Select Category------", "0"));
        }
    }

    public void Bind_Role_SubCategory(string selectedCategoryRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select SubCaty, SubCatyName, SubCatyCode, RefId from SubCaty874 where SubCatyName = @SubCatyName";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@SubCatyName", selectedCategoryRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddSubCat.DataSource = dt;
            ddSubCat.DataTextField = "SubCaty";
            ddSubCat.DataValueField = "RefId";
            ddSubCat.DataBind();
            ddSubCat.Items.Insert(0, new ListItem("------Select Sub Category------", "0"));
        }
    }

    public void Bind_Role_ProjectMaster(string selectedCategoryRefID, string selectedSubCategoryRefID)
    {
        DataTable categoryDt = getCategory(selectedCategoryRefID); // Ref ID
        DataTable subCategoryDt = getSubCategory(selectedSubCategoryRefID); // Ref Id

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ProjectMaster874 where ProjMasterCaty = @ProjMasterCaty and ProjSubCaty = @ProjSubCaty";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ProjMasterCaty", categoryDt.Rows[0]["RefID"].ToString());
            cmd.Parameters.AddWithValue("@ProjSubCaty", subCategoryDt.Rows[0]["RefId"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddProjectMaster.DataSource = dt;
            ddProjectMaster.DataTextField = "ProjectName";
            ddProjectMaster.DataValueField = "RefID";
            ddProjectMaster.DataBind();
            ddProjectMaster.Items.Insert(0, new ListItem("------Select Sub Category------", "0"));
        }
    }

    public void Bind_Role_AODetails(string selectedCategoryRefID, string selectedSubCategoryRefID, string selectedProjectMasterRefID)
    {
        DataTable categoryDt = getCategory(selectedCategoryRefID); // ref id
        DataTable subCategoryDt = getSubCategory(selectedSubCategoryRefID); // ref id
        DataTable projectMasterDt = getProjectMaster(selectedProjectMasterRefID); // ref id

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            //string sql = "select * from AcceptAO874 where AccptAOCaty=@AccptAOCaty and AccptAOSubCaty=@AccptAOSubCaty and AccptAOProj=@AccptAOProj";
            string sql = "select * from AODetails874 where AOCaty=@AOCaty and AOSubCaty=@AOSubCaty and AOProject=@AOProject";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AOCaty", categoryDt.Rows[0]["RefID"].ToString());
            cmd.Parameters.AddWithValue("@AOSubCaty", subCategoryDt.Rows[0]["RefId"].ToString());
            cmd.Parameters.AddWithValue("@AOProject", projectMasterDt.Rows[0]["RefID"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            //ddAODetails.DataSource = dt;
            //ddAODetails.DataTextField = "AOTitle";
            //ddAODetails.DataValueField = "AONo";
            //ddAODetails.DataBind();
            //ddAODetails.Items.Insert(0, new ListItem("------Select A.O.------", "0"));
        }
    }

    public void Bind_Role_WorkOrderDetails(string selectedProjectMasterRefID)
    {
        DataTable projectMasterDt = getProjectMaster(selectedProjectMasterRefID); // project ref id

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from WorkOrder874 where woProject=@woProject";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@woProject", projectMasterDt.Rows[0]["RefID"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddWorkOrder.DataSource = dt;
            ddWorkOrder.DataTextField = "woTitle";
            ddWorkOrder.DataValueField = "RefID";
            ddWorkOrder.DataBind();
            ddWorkOrder.Items.Insert(0, new ListItem("------Select Work Order------", "0"));
        }
    }

    public void Bind_Role_VendorName(string selectedWorkOrderRefID)
    {
        DataTable WorkOrderDT = getWorkOrderDetails(selectedWorkOrderRefID); // wo ref id

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT v.vName, v.RefID FROM VendorMaster874 as v INNER JOIN WorkOrder874 AS w ON v.RefID = w.woVendor AND w.RefID = @RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", WorkOrderDT.Rows[0]["RefID"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddVender.DataSource = dt;
            ddVender.DataTextField = "vName";
            ddVender.DataValueField = "RefID";
            ddVender.DataBind();
            ddVender.Items.Insert(0, new ListItem("------Select Vendor------", "0"));
        }
    }

    //=============================={ Drop Down Selected Event }============================================

    protected void ddCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedCategoryRefID = ddCat.SelectedValue;

        if (ddCat.SelectedValue != "0")
        {
            Bind_Role_SubCategory(selectedCategoryRefID);
        }
        else
        {
            gridEmbDiv.Visible = false;

            // Clear the values of follwing dropdowns on the server side
            ddSubCat.Items.Clear();
            ddProjectMaster.Items.Clear();
            ddWorkOrder.Items.Clear();
            ddVender.Items.Clear();
            //ddWorkOrder.Items.Insert(0, new ListItem("------Select Vendor------", "0"));

            // Clear the values of ddWorkOrder & ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearSubCategoryDropdown", "ClearSubCategoryDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearProjectDropdown", "ClearProjectDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearWorkOrderDropdown", "ClearWorkOrderDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "clearVendorDropdown();", true);
        }
    }

    protected void ddSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        // RefIDs of Category & Sub Category
        selectedCategoryRefID = ddCat.SelectedValue;
        selectedSubCategoryRefID = ddSubCat.SelectedValue;

        if (ddSubCat.SelectedValue != "0")
        {
            Bind_Role_ProjectMaster(selectedCategoryRefID, selectedSubCategoryRefID);
        }
        else
        {
            gridEmbDiv.Visible = false;

            // Clear the values of follwing dropdowns on the server side
            ddProjectMaster.Items.Clear();
            ddWorkOrder.Items.Clear();
            ddVender.Items.Clear();

            // Clear the values of ddWorkOrder & ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearProjectDropdown", "ClearProjectDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearWorkOrderDropdown", "ClearWorkOrderDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "clearVendorDropdown();", true);
        }
    }

    protected void ddProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProjectMasterRefID = ddProjectMaster.SelectedValue; // Project Master RefID

        if (ddProjectMaster.SelectedValue != "0")
        {
            Bind_Role_WorkOrderDetails(selectedProjectMasterRefID);
        }
        else
        {
            gridEmbDiv.Visible = false;

            // Clear the values of follwing dropdowns on the server side
            ddWorkOrder.Items.Clear();
            ddVender.Items.Clear();

            // Clear the values of ddWorkOrder & ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearWorkOrderDropdown", "ClearWorkOrderDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "clearVendorDropdown();", true);
        }
    }

    protected void ddAODetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProjectMasterRefID = ddProjectMaster.SelectedValue; // Project Code
        //selectedAODetails = ddAODetails.SelectedValue; // AO No.

        //Bind_Role_WorkOrderDetails(selectedProjectMasterRefID, selectedAODetails);
    }

    protected void ddWorkOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProjectMasterRefID = ddProjectMaster.SelectedValue; // Project ref id
        selectedWorkOrderRefID = ddWorkOrder.SelectedValue; // WO RefID

        if (ddWorkOrder.SelectedValue != "0")
        {
            Bind_Role_VendorName(selectedWorkOrderRefID);

            // checking if there is only one vendor
            // assuming the first item is "------Select Vendor------" i.e. 0
            if (ddVender.Items.Count == 2)
            {
                ddVender.SelectedIndex = 1;
            }
            else
            {
                ddVender.SelectedIndex = 0;
            }

            FillGridViewWithBoqDetails(selectedProjectMasterRefID, selectedWorkOrderRefID);
        }
        else
        {
            gridEmbDiv.Visible = false;

            // Clear the values of ddVender on the server side
            ddVender.Items.Clear();

            // Clear the values of ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "clearVendorDropdown();", true);
        }
    }

    protected void ddVender_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //=============================={ Fetching Data }============================================

    private DataTable getCategory(string selectedCategoryRefIDRefId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM Caty874 where RefId = @RefId";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefId", selectedCategoryRefIDRefId.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getSubCategory(string selectedSubCategoryRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM SubCaty874 where RefId = @RefId";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefId", selectedSubCategoryRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getProjectMaster(string selectedProjectMasterRefIDCode)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM ProjectMaster874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", selectedProjectMasterRefIDCode.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getAODetails(string selectedAONo)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM AODetails874 where AONo=@AONo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AONo", selectedAONo.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getWorkOrderDetails(string selectedWorkOrderRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM WorkOrder874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", selectedWorkOrderRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getVendorDetails(string selectedVendorRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM VendorMaster874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", selectedVendorRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    //=============================={ Fill BoQ Grid Records }============================================

    protected void GridDyanmic_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            // Set the row in edit mode
            e.Row.RowState = e.Row.RowState ^ DataControlRowState.Edit;
        }
    }

    private void FillGridViewWithBoqDetails(string selectedProjectMasterRefID, string selectedWorkOrderRefID)
    {
        DataTable projectMasterDt = getProjectMaster(selectedProjectMasterRefID); // project ref id
        DataTable WorkOrderDT = getWorkOrderDetails(selectedWorkOrderRefID); // wo ref id

        // assign work order amount
        int woAmount = Convert.ToInt32(WorkOrderDT.Rows[0]["woTendrValue"]);
        //txtWoAmnt.Text = "₹ " + woAmount.ToString("N0");
        txtWoAmnt.Text = woAmount.ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM UploadBOQ874 WHERE BOQProj=@BOQProj and BOQwoTendrDetail=@BOQwoTendrDetail and BOQwoTendrValue=@BOQwoTendrValue";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@BOQProj", projectMasterDt.Rows[0]["RefID"].ToString());
            cmd.Parameters.AddWithValue("@BOQwoTendrDetail", WorkOrderDT.Rows[0]["RefID"].ToString());
            cmd.Parameters.AddWithValue("@BOQwoTendrValue", WorkOrderDT.Rows[0]["RefID"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable boqDT = new DataTable();
            Session["BoQDT"] = boqDT;
            ad.Fill(boqDT);
            con.Close();

            if (boqDT.Rows.Count > 0)
            {
                gridEmbDiv.Visible = true;

                gridDynamicBOQ.DataSource = boqDT;
                gridDynamicBOQ.DataBind();
            }
            else
            {
                // alert pop-up with only message
                string message = "No BOQ Data Found";
                string script = $"alert('{message}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
            }
        }
    }

    //=============================={ Calculate Basic Amount }============================================

    protected void btnBasicAmount_Click(object sender, EventArgs e)
    {
        string script = "showAlert('Success', 'Data inserted successfully!', 'success');";
        ScriptManager.RegisterStartupScript(this, GetType(), "SuccessScript", script, true);

        DataTable dt = (DataTable)Session["BoQDT"];

        int basicAmount = 0;

        if (dt != null)
        {
            // Iterate through each row in the GridView
            foreach (GridViewRow row in gridDynamicBOQ.Rows)
            {
                TextBox boqQty = row.FindControl("QtyMeasure") as TextBox;
                int qtyMeasuredValue = Convert.ToInt32(boqQty.Text);

                int rowIndex = row.RowIndex;
                int boqUnitRate = Convert.ToInt32(dt.Rows[rowIndex]["BoqRate"]);

                if (qtyMeasuredValue != 0 && boqUnitRate != 0)
                {
                    int prod = (qtyMeasuredValue * boqUnitRate);

                    // Perform operations with the value
                    basicAmount = basicAmount + prod;
                }
                // You can break the loop if you only need the value from the first row
                //break;
            }

            string basicAmountStr = basicAmount.ToString("N0");

            txtBasicAmt.CssClass = "form-control fw-normal border border-2";
            txtBasicAmt.Text = basicAmountStr;

            btnSubmitBasicAmount.Enabled = true;
        }
    }

    //=============================={ Submit Button Event }============================================

    protected void btnSubmitBasicAmount_Click(object sender, EventArgs e)
    {
        // inserting EMB header info
        InsertEmbHeader();
        btnSubmitBasicAmount.Enabled = false;
    }

    private void InsertEmbHeader()
    {
        DataTable dt = (DataTable)Session["BoQDT"];

        bool isPositive = true;
        bool someBoqQtyMeasured = false;

        // pre loop to check for any negative boq qty measured & at least one boq qty measured
        foreach (GridViewRow row in gridDynamicBOQ.Rows)
        {
            TextBox qtyMeasured = row.FindControl("QtyMeasure") as TextBox;
            double boqQtyMeasured = Convert.ToDouble(qtyMeasured.Text);

            if (boqQtyMeasured < 0.00 && boqQtyMeasured < 0)
            {
                isPositive = false; // negative boq entry
            }

            if (boqQtyMeasured > 0.00 && boqQtyMeasured > 0)
            {
                someBoqQtyMeasured = true;
            }
        }

        // once filteration is done, checking the results
        if (isPositive)
        {
            if (someBoqQtyMeasured)
            {
                // selected values
                selectedCategoryRefID = ddCat.SelectedValue; // Category RefID
                selectedSubCategoryRefID = ddSubCat.SelectedValue; // Sub Categoryred RefID
                selectedProjectMasterRefID = ddProjectMaster.SelectedValue; // Project Master RefID
                selectedWorkOrderRefID = ddWorkOrder.SelectedValue; // Work Order RefID
                selectedVendorRefID = ddVender.SelectedValue; // Vendor RefID
                selectedWorkOrderAmount = Convert.ToInt32(txtWoAmnt.Text.ToString());

                // DataTables
                DataTable categoryDt = getCategory(selectedCategoryRefID); // Ref ID
                DataTable subCategoryDt = getSubCategory(selectedSubCategoryRefID); // Ref Id
                DataTable projectMasterDt = getProjectMaster(selectedProjectMasterRefID); // Ref Id
                DataTable workOrderDt = getWorkOrderDetails(selectedWorkOrderRefID); // Ref Id
                DataTable vendorDt = getVendorDetails(selectedVendorRefID); // Ref Id

                // selected Dates
                //DateTime? abstractDate = !String.IsNullOrEmpty(dateAbstract.Text) ? DateTime.Parse(dateAbstract.Text) : (DateTime?)null;
                DateTime abstractDate = DateTime.Parse(dateAbstract.Text);
                DateTime measuredFromDate = DateTime.Parse(dateMeasuredFrom.Text);
                DateTime measuredToDate = DateTime.Parse(dateMeasuredTo.Text);

                // check for RefID
                int RefID = GetRefIDFromEmbMasterTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "INSERT INTO EmbMaster874 " +
                                 "(EmbCat, EmbSubCat, EmbPM, EmbWO, EmbVenN, EmbWOAmnt, EmbPreRAAmnt, EmbRemarks, EmbAbstractDt, EmbMeaFromDt, EmbMeaToDt, EmbMasRefId, RefID) " +
                                 "VALUES " +
                                 "(@EmbCat, @EmbSubCat, @EmbPM, @EmbWO, @EmbVenN, @EmbWOAmnt, @EmbPreRAAmnt, @EmbRemarks, @EmbAbstractDt, @EmbMeaFromDt, @EmbMeaToDt, @EmbMasRefId, @RefID) " +
                                 "SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@EmbCat", categoryDt.Rows[0]["CatyName"].ToString());
                    cmd.Parameters.AddWithValue("@EmbSubCat", subCategoryDt.Rows[0]["SubCaty"].ToString());
                    cmd.Parameters.AddWithValue("@EmbPM", projectMasterDt.Rows[0]["ProjectName"].ToString());
                    cmd.Parameters.AddWithValue("@EmbWO", workOrderDt.Rows[0]["woTendrNo"].ToString());
                    cmd.Parameters.AddWithValue("@EmbVenN", vendorDt.Rows[0]["vName"].ToString()); // **
                    cmd.Parameters.AddWithValue("@EmbWOAmnt", Convert.ToInt32(workOrderDt.Rows[0]["woTendrValue"].ToString()));
                    cmd.Parameters.AddWithValue("@EmbPreRAAmnt", Convert.ToInt32(0)); // **
                    cmd.Parameters.AddWithValue("@EmbRemarks", txtRemarks.Value.ToString());
                    cmd.Parameters.AddWithValue("@EmbAbstractDt", abstractDate.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@EmbMeaFromDt", measuredFromDate.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@EmbMeaToDt", measuredToDate.ToString("dd-MM-yyyy"));
                    cmd.Parameters.AddWithValue("@EmbMasRefId", RefID.ToString());
                    cmd.Parameters.AddWithValue("@RefID", RefID.ToString());
                    //cmd.ExecuteNonQuery();

                    int headerId = Convert.ToInt32(cmd.ExecuteScalar()); // 
                    con.Close();

                    // inserting EMB details
                    InsertEmbDetails(RefID);
                }
            }
            else
            {
                // sweet alert - error only block
                getSweetAlertErrorMandatory("Oops!", "Measure atleast one BOQ quantity to insert record !");
            }
        }
        else
        {
            // sweet alert - error only block
            getSweetAlertErrorMandatory("Oops!", "Please add positive BOQ quantity only!");
        }
    }

    private void InsertEmbDetails(int headerId)
    {
        DataTable dt = (DataTable)Session["BoQDT"];

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (GridViewRow row in gridDynamicBOQ.Rows)
                {
                    // to get the current row index
                    int rowIndex = row.RowIndex;

                    // current uploaded boq index, to be used for updating the original value
                    string currentBoQRefID = dt.Rows[rowIndex]["RefID"].ToString();

                    string boqItemName = dt.Rows[rowIndex]["BoqItem"].ToString();
                    string uom = dt.Rows[rowIndex]["BoqUOM"].ToString();
                    double boqQty = Convert.ToDouble(dt.Rows[rowIndex]["BoqQty"]);
                    double boqPendingQty = Convert.ToDouble(dt.Rows[rowIndex]["BoqPenQty"]);

                    TextBox qtyMeasured = row.FindControl("QtyMeasure") as TextBox;
                    double boqQtyMeasured = Convert.ToDouble(qtyMeasured.Text);

                    //============{ Performing Calculations }================

                    if (boqPendingQty != 0.00)
                    {
                        // for already used calculations
                        boqPendingQty = boqPendingQty - boqQtyMeasured;
                    }
                    else
                    {
                        // for fresh calcualtions
                        boqPendingQty = boqQty - boqQtyMeasured;
                    }

                    double boqQtyDiff = boqQty - (boqQty - boqQtyMeasured);

                    double boquptoPreRaQty = Convert.ToDouble(dt.Rows[rowIndex]["UptoPreRaQty"]);

                    double boqQtyRate = Convert.ToDouble(dt.Rows[rowIndex]["BoqRate"]);

                    double calculatedBasicAmount = Convert.ToDouble(txtBasicAmt.Text);

                    // condition to insert only BoQ qty measured - greater than zero
                    if (boqQtyMeasured > 0.00 && boqQtyMeasured > 0)
                    {
                        //============{ updating original uploaded boq }================

                        UpdateOriginalUploadedBoq(currentBoQRefID, boqPendingQty);

                        //============{ inserting emb details }================

                        string sql = "INSERT INTO EmbRecords874" +
                                     "(EmbHeaderId, BoQItemName, BoQUOM, BoqQty, BoqPenQty, BoQItemRate, BoqQtyMeas, BoQUptoPreRaQty, BoqQtyDIff, BasicAmount)" +
                                     "VALUES" +
                                     "(@EmbHeaderId, @BoQItemName, @BoQUOM, @BoqQty, @BoqPenQty, @BoQItemRate, @BoqQtyMeas, @BoQUptoPreRaQty, @BoqQtyDIff, @BasicAmount)";

                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@EmbHeaderId", Convert.ToInt32(headerId));
                        cmd.Parameters.AddWithValue("@BoQItemName", boqItemName.ToString());
                        cmd.Parameters.AddWithValue("@BoQUOM", uom.ToString());
                        cmd.Parameters.AddWithValue("@BoqQty", boqQty);
                        cmd.Parameters.AddWithValue("@BoqPenQty", boqPendingQty);
                        cmd.Parameters.AddWithValue("@BoQItemRate", boqQtyRate);
                        cmd.Parameters.AddWithValue("@BoqQtyMeas", boqQtyMeasured);
                        cmd.Parameters.AddWithValue("@BoQUptoPreRaQty", boquptoPreRaQty);
                        cmd.Parameters.AddWithValue("@BoqQtyDIff", boqQtyDiff);
                        cmd.Parameters.AddWithValue("@BasicAmount", calculatedBasicAmount);
                        cmd.ExecuteNonQuery();
                    }
                }

                // sweet - success & redirect
                getSweetAlertSuccessRedirectMandatory("Saved!", "Record saved successfully!", "Update/UpdateEMB.aspx");

                con.Close();
            }
        }
        catch (Exception ex)
        {
            string message1 = "Exception while inserting EMB Details";
            string script1 = $"alert('{message1}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script1, true);
        }
    }

    private void UpdateOriginalUploadedBoq(string currentBoQRefID, double boqPendingQty)
    {
        //double boqPendingQtyNew;
        //if (currentBoQRefID == "1000009") { boqPendingQtyNew = 100; }
        //else { boqPendingQtyNew = 12; }

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "UPDATE UploadBOQ874 SET BoqPenQty = @BoqPenQty WHERE RefID = @RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@BoqPenQty", boqPendingQty);
            cmd.Parameters.AddWithValue("@RefID", currentBoQRefID.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    private int GetRefIDFromEmbMasterTable()
    {
        string nextRefID = "1000001";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT ISNULL(MAX(CAST(RefID AS INT)), 1000000) + 1 AS NextRefID FROM EmbMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                //nextRefID = (Convert.ToInt32(result) + 1).ToString(); // Convert to int, increment, and convert back to string
                nextRefID = result.ToString();
            }

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return Convert.ToInt32(nextRefID);
        }
    }
}