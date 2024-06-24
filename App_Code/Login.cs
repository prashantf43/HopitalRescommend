using System.Configuration;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Login
/// </summary>
public class Login
{
    
    public Login()
    {
      
    }

    public DataTable CheckAdminUser(string email,string password)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Admin where email='" + email + "' and passwords='" + password + "'", connection);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            return dt;
        }
    }

    public DataTable CheckHospitalUser(string email, string password)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from HospitalRegistration where hospitalemail='" + email + "' and passwords='" + password + "'", connection);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            return dt;
        }
    }

    public DataTable CheckPatientUser(string aadharNumber, string password)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from PatientRegistration where AadharNumber='" + aadharNumber + "' and Passwords='" + password + "'", connection);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            return dt;
        }
    }
}