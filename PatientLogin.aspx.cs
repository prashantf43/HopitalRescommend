using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtAadharNumber.Focus();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            Login login = new Login();
            DataTable dt = login.CheckPatientUser(txtAadharNumber.Text.Trim(), txtPassword.Text.Trim());

            if (dt.Rows.Count > 0)
            {
                Session["LoginId"] = dt.Rows[0]["PatientId"].ToString();
                Session["Role"] = "Patient";

                Response.Redirect("~/Hospital/DashBoard.aspx?name=" + dt.Rows[0]["Name"].ToString() + "");
            }
            else
            {
                lblmsg.Text = "Incorrect Credential. Please try again!";
                lblmsg.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = Color.Red;
        }
    }

    protected void btnAdminLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void btnGuest_Click(object sender, EventArgs e)
    {
        Session["Role"] = "Guest";
        Session["LoginId"] = 1;
        Response.Redirect("~/Recommendation/SearchHospitalByTreatment.aspx");
    }

    protected void btnHospital_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HospitalLogin.aspx");
    }
}