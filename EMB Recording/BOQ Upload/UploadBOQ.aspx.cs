using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Emp_Calculation_UploadBOQ : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
    private object worksheet;
    string selectedProjectMaster;
    string selectedAODetails;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Role_ProjectMaster();
        }
    }

    //=============================={ Bind Drop Downs }============================================

    public void Bind_Role_ProjectMaster()
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

            ddProjectMaster.DataSource = dt;
            ddProjectMaster.DataTextField = "ProjectName";
            ddProjectMaster.DataValueField = "RefID";
            ddProjectMaster.DataBind();
            ddProjectMaster.Items.Insert(0, new ListItem("------Select Project------", "0"));
        }
    }

    public void Bind_Role_AODetails(string selectedProjectMaster)
    {
        DataTable projectMasterDt = getProjectMaster(selectedProjectMaster); // project ref id

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            //string sql = "select * from AcceptAO874 where AccptAOCaty=@AccptAOCaty and AccptAOSubCaty=@AccptAOSubCaty and AccptAOProj=@AccptAOProj";
            string sql = "select * from AODetails874 where AOProject=@AOProject";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AOProject", projectMasterDt.Rows[0]["RefID"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddAODetails.DataSource = dt;
            ddAODetails.DataTextField = "AONo";
            ddAODetails.DataValueField = "RefID";
            ddAODetails.DataBind();
            ddAODetails.Items.Insert(0, new ListItem("------Select AO No.------", "0"));
        }
    }

    public void Bind_Role_WorkOrderDetails(string selectedProjectMaster, string selectedAODetails)
    {
        DataTable projectMasterDt = getProjectMaster(selectedProjectMaster); // project code
        DataTable AoDetailsDt = getAODetails(selectedAODetails); // ao no.

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from WorkOrder874 where woProject=@woProject and woApprvAO=@woApprvAO";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@woProject", projectMasterDt.Rows[0]["RefID"].ToString());
            cmd.Parameters.AddWithValue("@woApprvAO", AoDetailsDt.Rows[0]["RefID"].ToString());
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

            Bind_Role_TendorValue(dt);
        }
    }

    public void Bind_Role_TendorValue(DataTable workOrderDT)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from WorkOrder874 where woTendrNo=@woTendrNo";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@woTendrNo", workOrderDT.Rows[0]["woTendrNo"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddWoTendorValue.DataSource = dt;
            ddWoTendorValue.DataTextField = "woTendrValue";
            ddWoTendorValue.DataValueField = "woTendrValue";
            ddWoTendorValue.DataBind();
            ddWoTendorValue.Items.Insert(0, new ListItem("------Select Tendor Value------", "0"));

            Bind_Role_TendorValue(dt);
        }
    }

    //=============================={ Drop Down Selected Event }============================================

    protected void ddProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProjectMaster = ddProjectMaster.SelectedValue; // Project Code

        Bind_Role_AODetails(selectedProjectMaster);
    }

    protected void ddAODetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProjectMaster = ddProjectMaster.SelectedValue; // Project Code
        selectedAODetails = ddAODetails.SelectedValue; // AO No.

        Bind_Role_WorkOrderDetails(selectedProjectMaster, selectedAODetails);
    }

    protected void ddWorkOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddWoTendorValue_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //=============================={ Fetching Data }============================================

    private DataTable getProjectMaster(string selectedProjectMasterCode)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM ProjectMaster874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", selectedProjectMasterCode.ToString());
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
            string sql = "SELECT * FROM AODetails874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", selectedAONo.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    //=============================={ Basic Amount }============================================

    protected void btnExclUpload_Click(object sender, EventArgs e)
    {
        if (fileExcel.HasFile)
        {
            string FileExtension = System.IO.Path.GetExtension(fileExcel.FileName);

            if (FileExtension == ".xlsx" || FileExtension == ".xls")
            {
                string strFileName = DateTime.Now.Day.ToString() + '_' + DateTime.Now.Month.ToString() + '_' + DateTime.Now.Year.ToString() + '_' + DateTime.Now.Hour.ToString() + '_' +
                                     DateTime.Now.Minute.ToString() + '_' + fileExcel.FileName.ToString();

                string filePath = Server.MapPath("~/Upload/" + strFileName);
                //string filePath = Server.MapPath("~/Ginie/External?url=../Portal/BoQSave/" + strFileName);
                fileExcel.SaveAs(filePath);

                try
                {
                    using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        // Licence for Non-Commercial applications
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                        // Assuming the data is in the first worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        // Access data from the worksheet
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        DataTable dt = new DataTable();

                        // Assuming the first row contains column headers
                        for (int col = 1; col <= colCount; col++)
                        {
                            dt.Columns.Add(worksheet.Cells[1, col].Text);
                        }

                        // Starting from the second row to skip headers
                        for (int row = 2; row <= rowCount; row++)
                        {
                            DataRow dataRow = dt.NewRow();

                            for (int col = 1; col <= colCount; col++)
                            {
                                dataRow[col - 1] = worksheet.Cells[row, col].Text;
                            }

                            dt.Rows.Add(dataRow);
                        }

                        // Checking column names present in excel sheet or not
                        if (dt.Columns[0].ColumnName.Trim() == "BoQ_Item_Description" && dt.Columns[1].ColumnName.Trim() == "BoQ_Unit" && dt.Columns[2].ColumnName.Trim() == "BoQ_Qty" && dt.Columns[3].ColumnName.Trim() == "Rate_Unit")
                        {
                            if (dt.Rows.Count > 0)
                            {
                                // Method 1: delete data from Temp Table
                                // Method 2: inserting data into temp table if needed

                                InsertExcelDataToMainTable(dt);

                                // Method 3: To display the inserted data from Temp or Main Table
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    public DataTable InsertExcelDataToMainTable(DataTable dtItemInsert)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            DataTable dtEmp = new DataTable();

            foreach (DataRow row in dtItemInsert.Rows)
            {
                string sql = "insert into BoQ_Upload (BoQ_Item_Description, BoQ_Unit, BoQ_Qty, Rate_Unit) values (@BoQ_Item_Description, @BoQ_Unit, @BoQ_Qty, @Rate_Unit)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BoQ_Item_Description", row["BoQ_Item_Description"].ToString());
                    cmd.Parameters.AddWithValue("@BoQ_Unit", row["BoQ_Unit"].ToString());
                    cmd.Parameters.AddWithValue("@BoQ_Qty", row["BoQ_Qty"].ToString());
                    cmd.Parameters.AddWithValue("@Rate_Unit", row["Rate_Unit"].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
            return dtEmp;
        }
    }
}