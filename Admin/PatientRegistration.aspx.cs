using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
public partial class Admin_PatientRegistration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRecord();
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblmsg.Text = "";
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            CheckAadharExist(txtAadharNumber.Text);
            if (BtnSave.Text == "Save")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into PatientRegistration values('" + txtAadharNumber.Text+ "','" + txtName.Text + "','" + txtAddress.Text + "','" + Convert.ToDateTime(txtDOB.Text).ToString("MM/dd/yyyy") + "','" + txtContactNumber.Text + "','" + txtPassword.Text + "')", con);
                int result = cmd.ExecuteNonQuery();
                if (result >= 0)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Record Inserted Successfully";
                    Clear();
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
                SqlCommand cmd = new SqlCommand("update PatientRegistration set AadharNumber='" + txtAadharNumber.Text + "',Name='" + txtName.Text + "',Address='" + txtAddress.Text + "',DOB='" + Convert.ToDateTime(txtDOB.Text).ToString("MM/dd/yyyy") + "',ContactNumber='" + txtContactNumber.Text + "',Passwords='" + txtPassword.Text + "' where PatientId=" + HfId.Value + "", con);
                int result = cmd.ExecuteNonQuery();
                if (result >= 0)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Record Updated Successfully";
                    Clear();
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

    void CheckAadharExist(string aadhar)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from PatientRegistration where AadharNumber='" + aadhar + "' and PatientId!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count>0)
        {
            throw new Exception("Aadhar Number already exist");
        }
    }

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from PatientRegistration order by PatientId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

   
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Updates")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from PatientRegistration where PatientId='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["PatientId"].ToString();
                txtAadharNumber.Text= dt.Rows[0]["AadharNumber"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtDOB.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd-MM-yyyy");
                txtContactNumber.Text = dt.Rows[0]["ContactNumber"].ToString();
                txtPassword.Text = dt.Rows[0]["Passwords"].ToString();
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from PatientRegistration where PatientId='" + e.CommandArgument.ToString() + "'", con);
                int result = cmd.ExecuteNonQuery();
                if (result >= 0)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Record Deleted Successfully";
                    Clear();
                    GetRecord();
                    con.Close();
                    cmd.Dispose();
                }
                else
                {
                    lblmsg.ForeColor = Color.Red;
                    lblmsg.Text = "Record Failed To Delete";
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }
    }

    void Clear()
    {
        txtAadharNumber.Text= "";
        txtName.Text = "";
        txtAddress.Text = "";
        txtDOB.Text = "";
        txtContactNumber.Text = "";
        txtPassword.Text = "";
        BtnSave.Text = "Save";
        txtAadharNumber.Focus();
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}