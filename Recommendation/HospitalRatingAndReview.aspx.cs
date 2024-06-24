using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class Recommendation_HospitalRatingAndReview : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    int hospitalId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        hospitalId = Convert.ToInt32(Request.QueryString["Id"]);
        if (!IsPostBack)
        {
            GetRecord();
        }
    }

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("select r.rating,r.Comment,PR.Name,H.HospitalName,r.hospitalId from rating r inner join PatientRegistration PR on PR.PatientId=r.PatientId inner join HospitalRegistration H on H.hospitalId=r.hospitalId where r.hospitalId=" + hospitalId + "  group by r.Comment,PR.Name,H.HospitalName,r.hospitalId,r.rating", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            divHospitalName.Visible = true;
            divRating.Visible = true;
            lblHospitalName.Text = dt.Rows[0]["HospitalName"].ToString();
            int rating = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rating += Convert.ToInt32(dt.Rows[i]["rating"]);
            }
            RatingQ3.CurrentRating = rating / dt.Rows.Count;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
        }
        else
        {
            lblmsg.Text = "Currently no review and rating present.";
            lblmsg.ForeColor = Color.Red;
        }
    }
}