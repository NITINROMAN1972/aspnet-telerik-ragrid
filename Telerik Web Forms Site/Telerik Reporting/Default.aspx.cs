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

    private void BindGrid()
    {
        // Retrieve connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM Employee";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            Radgrid1.DataSource = dt;
        }
    }

    protected void Radgrid1_InsertCommand(object sender, GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        Hashtable newValues = new Hashtable();
        editedItem.ExtractValues(newValues);
        string employeeIDString = newValues["EmployeeID"].ToString();

        if (int.TryParse(employeeIDString, out int employeeID))
        {
            // The conversion was successful, and 'employeeID' now contains the integer value.
            // You can use 'employeeID' in your code.
        }
        else
        {
            // Handle the case where the conversion failed. Log an error, set a default value, etc.
        }
        string employeeFirstName = newValues["FirstName"].ToString();
        string employeeLastName = newValues["LastName"].ToString();
        string EmployeeSalary = newValues["Salary"].ToString();
        string hireDateString = newValues["HireDate"].ToString();

        if (DateTime.TryParse(hireDateString, out DateTime HiringDate))
        {
            // The conversion was successful, and 'hiringDate' now contains the DateTime value.
            // You can use 'hiringDate' in your code.
        }
        else
        {
            // Handle the case where the conversion failed. Log an error, set a default value, etc.
        }
        string Email = newValues["Email"].ToString();
        string Department = newValues["Department"].ToString();
        string birthDateString = newValues["BirthDate"].ToString();

        if (DateTime.TryParse(birthDateString, out DateTime birthDate))
        {
            // The conversion was successful, and 'birthDate' now contains the DateTime value.
            // You can use 'birthDate' in your code.
        }
        else
        {
            // Handle the case where the conversion failed. Log an error, set a default value, etc.
        }

        string Address = newValues["Address"].ToString();
        // ... Extract other values similarly

        // Insert the new record into the database
        InsertEmployee( employeeIDString ,employeeFirstName, employeeLastName, EmployeeSalary, HiringDate, Email, Department, birthDate, Address  /*, ... other parameters */);

        // Rebind the RadGrid to refresh the data
        Radgrid1.Rebind();
    }

    private void InsertEmployee(string empid, string firstName, string lastName, string salary, DateTime hiredate, string email, string department, DateTime birthdate, string address)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
        string insertSql = "INSERT INTO Employee (EmployeeID, FirstName, LastName, Salary, HireDate, Email, Department, BirthDate, Address) VALUES (@EmployeeID, @FirstName, @LastName, @Salary, @HireDate, @Email, @Department, @BirthDate, @Address)";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(insertSql, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeID", empid);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Salary", salary );
                cmd.Parameters.AddWithValue("@HireDate", hiredate);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@BirthDate", birthdate);
                cmd.Parameters.AddWithValue("@Address", address);
                
                // ... Add other parameters similarly

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
