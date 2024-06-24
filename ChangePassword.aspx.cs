using System;
using System.Drawing;

public partial class ChangePassword : System.Web.UI.Page
{
    int userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginId"] != null && Session["Role"] != null)
        {
            userId = Convert.ToInt32(Session["LoginId"].ToString());
        }
        else
        {
            Response.Redirect("~/SessionExpired.aspx");
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Password password = new Password();
            bool oldPasswordValid = false;
            bool result = false;
            if (Session["Role"].ToString().Equals("Admin"))
            {
                oldPasswordValid = password.CheckAdminOldPassword(txtOldPassword.Text, userId);
            }
            else if (Session["Role"].ToString().Equals("Hospital"))
            {
                oldPasswordValid = password.CheckHospitalOldPassword(txtOldPassword.Text, userId);
            }
            else if (Session["Role"].ToString().Equals("Patient"))
            {
                oldPasswordValid = password.CheckPatientOldPassword(txtOldPassword.Text, userId);
            }


            if (oldPasswordValid == false)
            {
                lblmsg.ForeColor = Color.Red;
                lblmsg.Text = "Old Password Not Match";
                return;
            }

            if (Session["Role"].ToString().Equals("Admin"))
            {
                result = password.ChangeAdminPassword(txtNewPassword.Text, userId);
            }
            else if (Session["Role"].ToString().Equals("Hospital"))
            {
                result = password.ChangeHospitalPassword(txtNewPassword.Text, userId);
            }
            else if (Session["Role"].ToString().Equals("Patient"))
            {
                result = password.ChangePatientPassword(txtNewPassword.Text, userId);
            }


            if (result == true)
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Password Changed Successfully....";
                clear();
                txtOldPassword.Focus();
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Password Not Changed";
            }
        }
        catch (Exception ex)
        {
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = ex.Message;
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
    }

    void clear()
    {
        txtOldPassword.Text = "";
        txtNewPassword.Text = "";
        txtReNewPassword.Text = "";
    }
}