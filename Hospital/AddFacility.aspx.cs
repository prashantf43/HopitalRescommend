using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
public partial class Hospital_AddFacility : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetFacility();
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
                SqlCommand cmd = new SqlCommand("insert into HospitalFacility values('" + ddlFacility.SelectedValue + "','" + txtCost.Text + "'," + Convert.ToInt32(Session["LoginId"]) + ")", con);
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
                SqlCommand cmd = new SqlCommand("update HospitalFacility set FacilityId='" + ddlFacility.SelectedValue + "',Cost='" + txtCost.Text + "' where Id=" + HfId.Value + "", con);
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
        SqlDataAdapter da = new SqlDataAdapter("Select * from HospitalFacility where FacilityId='" + ddlFacility.SelectedValue + "' and HospitalId='"+Session["LoginId"].ToString()+"' and Id!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Already Exist");
        }
    }

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select HM.Id,HM.FacilityId,FM.FacilityName,HM.Cost from HospitalFacility HM left join FacilityMaster FM on FM.FacilityId=HM.FacilityId where HospitalId=" + Convert.ToInt32(Session["LoginId"]) + " order by HM.Id desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

    void GetFacility()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from FacilityMaster order by FacilityId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlFacility.DataSource = dt;
        ddlFacility.DataTextField = "FacilityName";
        ddlFacility.DataValueField = "FacilityId";
        ddlFacility.DataBind();
        ddlFacility.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Updates")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from HospitalFacility where Id='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["Id"].ToString();
                ddlFacility.SelectedValue = dt.Rows[0]["FacilityId"].ToString();
                txtCost.Text = dt.Rows[0]["Cost"].ToString();
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from HospitalFacility where Id='" + e.CommandArgument.ToString() + "'", con);
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
        txtCost.Text = "";
        ddlFacility.SelectedIndex = 0;
        BtnSave.Text = "Save";
        ddlFacility.Focus();

    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}