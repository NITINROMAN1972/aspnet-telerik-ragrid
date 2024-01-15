using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Emp_Calculation_UpdateEMB : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    string projectRefID;
    string workOrderRefID;
    string vendorRefID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_EMBHeader();
            Bind_Role_Project();

            //BindDynamicGridView();

            //redirect with only message
            //string message = "vendor : " + vendorRefID;
            //string script = $"alert('{message}');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
        }
    }

    protected void BindDynamicGridView()
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from VendorMaster874";
            //string sql = "select * from VendorMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            con.Close();
        }

        if (dt.Rows.Count > 0)
        {
            // turning OFF column auto generation
            GridTest.AutoGenerateColumns = true;

            // assigning data source to GridView
            GridTest.DataSource = dt;
            GridTest.DataBind();

            // Clear existing columns
            GridTest.Columns.Clear();

            // Dynamically creating BoundFields or Columns using from the data source
            foreach (DataColumn col in dt.Columns)
            {
                BoundField boundField = new BoundField();
                boundField.DataField = col.ColumnName;
                boundField.HeaderText = col.ColumnName;
                GridTest.Columns.Add(boundField);
            }

            // turning ON column auto generation
            GridTest.AutoGenerateColumns = false;
        }
        else
        {
            //redirect with only message
            string message = "No records";
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
        }
    }

    private void Bind_EMBHeader()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from EmbMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            gridEMBHeader.DataSource = dt;
            gridEMBHeader.DataBind();
        }
    }

    //====================={ Drop Downs Bind }================================

    private void Bind_Role_Project()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ProjectMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddProject.DataSource = dt;
            ddProject.DataTextField = "ProjectName";
            ddProject.DataValueField = "RefID";
            ddProject.DataBind();
            ddProject.Items.Insert(0, new ListItem("------Select Project------", "0"));
        }
    }

    private void Bind_Role_WorkOrder(string projectRefID)
    {
        DataTable projectDT = getProject(projectRefID);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from WorkOrder874 where woProject = @woProject";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@woProject", projectDT.Rows[0]["RefID"].ToString());

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddWOName.DataSource = dt;
            ddWOName.DataTextField = "woTitle";
            ddWOName.DataValueField = "RefID";
            ddWOName.DataBind();
            ddWOName.Items.Insert(0, new ListItem("------Select Work Order------", "0"));
        }
    }

    private void Bind_Role_Vendor(string projectRefID, string workOrderRefID)
    {
        DataTable projectDT = getProject(projectRefID);
        DataTable workOrderDT = getWorkOrder(workOrderRefID);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from VendorMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddVendorName.DataSource = dt;
            ddVendorName.DataTextField = "vName";
            ddVendorName.DataValueField = "RefID";
            ddVendorName.DataBind();
            ddVendorName.Items.Insert(0, new ListItem("------Select Vendor------", "0"));
        }
    }

    //====================={ Drop Down Events }================================

    protected void ddProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        projectRefID = ddProject.SelectedValue;

        if (ddProject.SelectedValue != "0")
        {
            Bind_Role_WorkOrder(projectRefID);
        }
        else
        {
            gridEmbDiv.Visible = false;

            // Clear the values of follwing dropdowns on the server side
            ddWOName.Items.Clear();
            ddVendorName.Items.Clear();
            //ddWorkOrder.Items.Insert(0, new ListItem("------Select Vendor------", "0"));

            // Clear the values of ddWorkOrder & ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearWorkOrderDropdown", "ClearWorkOrderDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "ClearVendorDropdown();", true);
        }
    }

    protected void ddWOName_SelectedIndexChanged(object sender, EventArgs e)
    {
        projectRefID = ddProject.SelectedValue;
        workOrderRefID = ddWOName.SelectedValue;

        if (ddWOName.SelectedValue != "0")
        {
            Bind_Role_Vendor(projectRefID, workOrderRefID);
        }
        else
        {
            gridEmbDiv.Visible = false;

            // Clear the values of follwing dropdowns on the server side
            ddVendorName.Items.Clear();

            // Clear the values of ddWorkOrder & ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "ClearVendorDropdown();", true);
        }
    }

    protected void ddVendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        projectRefID = ddProject.SelectedValue;
        workOrderRefID = ddWOName.SelectedValue;
        vendorRefID = ddVendorName.SelectedValue;
    }

    //====================={ EMB Update }================================

    protected void GrdUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lnkView")
        {
            int rowId = Convert.ToInt32(e.CommandArgument);
            Session["RowID"] = rowId;

            gridEmbDiv.Visible = false;
            divEMBUpdate.Visible = true;
            embDetailsUpdate.Visible = true;

            divTopSearch.Visible = false;
            btnSubmitBasicAmount.Enabled = true;

            // fill EMB header
            UpdateEmbCaculations(rowId);

            // fill EMB Details
            updateEmbDetails(rowId);
        }
    }

    private void UpdateEmbCaculations(int rowId)
    {
        // fetching EMB header
        DataTable embHeaderDT = getEmbHeader(rowId);

        if (embHeaderDT.Rows.Count > 0)
        {
            ddCat.Text = embHeaderDT.Rows[0]["EmbCat"].ToString();
            ddSubCat.Text = embHeaderDT.Rows[0]["EmbSubCat"].ToString();
            ddProjectMaster.Text = embHeaderDT.Rows[0]["EmbPM"].ToString();
            //ddAODetails.Text = embHeaderDT.Rows[0]["EmbAO"].ToString();
            ddWorkOrder.Text = embHeaderDT.Rows[0]["EmbWO"].ToString();
            ddVender.Text = embHeaderDT.Rows[0]["EmbVenN"].ToString();
            txtWoAmnt.Text = embHeaderDT.Rows[0]["EmbWOAmnt"].ToString();
            txtUpToTotalRaAmnt.Text = embHeaderDT.Rows[0]["EmbPreRAAmnt"].ToString();
            txtRemarks.Value = embHeaderDT.Rows[0]["EmbRemarks"].ToString();

            DateTime abstractDate = DateTime.Parse(embHeaderDT.Rows[0]["EmbAbstractDt"].ToString());
            DateTime measFrom = DateTime.Parse(embHeaderDT.Rows[0]["EmbMeaFromDt"].ToString());
            DateTime measTo = DateTime.Parse(embHeaderDT.Rows[0]["EmbMeaToDt"].ToString());

            dateAbstract.Text = abstractDate.ToString("dd-MM-yyyy");
            dateMeasuredFrom.Text = measFrom.ToString("dd-MM-yyyy");
            dateMeasuredTo.Text = measTo.ToString("dd-MM-yyyy");
        }
        else
        {
            string message = "No EMB Header data Found !";
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
        }
    }

    private void updateEmbDetails(int rowId)
    {
        // fetching EMB details
        DataTable dt = getEmbDetails(rowId);
        Session["BoQDTUpdate"] = dt;

        if (dt.Rows.Count > 0)
        {
            txtBasicAmt.Text = dt.Rows[0]["BasicAmount"].ToString();

            gridDynamicBOQ.DataSource = dt;
            gridDynamicBOQ.DataBind();
        }
        else
        {
            string message1 = "No EMB Details Found !";
            string script1 = $"alert('{message1}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script1, true);
        }
    }

    //====================={ Fetching Data }================================

    private DataTable getProject(string projectRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM ProjectMaster874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", projectRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getWorkOrder(string workOrderRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM WorkOrder874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", workOrderRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getVendor(string vendorRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM VendorMaster874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", vendorRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getSearchedEmbHeader(string project, string workOrder, string vendor)
    {
        DataTable dt = new DataTable();

        if (project != "" && workOrder != "" && vendor != "")
        {
            DataTable projectDT = getProject(project); // project dt
            DataTable workOrderDT = getWorkOrder(workOrder); // work order dt
            DataTable vendorDT = getVendor(vendor); // vendor dt

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM EmbMaster874 where EmbPM = @EmbPM and EmbWO = @EmbWO and EmbVenN = @EmbVenN";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmbPM", projectDT.Rows[0]["ProjectName"].ToString());
                cmd.Parameters.AddWithValue("@EmbWO", workOrderDT.Rows[0]["woTendrNo"].ToString());
                cmd.Parameters.AddWithValue("@EmbVenN", vendorDT.Rows[0]["vName"].ToString());
                cmd.ExecuteNonQuery();

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
            }
        }
        else if (project != "" && workOrder != "")
        {
            DataTable projectDT = getProject(project); // project dt
            DataTable workOrderDT = getWorkOrder(workOrder); // work order dt

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM EmbMaster874 where EmbPM = @EmbPM and EmbWO = @EmbWO";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmbPM", projectDT.Rows[0]["ProjectName"].ToString());
                cmd.Parameters.AddWithValue("@EmbWO", workOrderDT.Rows[0]["woTendrNo"].ToString());
                cmd.ExecuteNonQuery();

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
            }
        }
        else if (project != "")
        {
            DataTable projectDT = getProject(project); // project dt

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM EmbMaster874 where EmbPM = @EmbPM";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmbPM", projectDT.Rows[0]["ProjectName"].ToString());
                cmd.ExecuteNonQuery();

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
            }
        }

        return dt;
    }

    protected void GridDyanmic_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            // Set the row in edit mode
            e.Row.RowState = e.Row.RowState ^ DataControlRowState.Edit;
        }
    }

    protected void GridEmbHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //binding GridView to PageIndex object
        gridEMBHeader.PageIndex = e.NewPageIndex;
        Bind_EMBHeader();
    }

    private DataTable getEmbHeader(int rowId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM EmbMaster874 where EmbMasRefId=@EmbMasRefId";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EmbMasRefId", rowId.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getEmbDetails(int rowId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            //string sql = "SELECT BoQItemName, BoqUOM, BoqQty, BoqPenQty, BoQUptoPreRaQty, BoqQtyDIff, BoQItemRate, BasicAmount FROM EmbRecords874 WHERE EmbHeaderId=@EmbHeaderId";
            string sql = "SELECT BoQItemName, BoqUOM, BoqQty, BoqPenQty, BoqQtyMeas, BoQUptoPreRaQty, BoqQtyDIff, BoQItemRate, BasicAmount FROM EmbRecords874 WHERE EmbHeaderId=@EmbHeaderId";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EmbHeaderId", rowId.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            // Adding a new column "BoqQtyMeas" and set its value to 0.00 for each row
            //DataColumn BoqQtyMeas = new DataColumn("BoqQtyMeas", typeof(decimal));
            //BoqQtyMeas.DefaultValue = 0.00m;
            //dt.Columns.Add(BoqQtyMeas);

            con.Close();

            return dt;
        }
    }

    //====================={ Button Events }================================

    protected void btnTruncate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            string sql = "truncate table EmbMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            string sql1 = "truncate table EmbRecords874";
            SqlCommand cmd1 = new SqlCommand(sql1, con);
            cmd1.ExecuteNonQuery();


            //==================================================================================
            string sq2 = "UPDATE UploadBOQ874 SET BoqPenQty = @BoqPenQty WHERE RefID = @RefID";
            SqlCommand cm2 = new SqlCommand(sq2, con);
            cm2.Parameters.AddWithValue("@BoqPenQty", 100.00);
            cm2.Parameters.AddWithValue("@RefID", Convert.ToInt32("1000009"));
            cm2.ExecuteNonQuery();

            string sql3 = "UPDATE UploadBOQ874 SET BoqPenQty = @BoqPenQty WHERE RefID = @RefID";
            SqlCommand cmd3 = new SqlCommand(sql3, con);
            cmd3.Parameters.AddWithValue("@BoqPenQty", 12.00);
            cmd3.Parameters.AddWithValue("@RefID", Convert.ToInt32("1000010"));
            cmd3.ExecuteNonQuery();

            string sql4 = "UPDATE UploadBOQ874 SET BoqPenQty = @BoqPenQty WHERE RefID = @RefID";
            SqlCommand cmd4 = new SqlCommand(sql4, con);
            cmd4.Parameters.AddWithValue("@BoqPenQty", 52.00);
            cmd4.Parameters.AddWithValue("@RefID", Convert.ToInt32("1000013"));
            cmd4.ExecuteNonQuery();

            con.Close();
        }

        string message = "All EMB related tables truncated !";
        string script = $"alert('{message}');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
    }

    protected void btnNewEmb_Click(object sender, EventArgs e)
    {
        Response.Redirect("../EmbRecording.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();

        //using (SqlConnection con = new SqlConnection(connectionString))
        //{
        //    con.Open();
        //    string sql = "TRUNCATE TABLE EmbMaster874";
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    cmd.ExecuteNonQuery();

        //    string sql1 = "TRUNCATE TABLE EmbRecords874";
        //    SqlCommand cmd1 = new SqlCommand(sql1, con);
        //    cmd1.ExecuteNonQuery();
        //    con.Close();
        //}
    }

    private void BindGridView()
    {
        DataTable searchedEmbDT;

        if (ddProject.SelectedValue.ToString() == "0" && string.IsNullOrEmpty(ddWOName.SelectedValue) && string.IsNullOrEmpty(ddVendorName.SelectedValue))
        {
            gridEmbDiv.Visible = true;
        }
        else if (ddProject.SelectedValue.ToString() != "0" && ddWOName.SelectedValue.ToString() == "0" && string.IsNullOrEmpty(ddVendorName.SelectedValue))
        {
            // only project is selceted
            projectRefID = ddProject.SelectedValue; // Ref ID

            searchedEmbDT = getSearchedEmbHeader(projectRefID, "", "");

            gridEmbDiv.Visible = true;

            // binding the grid
            gridEMBHeader.DataSource = searchedEmbDT;
            gridEMBHeader.DataBind();
        }
        else if (ddProject.SelectedValue.ToString() != "0" && ddWOName.SelectedValue.ToString() != "0" && ddVendorName.SelectedValue.ToString() == "0")
        {
            // only project and work order are selceted

            projectRefID = ddProject.SelectedValue; // Ref ID
            workOrderRefID = ddWOName.SelectedValue; // Ref ID

            searchedEmbDT = getSearchedEmbHeader(projectRefID, workOrderRefID, "");

            gridEmbDiv.Visible = true;

            // binding the grid
            gridEMBHeader.DataSource = searchedEmbDT;
            gridEMBHeader.DataBind();
        }
        else if (ddProject.SelectedValue.ToString() != "0" && ddWOName.SelectedValue.ToString() != "0" && ddVendorName.SelectedValue.ToString() != "0")
        {
            // project, work order and vendor are selceted

            projectRefID = ddProject.SelectedValue; // Ref ID
            workOrderRefID = ddWOName.SelectedValue; // Ref ID
            vendorRefID = ddVendorName.SelectedValue; // Ref ID

            searchedEmbDT = getSearchedEmbHeader(projectRefID, workOrderRefID, vendorRefID);

            gridEmbDiv.Visible = true;

            // binding the grid
            gridEMBHeader.DataSource = searchedEmbDT;
            gridEMBHeader.DataBind();
        }
        else
        {
            //redirect with only message
            string message = "Please select properly !";
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
        }
    }

    protected void btnBasicAmount_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["BoQDTUpdate"];

        double basicAmount = 0;

        if (dt != null)
        {
            // Iterate through each row in the GridView
            foreach (GridViewRow row in gridDynamicBOQ.Rows)
            {
                TextBox boqQty = row.FindControl("BoqQtyMeas") as TextBox;
                double qtyMeasuredValue = Convert.ToDouble(boqQty.Text);

                int rowIndex = row.RowIndex;
                double boqUnitRate = Convert.ToDouble(dt.Rows[rowIndex]["BoQItemRate"]);

                if (qtyMeasuredValue != 0.00 && boqUnitRate != 0.00)
                {
                    double prod = (qtyMeasuredValue * boqUnitRate);

                    // Perform operations with the value
                    basicAmount = basicAmount + prod;
                }

                // You can break the loop if you only need the value from the first row
                //break;
            }

            btnSubmitBasicAmount.Enabled = true;

            string basicAmountStr = basicAmount.ToString("N0");

            txtBasicAmt.CssClass = "form-control fw-normal border border-2";
            txtBasicAmt.Text = basicAmountStr;
        }
    }

    protected void btnSubmitBasicAmount_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["BoQDTUpdate"];
        DataTable embRecordsDT = getEmbRecordsByHeaderId(Session["RowID"].ToString());

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (GridViewRow row in gridDynamicBOQ.Rows)
                {
                    // to get the current row index
                    int rowIndex = row.RowIndex;

                    double boqQty = Convert.ToDouble(dt.Rows[rowIndex]["BoqQty"]);

                    double calculatedBasicAmount = Convert.ToDouble(txtBasicAmt.Text);

                    //============{ Performing Calculations }================

                    TextBox qtyMeasured = row.FindControl("BoqQtyMeas") as TextBox;
                    double boqQtyMeasured = Convert.ToDouble(qtyMeasured.Text);

                    double oldBoqPendingQty = Convert.ToDouble(dt.Rows[rowIndex]["BoqPenQty"]);
                    double boqPendingQty = oldBoqPendingQty - boqQtyMeasured;

                    double boqQtyDiff = boqQty - (oldBoqPendingQty - boqQtyMeasured);

                    //============{ inserting using sql }================

                    string sql = "UPDATE EmbRecords874 " +
                                 "SET " +
                                 "BoqPenQty=@BoqPenQty, BoqQtyMeas=@BoqQtyMeas,  BoqQtyDIff=@BoqQtyDIff,  BasicAmount=@BasicAmount " +
                                 "WHERE ID=@ID";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@BoqPenQty", boqPendingQty);
                    cmd.Parameters.AddWithValue("@BoqQtyMeas", boqQtyMeasured);
                    cmd.Parameters.AddWithValue("@BoqQtyDIff", boqQtyDiff);
                    cmd.Parameters.AddWithValue("@BasicAmount", calculatedBasicAmount);
                    cmd.Parameters.AddWithValue("@ID", embRecordsDT.Rows[rowIndex]["ID"].ToString());
                    cmd.ExecuteNonQuery();
                }

                con.Close();

                btnSubmitBasicAmount.Enabled = false;

                //redirect with only message
                string message = "EMB Recordings Updated Successfully !";
                string href = "UpdateEMB.aspx";
                string script = $"alert('{message}');location.href = '{href}';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
            }
        }
        catch (Exception ex)
        {
            string message1 = "Exception while inserting EMB Details";
            string script1 = $"alert('{message1}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script1, true);
        }
    }

    private DataTable getEmbRecordsByHeaderId(string EmbHeaderId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM EmbRecords874 WHERE EmbHeaderId = @EmbHeaderId ORDER BY ID ASC";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EmbHeaderId", EmbHeaderId);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }
}