using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Recommendation_SearchHospitalByTreatment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDisease();
            GetFacilityRecord();
        }
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

    protected void ddlDisease_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkDoctor.Checked)
        {
            GetDoctor();
        }
        GetTreatment();
        gvDetails.DataSource = null;
        gvDetails.DataBind();
        if (chkFacility.Checked)
        {
            ddlFacility.SelectedIndex = 0;

        }
    }

    void GetTreatment()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from TreatmentMaster where DiseaseId=" + ddlDisease.SelectedValue + "  order by TreatmentId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlTreatment.DataSource = dt;
        ddlTreatment.DataTextField = "Treatment";
        ddlTreatment.DataValueField = "TreatmentId";
        ddlTreatment.DataBind();
        ddlTreatment.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void chkCost_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCost.Checked)
        {
            divCost.Visible = true;
        }
        else if (!chkCost.Checked)
        {
            divCost.Visible = false;
        }
    }

    protected void ddlTreatment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkFacility.Checked)
        {
            ddlFacility.SelectedIndex = 0;

        }
        SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByTreatment, 0, 0, 0);
    }

    private void SearchHospital(int diseaseId, int treatmentId, FilterType filterType, int facilityId, decimal fromCost = 0, decimal toCost = 0, int rating = 0, int doctorId = 0, int cityId = 0, int stateId = 0, int districtId = 0, int talukaId = 0)
    {
        SearchingAlgorithm searchingAlgorithm = new SearchingAlgorithm();
        DataTable dt = searchingAlgorithm.GetHospital(diseaseId, treatmentId, fromCost, toCost, filterType, facilityId, rating, doctorId, cityId, stateId, districtId, talukaId);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Text = "";
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
        }
        else
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = "No hospital found";
        }
    }

    protected void costList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int doctorId = 0;
        if (ddlDisease.SelectedIndex == 0 && ddlTreatment.SelectedIndex == -1 || ddlTreatment.SelectedIndex == 0)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = "Please select disease and treatment";
            return;
        }
        if (chkDoctor.Checked && ddlDoctor.SelectedIndex > 0)
        {
            doctorId = Convert.ToInt32(ddlDoctor.SelectedValue);
        }
        if (costList.SelectedIndex == 0)
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByCost, 0, 0, 0, 0, doctorId);
        }
        else if (costList.SelectedIndex == 1)
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByCost, 0, 0, 5000, 0, doctorId);
        }
        else if (costList.SelectedIndex == 2)
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByCost, 0, 5000, 20000, 0, doctorId);
        }

        else if (costList.SelectedIndex == 3)
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByCost, 0, 20000, 50000, 0, doctorId);
        }

        else if (costList.SelectedIndex == 4)
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByCost, 0, 50000, 100000, 0, doctorId);
        }
        else if (costList.SelectedIndex == 5)
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByCost, 0, 100000, 10000000, 0, doctorId);
        }
    }

    protected void chkLocation_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLocation.Checked)
        {
            divLocation.Visible = true;
        }
        else
        {
            divLocation.Visible = false;
        }
    }

    protected void chkFacility_CheckedChanged(object sender, EventArgs e)
    {

        if (chkFacility.Checked)
        {
            GetFacilityRecord();
            divFacility.Visible = true;
        }
        else
        {
            divFacility.Visible = false;
        }
    }

    private void GetFacilityRecord()
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

    protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDisease.SelectedIndex == 0 && ddlTreatment.SelectedIndex == -1 || ddlTreatment.SelectedIndex == 0)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = "Please select disease and treatment";
            return;
        }
        SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByFacility, Convert.ToInt32(ddlFacility.SelectedValue), 0, 0);
    }

    protected void chkRating_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlDisease.SelectedIndex == 0 && ddlTreatment.SelectedIndex == -1 || ddlTreatment.SelectedIndex == 0)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = "Please select disease and treatment";
            chkRating.Checked = false;
            return;
        }
        if (chkRating.Checked)
        {
            divRating.Visible = true;
        }
        else
        {
            divRating.Visible = false;
            RatingQ3.CurrentRating = 0;
        }
    }

    protected void RatingQ3_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        if (ddlDisease.SelectedIndex == 0 && ddlTreatment.SelectedIndex == -1 || ddlTreatment.SelectedIndex == 0)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = "Please select disease and treatment";
            chkRating.Checked = false;
            return;
        }

        if (ddlFacility.SelectedIndex != 0)
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByRating, Convert.ToInt32(ddlFacility.SelectedValue), 0, 0, RatingQ3.CurrentRating);
        }
        else
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByRating, 0, 0, 0, RatingQ3.CurrentRating);
        }

    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Updates")
        {
            Response.Redirect("~/Recommendation/HospitalRatingAndReview.aspx?Id=" + e.CommandArgument.ToString());
        }
    }

    protected void brnReset_Click(object sender, EventArgs e)
    {
        chkCost.Checked = false;
        chkFacility.Checked = false;
        chkRating.Checked = false;
        divCost.Visible = false;
        divFacility.Visible = false;
        divRating.Visible = false;
        ddlTreatment.Items.Clear();
        ddlFacility.Items.Clear();
        ddlDisease.SelectedIndex = 0;
        RatingQ3.CurrentRating = 0;
        lblmsg.Text = "";
        gvDetails.DataSource = null;
        gvDetails.DataBind();
        chkDoctor.Checked = false;
        divDoctor.Visible = false;
        chkLocation.Checked = false;
        divLocation.Visible = false;
    }

    protected void chkDoctor_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDoctor.Checked)
        {
            if (ddlDisease.SelectedIndex != 0)
            {
                GetDoctor();
                divDoctor.Visible = true;
            }
            else
            {
                lblmsg.ForeColor = Color.Red;
                lblmsg.Text = "Please select disease";
                chkDoctor.Checked = false;
            }
        }
        else
        {
            ddlDoctor.SelectedIndex = 0;
            divDoctor.Visible = false;
        }
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

    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByDoctor, 0, 0, 0, 0, Convert.ToInt32(ddlDoctor.SelectedValue));
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

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByLocation, 0, 0, 0, 0, 0, 0, Convert.ToInt32(ddlState.SelectedValue), 0, 0);
            GetDistrictBasedOnState(ddlState.SelectedValue);
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = Color.Red;
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByLocation, 0, 0, 0, 0, 0, 0, Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0);
            GetTalukaBasedOnDistrict(ddlDistrict.SelectedValue);
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = Color.Red;
        }
    }

    protected void ddlTaluka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByLocation, 0, 0, 0, 0, 0, 0, Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluka.SelectedValue));
            GetCityBasedOnTaluka(ddlTaluka.SelectedValue);
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = Color.Red;
        }
    }

    protected void chkLocation_CheckedChanged1(object sender, EventArgs e)
    {
        if (ddlDisease.SelectedIndex == 0 && ddlTreatment.SelectedIndex == -1 || ddlTreatment.SelectedIndex == 0)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = "Please select disease and treatment";
            chkLocation.Checked = false;
            return;
        }
        if (chkLocation.Checked)
        {
            GetState();
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlTaluka.Items.Clear();
            ddlCity.Items.Clear();
            divLocation.Visible = true;
        }
        else
        {
            divLocation.Visible = false;
        }
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchHospital(Convert.ToInt32(ddlDisease.SelectedValue), Convert.ToInt32(ddlTreatment.SelectedValue), FilterType.ByLocation, 0, 0, 0, 0, 0, Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluka.SelectedValue));
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
    }
}