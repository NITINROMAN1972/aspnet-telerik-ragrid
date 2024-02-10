using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Telerik_Reporting_TelerikReport : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void RadGridID_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        // data source for the Telerik Grid
        BindGrid();
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select id, SlotID, SlDate, SlTime1, SlTime2, SlCount, SlRole, SlDist, SlSavedOn from Slot867";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            RadGridID.DataSource = dt;
        }
    }

    protected void RadGridID_InsertCommand(object sender, GridCommandEventArgs e)
    {
        // Web Forms Site event handler
        // Especially for inserting a record in Telerik RaGrid

        // Extracting inserted values
        GridEditableItem editedItem = e.Item as GridEditableItem;
        Hashtable newValues = new Hashtable();
        editedItem.ExtractValues(newValues);

        string employeeIDString = newValues["id"].ToString();
        if (int.TryParse(employeeIDString, out int SlotID))
        {
            // The conversion was successful, and 'SlotID' now contains the integer value.
            // You can use 'SlotID' in your code.
        }
        else
        {
            // Handle the case where the conversion failed. Log an error, set a default value, etc.
        }

        // Retriving inserted values from HashTable

        int slotID = Convert.ToInt32(newValues["id"]);

        string slDateString = newValues["SlDate"]?.ToString();
        DateTime? slDate = DateTime.TryParse(slDateString, out var parsedDate) ? parsedDate : (DateTime?)null;

        TimeSpan? SlTime1 = TimeSpan.TryParse(newValues["SlTime1"]?.ToString(), out var parseDate1) ? parseDate1 : TimeSpan.Zero;
        TimeSpan? SlTime2 = TimeSpan.TryParse(newValues["SlTime2"]?.ToString(), out var parseDate2) ? parseDate2 : TimeSpan.Zero;

        int slotCount = Convert.ToInt32(newValues["SlCount"]);
        string slotRole = newValues["SlRole"].ToString();
        string slotDistrict = newValues["SlDist"].ToString();

        DateTime? slotSavedOn = DateTime.TryParse(newValues["SlSavedOn"]?.ToString(), out var parseSavedOn) ? parseSavedOn : (DateTime?)null;

        // inserting data to table
        InsertData(slotID, slDate, SlTime1, SlTime2, slotCount, slotRole, slotDistrict, slotSavedOn);
    }

    private void InsertData(int slotID, DateTime? slDate, TimeSpan? SlTime1, TimeSpan? SlTime2, int slotCount, string slotRole, string slotDistrict, DateTime? slotSavedOn)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "insert into Slot867 (SlotID, SlDate, SlTime1, SlTime2, SlCount, SlRole, SlDist, SlSavedOn) " +
                         "values (@SlotID, @SlDate, @SlTime1, @SlTime2, @SlCount, @SlRole, @SlDist, @SlSavedOn)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@SlotID", slotID);
            cmd.Parameters.AddWithValue("@SlDate", slDate);
            cmd.Parameters.AddWithValue("@SlTime1", SlTime1);
            cmd.Parameters.AddWithValue("@SlTime2", SlTime2);
            cmd.Parameters.AddWithValue("@SlCount", slotCount);
            cmd.Parameters.AddWithValue("@SlRole", slotRole);
            cmd.Parameters.AddWithValue("@SlDist", slotDistrict);
            cmd.Parameters.AddWithValue("@SlSavedOn", slotSavedOn);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            RadGridID.DataSource = dt;
        }
    }

}