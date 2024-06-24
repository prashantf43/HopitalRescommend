using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Patient_DashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
            //SqlDataAdapter da = new SqlDataAdapter("select * from AddEvent A inner join CollegeMaster CM on CM.CollegeId=A.CollegeId where A.CollegeId!='" + Session["LoginId"].ToString() + "' and EventDate>'" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + "'", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //RptEnquiryList.DataSource = dt;
            //RptEnquiryList.DataBind();
        }
        catch {
        }

    }
}