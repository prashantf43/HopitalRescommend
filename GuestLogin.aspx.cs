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
            DataTable dt = login.CheckHospitalUser(txtEmail.Text.Trim(), txtPassword.Text.Trim());

            if (dt.Rows.Count > 0)
            {
                Session["LoginId"] = dt.Rows[0]["HospitalId"].ToString();
                Session["Role"] = "Hospital";
                
                Response.Redirect("~/Hospital/DashBoard.aspx?name=" + dt.Rows[0]["HospitalName"].ToString() + "");
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

    protected void btnHospital_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HospitalLogin.aspx");
    }

    protected void btnPatient_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PatientLogin.aspx");
    }
   
}