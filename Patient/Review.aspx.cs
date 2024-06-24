using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;


public partial class Patient_Review : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    int hospitalId = 0;
    int patientTreantmentId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        hospitalId = Convert.ToInt32(Request.QueryString["hospitalid"]);
        patientTreantmentId = Convert.ToInt32(Request.QueryString["Id"]);
        if (!IsPostBack)
        {
            GetRecord();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (BtnSave.Text == "Save")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Rating values(" + hospitalId + "," + RatingQ3.CurrentRating + ",'" + txtReivew.Text + "','" + Session["LoginId"].ToString() + "'," + patientTreantmentId + ")", con);
                int result = cmd.ExecuteNonQuery();
                if (result >= 0)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Record Inserted Successfully";
                    GetRecord();
                    con.Close();
                    cmd.Dispose();
                }
                else
                {
                    lblmsg.ForeColor = Color.Red;
                    lblmsg.Text = "Record Failed To Inserted";
                }
            }

            else if (BtnSave.Text == "Update")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Rating set Rating='" + RatingQ3.CurrentRating + "',Comment='" + txtReivew.Text + "' where Id=" + HfId.Value + "", con);
                int result = cmd.ExecuteNonQuery();
                if (result >= 0)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Record Updated Successfully";
                    GetRecord();
                    con.Close();
                    cmd.Dispose();
                }
                else
                {
                    lblmsg.ForeColor = Color.Red;
                    lblmsg.Text = "Record Failed To Update";
                }
            }


        }
        catch (Exception ex)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }
    }

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from Rating where PatientTreatmentId=" + patientTreantmentId + "", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            HfId.Value = dt.Rows[0]["Id"].ToString();
            txtReivew.Text = dt.Rows[0]["Comment"].ToString();
            RatingQ3.CurrentRating = Convert.ToInt32(dt.Rows[0]["Rating"]);
            BtnSave.Text = "Update";
        }
    }
}