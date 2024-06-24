using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Admin_TalukaMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetState();
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
                SqlCommand cmd = new SqlCommand("Insert into TalukaMaster values('" + ddlState.SelectedValue + "','" + ddlDistrict.SelectedValue + "','" + txtTaluka.Text + "')", con);
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
                SqlCommand cmd = new SqlCommand("update TalukaMaster set StateId='" + ddlState.SelectedValue + "',DistrictId='" + ddlDistrict.SelectedValue+ "',Taluka='"+txtTaluka.Text+"' where TalukaId=" + HfId.Value + "", con);
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
        SqlDataAdapter da = new SqlDataAdapter("Select * from TalukaMaster where StateId='" + ddlState.SelectedValue + "' and DistrictId='" + ddlDistrict.SelectedValue + "'and Taluka='" + txtTaluka.Text + "' and TalukaId!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Already Exist");
        }
    }

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select TM.TalukaId,SM.State,TM.Taluka,DM.District from TalukaMaster TM left join StateMaster SM on SM.StateId=TM.StateId left join DistrictMaster DM on DM.DistrictId=TM.DistrictId", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

    void GetState()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from StateMaster", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlState.DataSource = dt;
        ddlState.DataTextField = "State";
        ddlState.DataValueField = "StateId";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    void GetDistrictBasedOnState(string stateId)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from DistrictMaster where StateId='" + stateId + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlDistrict.DataSource = dt;
        ddlDistrict.DataTextField = "District";
        ddlDistrict.DataValueField = "DistrictId";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Updates")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from TalukaMaster where TalukaId='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["TalukaId"].ToString();
                ddlState.SelectedValue = dt.Rows[0]["StateId"].ToString();
                GetDistrictBasedOnState(ddlState.SelectedValue);
                ddlDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
                txtTaluka.Text = dt.Rows[0]["Taluka"].ToString();
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from TalukaMaster where TalukaId='" + e.CommandArgument.ToString() + "'", con);
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
        ddlState.SelectedIndex = 0;
        ddlDistrict.Items.Clear();
        txtTaluka.Text = "";
        ddlState.SelectedIndex = 0;
        BtnSave.Text = "Save";
        ddlState.Focus();
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetDistrictBasedOnState(ddlState.SelectedValue);
        }
        catch {
        }
    }
}