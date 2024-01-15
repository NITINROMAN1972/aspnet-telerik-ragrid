using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void Radgrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        BindGrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        // Retrieve connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = @"SELECT
                            a.ApelRefNo,
                            a.DtofFiiliApp,
                            a.DtFilliAffApp,
                            t.CourtName AS ApelCourtName,
                            b.CourtBenchName,
                            a.APOaNo,
                            a.APOaDte,
                            p.CourtName AS ApelCourtName2
                        FROM
                            Appeal864 AS a
                        INNER JOIN
                            TypeCourt864 AS t ON a.ApelCourt = t.CourtID
                        INNER JOIN
                            CourtBench864 AS b ON a.ApelTribunl = b.CourtBenchID
                        INNER JOIN
                            TypeCourt864 AS p ON a.ApelCourt = p.CourtID
                        INNER JOIN
                            CaseCreation864 AS m ON m.OANo = a.APOaNo
                        WHERE 1 = 1";

            // Check if APOaNo is provided for filtering
            //if (!string.IsNullOrEmpty(TxtOANo.Text.Trim()))
            //{
            //    sql += " AND  APOaNo = @APOaNo";
            //}

            // Check if CourtName is provided for filtering
            //if (!string.IsNullOrEmpty(TxtCourtName.Text.Trim()))
            //{
            //    sql += " AND ApelCourtName LIKE @CourtName";
            //}

            SqlCommand cmd = new SqlCommand(sql, con);

            // Add parameters for APOaNo filtering
            //if (!string.IsNullOrEmpty(TxtOANo.Text.Trim()))
            //{
            //    cmd.Parameters.AddWithValue("@APOaNo", TxtOANo.Text.Trim());
            //}

            // Add parameters for CourtName filtering
            //if (!string.IsNullOrEmpty(TxtCourtName.Text.Trim()))
            //{
            //    cmd.Parameters.AddWithValue("@CourtName", "%" + TxtCourtName.Text.Trim() + "%");
            //}

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            Radgrid1.DataSource = dt;
            //Radgrid1.Rebind();
        }
    }


    //private void BindGrid()
    //{
    //    // Retrieve connection string from Web.config
    //    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        con.Open();
    //        string sql = "SELECT * FROM Employee WHERE 1 = 1";

    //        // Check if Employee ID is provided for filtering
    //        if (!string.IsNullOrEmpty(TxtOANo.Text.Trim()))
    //        {
    //            sql += " AND  APOaNo = @EmployeeID";
    //        }

    //        if (!string.IsNullOrEmpty(TxtCourtName.Text.Trim()))
    //        {
    //            sql += " AND CourtName LIKE @FirstName";
    //        }

    //        SqlCommand cmd = new SqlCommand(sql, con);

    //        // Add parameters for Employee ID filtering
    //        if (!string.IsNullOrEmpty(TxtOANo.Text.Trim()))
    //        {
    //            cmd.Parameters.AddWithValue("@APOaNo", TxtOANo.Text.Trim());
    //        }


    //        // Add parameters for First Name filtering
    //        if (!string.IsNullOrEmpty(TxtCourtName.Text.Trim()))
    //        {
    //            cmd.Parameters.AddWithValue("@CourtName", "%" + TxtCourtName.Text.Trim() + "%");
    //        }

    //        SqlDataAdapter ad = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        ad.Fill(dt);
    //        con.Close();

    //        Radgrid1.DataSource = dt;
    //        Radgrid1.Rebind();

    //    }
    //}



    //private void BindGrid()
    //{
    //    // Retrieve connection string from Web.config
    //    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        con.Open();
    //        string sql = "SELECT * FROM Employee";
    //        SqlCommand cmd = new SqlCommand(sql, con);

    //        SqlDataAdapter ad = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        ad.Fill(dt);
    //        con.Close();

    //        Radgrid1.DataSource = dt;
    //    }
    //}

    
}
