using System.Configuration;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for Password
/// </summary>
public class Password
{
    public Password()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool ChangeAdminPassword(string newPassword, int userId)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("update Admin set passwords='" + newPassword + "' where Id='" + userId + "'", connection);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool ChangeHospitalPassword(string newPassword, int Id)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("update HospitalRegistration set passwords='" + newPassword + "' where HospitalId='" + Id + "'", connection);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool ChangePatientPassword(string newPassword, int Id)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("update PatientRegistration set passwords='" + newPassword + "' where PatientId='" + Id + "'", connection);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool CheckAdminOldPassword(string oldPassword, int userId)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Admin where passwords='" + oldPassword + "' and Id='" + userId + "'", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool CheckHospitalOldPassword(string oldPassword, int Id)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from HospitalRegistration where passwords='" + oldPassword + "' and HospitalId='" + Id + "'", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool CheckPatientOldPassword(string oldPassword, int Id)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from PatientRegistration where passwords='" + oldPassword + "' and PatientId='" + Id + "'", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}