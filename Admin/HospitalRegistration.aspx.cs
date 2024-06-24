using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_HospitalRegistration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRecord();
            GetState();
            
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            CheckEmailExist(txtHospitalEmail.Text);
            if (BtnSave.Text == "Save")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into HospitalRegistration values('" + txtHospitalName.Text + "','" + txtHospitalEmail.Text + "','" + txtPhoneNumber.Text + "','" + txtPasswords.Text + "','" + ddlState.SelectedValue + "','" + ddlDistrict.SelectedValue + "','" + ddlTaluka.SelectedValue + "','" + ddlCity.SelectedValue + "','" + txtAddress.Text + "')", con);
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
                SqlCommand cmd = new SqlCommand("update HospitalRegistration set HospitalName='" + txtHospitalName.Text + "',HospitalEmail='" + txtHospitalEmail.Text + "',PhoneNumber='" + txtPhoneNumber.Text + "',Passwords='" + txtPasswords.Text + "',StateId='" + ddlState.SelectedValue + "',DistrictId='" + ddlDistrict.SelectedValue + "',TalukaId='" + ddlTaluka.SelectedValue + "',CityId='" + ddlCity.SelectedValue + "',Address='" + txtAddress.Text + "' where HospitalId='" + HfId.Value + "'", con);
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
        SqlDataAdapter da = new SqlDataAdapter("Select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,HR.Passwords,SM.State,DM.District,TM.Taluka,CM.City,HR.Address from HospitalRegistration HR left join StateMaster SM on SM.StateId=HR.StateId left join DistrictMaster DM on DM.DistrictId=HR.DistrictId left join TalukaMaster TM on Tm.TalukaId=HR.TalukaId left join CityMaster CM on CM.CityId=HR.CityId order by HR.HospitalId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

    void CheckEmailExist(string email)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from HospitalRegistration where HospitalEmail='" + email + "' and HospitalId!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Email is already exist");
        }

    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblmsg.Text = "";
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Updates")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from HospitalRegistration where HospitalId='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["HospitalId"].ToString();
                txtHospitalName.Text = dt.Rows[0]["HospitalName"].ToString();
                txtHospitalEmail.Text = dt.Rows[0]["HospitalEmail"].ToString();
                txtPhoneNumber.Text = dt.Rows[0]["PhoneNumber"].ToString();
                txtPasswords.Text = dt.Rows[0]["Passwords"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                ddlState.SelectedValue= dt.Rows[0]["StateId"].ToString();
                GetDistrictBasedOnState(ddlState.SelectedValue);
                ddlDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
                GetTalukaBasedOnDistrict(ddlDistrict.SelectedValue);
                ddlTaluka.SelectedValue = dt.Rows[0]["TalukaId"].ToString();
                GetCityBasedOnTaluka(ddlTaluka.SelectedValue);
                ddlCity.SelectedValue = dt.Rows[0]["CityId"].ToString();
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from HospitalRegistration where HospitalId='" + e.CommandArgument.ToString() + "'", con);
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
        txtHospitalName.Text = "";
        txtHospitalEmail.Text = "";
        txtPhoneNumber.Text = "";
        txtPasswords.Text = "";
        txtAddress.Text = "";
        ddlState.SelectedIndex = 0;
        ddlDistrict.Items.Clear();
        ddlTaluka.Items.Clear();
        ddlCity.Items.Clear();
        BtnSave.Text = "Save";
        txtHospitalName.Focus();
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

    void GetCityBasedOnTaluka(string talukaId)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from CityMaster where TalukaId='" + talukaId + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlCity.DataSource = dt;
        ddlCity.DataTextField = "City";
        ddlCity.DataValueField = "CityId";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void ddlTaluka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetCityBasedOnTaluka(ddlTaluka.SelectedValue);
        }
        catch {
        }
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
        catch
        {
        }
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}