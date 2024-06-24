using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Admin_CityMaster : System.Web.UI.Page
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
            RecordExist(ddlState.SelectedValue, ddlDistrict.Text, ddlTaluka.SelectedValue, txtCity.Text);
            if (BtnSave.Text == "Save")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into CityMaster values('" + ddlState.SelectedValue + "','" + ddlDistrict.SelectedValue + "','" + ddlTaluka.SelectedValue + "','" + txtCity.Text + "')", con);
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
                SqlCommand cmd = new SqlCommand("update CityMaster set StateId='" + ddlState.SelectedValue + "',DistrictId='" + ddlDistrict.SelectedValue + "',TalukaId='" + ddlTaluka.SelectedValue + "',City='" + txtCity.Text + "' where CityId=" + HfId.Value + "", con);
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

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select CM.CityId,CM.City,SM.State,TM.Taluka,DM.District from CityMaster CM left join StateMaster SM on SM.StateId=CM.StateId left join DistrictMaster DM on DM.DistrictId=CM.DistrictId left join TalukaMaster TM on TM.TalukaId=CM.TalukaId", con);
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

    void GetTalukaBasedOnDistrict(string districtId)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from TalukaMaster where DistrictId='" + districtId + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlTaluka.DataSource = dt;
        ddlTaluka.DataTextField = "Taluka";
        ddlTaluka.DataValueField = "TalukaId";
        ddlTaluka.DataBind();
        ddlTaluka.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Updates")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from CityMaster where CityId='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["TalukaId"].ToString();
                ddlState.SelectedValue = dt.Rows[0]["StateId"].ToString();
                GetDistrictBasedOnState(ddlState.SelectedValue);
                ddlDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
                GetTalukaBasedOnDistrict(ddlDistrict.SelectedValue);
                ddlTaluka.SelectedValue = dt.Rows[0]["TalukaId"].ToString();
                txtCity.Text = dt.Rows[0]["City"].ToString();
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from CityMaster where CityId='" + e.CommandArgument.ToString() + "'", con);
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

    void RecordExist(string stateId, string districtId, string talukaId,string city)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from CityMaster where StateId='" + stateId + "' and DistrictId='" + districtId + "' and talukaId='" + talukaId + "' and City='" + city + "' and CityId!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            throw new Exception("Already Exist");
        }

    }

    void Clear()
    {
        ddlState.SelectedIndex = 0;
        ddlDistrict.Items.Clear();
        ddlTaluka.Items.Clear();
        txtCity.Text = "";
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
        catch
        {
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetTalukaBasedOnDistrict(ddlDistrict.SelectedValue);
        }
        catch {
        }
    }
}