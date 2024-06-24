using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;


public partial class Hospital_AddDoctorAvaliable : System.Web.UI.Page
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
                SqlCommand cmd = new SqlCommand("insert into HospitalDoctors values('" + ddlDisease.SelectedValue + "','" + ddlDoctor.SelectedValue + "'," + Convert.ToInt32(Session["LoginId"]) + ")", con);
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
                SqlCommand cmd = new SqlCommand("update HospitalDoctors set DiseaseId='" + ddlDisease.SelectedValue + "',DoctorId='" + ddlDoctor.SelectedValue + "' where Id=" + HfId.Value + "", con);
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
        SqlDataAdapter da = new SqlDataAdapter("Select * from HospitalDoctors where DiseaseId='" + ddlDisease.SelectedValue + "' and DoctorId='" + ddlDoctor.SelectedValue + "' and HospitalId='" + Session["LoginId"].ToString() + "' and Id!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Already Exist");
        }
    }


    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select Id,HT.DiseaseId,HT.DoctorId,DM.DiseaseName,TM.DoctorName from HospitalDoctors HT left join DiseaseMaster DM on DM.DiseaseId=HT.DiseaseId left join DoctorMaster TM on TM.DoctorId=HT.DoctorId where HT.HospitalId=" + Convert.ToInt32(Session["LoginId"]) + " order by Id desc", con);
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
    void GetDoctor()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from DoctorMaster where DiseaseId='" + ddlDisease.SelectedValue + "' order by DoctorId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlDoctor.DataSource = dt;
        ddlDoctor.DataTextField = "DoctorName";
        ddlDoctor.DataValueField = "DoctorId";
        ddlDoctor.DataBind();
        ddlDoctor.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Updates")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from HospitalDoctors where Id='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["Id"].ToString();
                ddlDisease.SelectedValue = dt.Rows[0]["DiseaseId"].ToString();
                GetDoctor();
                ddlDoctor.SelectedValue = dt.Rows[0]["DoctorId"].ToString();
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from HospitalDoctors where Id='" + e.CommandArgument.ToString() + "'", con);
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
        ddlDisease.SelectedIndex = 0;
        ddlDoctor.Items.Clear();
        BtnSave.Text = "Save";
        ddlDisease.Focus();
    }

    protected void ddlDisease_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDoctor(); 
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}