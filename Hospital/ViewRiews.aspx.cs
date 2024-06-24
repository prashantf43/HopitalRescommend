using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class Hospital_ViewRiews : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    int hospitalId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRecord();
        }
    }

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("select r.rating,r.Comment,PR.Name,H.HospitalName,r.hospitalId from rating r inner join PatientRegistration PR on PR.PatientId=r.PatientId inner join HospitalRegistration H on H.hospitalId=r.hospitalId where r.hospitalId=" + Convert.ToInt32(Session["LoginId"]) + "  group by r.Comment,PR.Name,H.HospitalName,r.hospitalId,r.rating", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

    protected void gvDetails_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}