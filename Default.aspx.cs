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
            txtEmail.Focus();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            Login login = new Login();
            DataTable dt = login.CheckAdminUser(txtEmail.Text.Trim(), txtPassword.Text.Trim());

            if (dt.Rows.Count > 0)
            {
                Session["LoginId"] = dt.Rows[0]["Id"].ToString();
                Session["Role"] = "Admin";
                //Response.Cookies["LoginDetailCk"]["LoginId"] = dt.Rows[0]["Id"].ToString();
                //Response.Cookies["LoginDetailCk"].Expires = DateTime.Now.AddHours(1); 
                Response.Redirect("~/Admin/DashBoard.aspx");
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

    protected void btnHospital_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HospitalLogin.aspx");
    }

    protected void btnPatient_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PatientLogin.aspx");
    }

    protected void btnGuest_Click(object sender, EventArgs e)
    {
        Session["Role"] = "Guest";
        Session["LoginId"] = 1;
        Response.Redirect("~/Recommendation/SearchHospitalByTreatment.aspx");
    }
}