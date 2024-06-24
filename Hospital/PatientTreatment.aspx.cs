using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Hospital_PatientTreatment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtAadharNumber.Focus();
            GetDisease();
            GetTreatment();
            GetRecord();
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

    void GetTreatment()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select HT.TreatmentId,TM.Treatment,HT.Cost from HospitalTreatment HT left join TreatmentMaster TM  on TM.TreatmentId=HT.TreatmentId where HT.HospitalId=" + Convert.ToInt32(Session["LoginId"]) + " order by TreatmentId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlTreatment.DataSource = dt;
        ddlTreatment.DataTextField = "Treatment";
        ddlTreatment.DataValueField = "TreatmentId";
        ddlTreatment.DataBind();
        ddlTreatment.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    void GetPatientDetail(string aadharNumber, int patientId, bool isUpdate = false)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from PatientRegistration where AadharNumber='" + aadharNumber + "' OR patientId=" + patientId + "", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hfPatientIds.Value = dt.Rows[0]["PatientId"].ToString();
            if (!isUpdate)
            {
                var dtRecord = CheckPatientExist(Convert.ToInt32(hfPatientIds.Value));
                if (dtRecord.Rows.Count <= 0)
                {
                    lblmsg.ForeColor = Color.Red;
                    lblmsg.Text = "Either Patient Is Discharged OR Not Admitted In This Hospital";
                    return;
                }
            }

            txtAadharNumber.Text = dt.Rows[0]["AadharNumber"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
        }
        else
        {
            Clear();
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = "Patient Not Found";
        }
    }

    DataTable CheckPatientExist(int patientId)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from HospitalPatients where PatientId=" + patientId + " and DateOfDischarge is null and HospitalId=" + Convert.ToInt32(Session["LoginId"]) + "", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    void Clear()
    {
        hfPatientIds.Value = "";
        txtAadharNumber.Text = "";
        txtName.Text = "";
        txtAddress.Text = "";
        ddlDisease.SelectedIndex = 0;
        ddlDoctor.Items.Clear();
        ddlTreatment.SelectedIndex = 0;
        txtTreatmentCost.Text = "";
        txtOtherCharges.Text = "";
        BtnSave.Text = "Save";
        txtAadharNumber.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetPatientDetail(txtAadharNumber.Text, 0);

        }
        catch (Exception ex)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }
    }

    void GetDoctorBasedOnDisease(string diseaseId)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select HD.DoctorId,DM.DoctorName from HospitalDoctors HD left join DoctorMaster DM on DM.DoctorId=HD.DoctorId where HD.DiseaseId=" + diseaseId + " and HD.HospitalId=" + Convert.ToInt32(Session["LoginId"]) + "", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlDoctor.DataSource = dt;
        ddlDoctor.DataTextField = "DoctorName";
        ddlDoctor.DataValueField = "DoctorId";
        ddlDoctor.DataBind();
        ddlDoctor.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void ddlDisease_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetDoctorBasedOnDisease(ddlDisease.SelectedValue);
        }
        catch (Exception ex)
        {

            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }
    }

    void GetTreatmentCost(string treatmentId)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select HT.Cost from HospitalTreatment HT where HT.Id=" + treatmentId + "", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtTreatmentCost.Text = dt.Rows[0][0].ToString();
        }
        else
        {
            txtTreatmentCost.Text = "0.00";
        }
    }

    protected void ddlTreatment_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTreatmentCost(ddlTreatment.SelectedValue);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (BtnSave.Text == "Save")
            {
                if (hfPatientIds.Value == "")
                {
                    lblmsg.ForeColor = Color.Red;
                    lblmsg.Text = "Please Enter The Patient Detail By Searching Aadhar number";
                    return;
                }

                con.Open();
                decimal totalCost = 0;
                totalCost = Convert.ToDecimal(txtTreatmentCost.Text) + Convert.ToDecimal(txtOtherCharges.Text);
                SqlCommand cmd = new SqlCommand("insert into PatientTreatments values(" + hfPatientIds.Value + "," + ddlDisease.SelectedValue + "," + ddlDoctor.SelectedValue + "," + ddlTreatment.SelectedValue + "," + txtTreatmentCost.Text + "," + txtOtherCharges.Text + "," + totalCost + "," + Convert.ToInt32(Session["LoginId"]) + ",0)", con);
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
                decimal totalCost = 0;
                totalCost = Convert.ToDecimal(txtTreatmentCost.Text) + Convert.ToDecimal(txtOtherCharges.Text);
                SqlCommand cmd = new SqlCommand("update PatientTreatments set PatientId=" + hfPatientIds.Value + ",DiseaseId=" + ddlDisease.SelectedValue + ",DoctorId=" + ddlDoctor.SelectedValue + ",TreatmentId=" + ddlTreatment.SelectedValue + ",TreatentCost=" + txtTreatmentCost.Text + ",OtherCharges=" + txtOtherCharges.Text + ",TotalCost=" + totalCost + " where Id=" + HfId.Value + "", con);
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
        SqlDataAdapter da = new SqlDataAdapter("select PT.Id,PR. [Name],PR.ContactNumber,DM.DiseaseName,DOM.DoctorName,TM.Treatment,PT.TreatentCost,PT.OtherCharges,PT.TotalCost from PatientTreatments PT inner join PatientRegistration PR on PT.PatientId = PR.PatientId inner join DiseaseMaster DM on DM.DiseaseId = PT.DiseaseId inner join DoctorMaster DOM on DOM.DoctorId = PT.DoctorId inner join TreatmentMaster TM on TM.TreatmentId = PT.TreatmentId where PT.HospitalId = " + Convert.ToInt32(Session["LoginId"]) + " order by PT.Id desc", con);
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
                SqlDataAdapter da = new SqlDataAdapter("select * from PatientTreatments where Id='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["Id"].ToString();
                GetPatientDetail(null, Convert.ToInt32(dt.Rows[0]["PatientId"].ToString()), true);
                ddlDisease.SelectedValue = dt.Rows[0]["DiseaseId"].ToString();
                GetDoctorBasedOnDisease(ddlDisease.SelectedValue);
                ddlDoctor.SelectedValue = dt.Rows[0]["DoctorId"].ToString();
                ddlTreatment.SelectedValue = dt.Rows[0]["TreatmentId"].ToString();
                GetTreatmentCost(ddlTreatment.SelectedValue);
                txtOtherCharges.Text = dt.Rows[0]["OtherCharges"].ToString();
                BtnSave.Text = "Update";
                lblmsg.Text = "";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from PatientTreatments where Id='" + e.CommandArgument.ToString() + "'", con);
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


    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblmsg.Text = "";
    }


    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}