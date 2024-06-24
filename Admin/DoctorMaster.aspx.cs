using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Admin_DoctorMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDisease();
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
            CheckExist();
            if (BtnSave.Text == "Save")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into DoctorMaster values('" + ddlDisease.SelectedValue + "','" + txtDoctorName.Text + "')", con);
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
                SqlCommand cmd = new SqlCommand("update DoctorMaster set DiseaseId='" + ddlDisease.SelectedValue + "',DoctorName='" + txtDoctorName.Text + "' where DoctorId=" + HfId.Value + "", con);
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

    private void CheckExist()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from DoctorMaster where DiseaseId='" + ddlDisease.SelectedValue + "' and DoctorName='" + txtDoctorName.Text + "' and DoctorId!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Already Exist");
        }
    }
    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select TM.DoctorId,TM.DiseaseId,DM.DiseaseName,TM.DoctorName from DoctorMaster TM left join DiseaseMaster DM on TM.DiseaseId=DM.DiseaseId order by TM.DoctorId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

    void GetDisease()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from DiseaseMaster order by DiseaseId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlDisease.DataSource = dt;
        ddlDisease.DataTextField = "Diseasename";
        ddlDisease.DataValueField = "DiseaseId";
        ddlDisease.DataBind();
        ddlDisease.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Updates")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from DoctorMaster where DoctorId='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["DoctorId"].ToString();
                ddlDisease.SelectedValue = dt.Rows[0]["DiseaseId"].ToString();
                txtDoctorName.Text = dt.Rows[0]["DoctorName"].ToString();
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from DoctorMaster where DoctorId='" + e.CommandArgument.ToString() + "'", con);
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
        catch
        {
        }
    }

    void Clear()
    {
        txtDoctorName.Text = "";
        ddlDisease.SelectedIndex = 0;
        BtnSave.Text = "Save";
        ddlDisease.Focus();

    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}