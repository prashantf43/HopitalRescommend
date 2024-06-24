using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    SqlDataAdapter da;

    public DataTable GetUserDetail(string role, int Id)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            if (role.Equals("Admin"))
            {
                da = new SqlDataAdapter("select * from Admin where Id='" + Id + "'", connection);
            }
            else if (role.Equals("Hospital"))
            {
                da = new SqlDataAdapter("select * from HospitalRegistration where HospitalId='" + Id + "'", connection);
            }
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}