using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Patient_Treatments : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRecord();
        }
    }

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("select HR.HospitalName, PT.Id,DM.DiseaseName,TM.Treatment,DOM.DoctorName from  PatientTreatments PT Left join DiseaseMaster DM on DM.DiseaseId=PT.DiseaseId Left join DoctorMaster DOM on DOM.DoctorId=PT.DoctorId Left join TreatmentMaster TM on TM.TreatmentId=PT.TreatmentId Left join HospitalRegistration HR on HR.HospitalId=PT.HospitalId where PT.PatientId='" + Session["LoginId"].ToString() + "' order by PT.Id desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

    protected void gvDetails_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        DataTable dt = GetOtherDetail(Convert.ToInt32(e.CommandArgument.ToString()));
        if (e.CommandName == "Updates")
        {
            Response.Redirect("~/Patient/Review.aspx?Id=" + e.CommandArgument.ToString() + "&hospitalid=" + dt.Rows[0]["HospitalId"].ToString());
        }
    }

    private  DataTable GetOtherDetail(int id)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from PatientTreatments where Id='" + id + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    protected void gvDetails_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}