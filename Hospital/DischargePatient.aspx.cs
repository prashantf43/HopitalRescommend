using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;


public partial class Hospital_DischargePatient : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtAadharNumber.Focus();
            GetPatientStatus();
            GetHospitalRoomDetail();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetPatientDetail(txtAadharNumber.Text, 0);
            GetTreatmentTotal();
        }
        catch(Exception ex)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }
    }

    void GetPatientDetail(string aadharNumber, int patientId)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from PatientRegistration where AadharNumber='" + aadharNumber + "' OR patientId=" + patientId + "", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hfPatientIds.Value = dt.Rows[0]["PatientId"].ToString();
            var dtRecord = CheckPatientExist(Convert.ToInt32(hfPatientIds.Value));
            if (dtRecord.Rows.Count > 0)
            {
                txtDateOfAdmission.Text = Convert.ToDateTime(dtRecord.Rows[0]["DateOfAdmission"]).ToString("dd-MM-yyyy");
            }
            else
            {
                lblmsg.ForeColor = Color.Red;
                lblmsg.Text = "Either Patient Is Discharged OR Not Admitted In This Hospital";
                return;
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

    void GetPatientStatus()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from PatientStatusMaster", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlPatientStatus.DataSource = dt;
        ddlPatientStatus.DataTextField = "PatientStatus";
        ddlPatientStatus.DataValueField = "PatientStatusId";
        ddlPatientStatus.DataBind();
        ddlPatientStatus.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    void Clear()
    {
        hfPatientIds.Value = "";
        txtAadharNumber.Text = "";
        txtName.Text = "";
        txtAddress.Text = "";
        txtDateOfAdmission.Text = "";
        ddlPatientStatus.SelectedIndex = 0;
        txtDischargeDate.Text = "";
        txtTotalClearDays.Text = "";
        ddlRoomType.SelectedIndex = 0;
        txtRentPerDay.Text = "";
        txtRentCost.Text = "";
        txtTreatmentCost.Text = "";
        txtFinalCharges.Text = "";
        BtnSave.Text = "Save";
        txtAadharNumber.Focus();
    }

    void GetHospitalRoomDetail()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select HRD.RoomId,RM.RoomType from HospitalRoomDetail HRD left join RoomMaster RM on RM.RoomId=HRD.RoomId where HRD.HospitalId=" + Convert.ToInt32(Session["LoginId"]) + "", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlRoomType.DataSource = dt;
        ddlRoomType.DataTextField = "RoomType";
        ddlRoomType.DataValueField = "RoomId";
        ddlRoomType.DataBind();
        ddlRoomType.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
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
                SqlCommand cmd = new SqlCommand("insert into DischargePatient values(" + hfPatientIds.Value + "," + ddlPatientStatus.SelectedValue + ",'" + Convert.ToDateTime(txtDischargeDate.Text).ToString("MM/dd/yyyy") + "'," + txtTotalClearDays.Text + "," + txtRentCost.Text + "," + txtTreatmentCost.Text + "," + txtFinalCharges.Text + "," + Convert.ToInt32(Session["LoginId"]) + ")", con);

                int result = cmd.ExecuteNonQuery();

                if (result >= 0)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Record Inserted Successfully";
                    UpdateDischargeDate(hfPatientIds.Value);
                    UpdatePatientTreatmentDetail();
                    Clear();
                    con.Close();
                    cmd.Dispose();
                }
                else
                {
                    lblmsg.ForeColor = Color.Red;
                    lblmsg.Text = "Record Failed To Inserted";
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }

    }

    private void UpdatePatientTreatmentDetail()
    {
        SqlCommand cmd = new SqlCommand("update PatientTreatments set Discharged=1 where HospitalId=" + Convert.ToInt32(Session["LoginId"]) + " and  PatientId=" + hfPatientIds.Value + "", con);
        cmd.ExecuteNonQuery();
    }

    void UpdateDischargeDate(string patientId)
    {
        SqlCommand cmd = new SqlCommand("update HospitalPatients set DateOfDischarge='" +  DateTime.Now.ToString("MM/dd/yyyy") + "' where PatientId='" + patientId + "' and HospitalId=" + Convert.ToInt32(Session["LoginId"]) + "", con);
         cmd.ExecuteNonQuery();
    }

    protected void txtDischargeDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtDateOfAdmission.Text))
            {

                double days = (Convert.ToDateTime(txtDischargeDate.Text) - Convert.ToDateTime(txtDateOfAdmission.Text)).TotalDays;
                if (days <= 0)
                {
                    lblmsg.ForeColor = Color.Red;
                    lblmsg.Text = "Discharge date should be more than admitted date.";
                    txtDischargeDate.Focus();
                    return;
                }
                else
                {
                    lblmsg.Text = "";
                }
                txtTotalClearDays.Text = days.ToString();
            }
        }
        catch (Exception ex)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }

    }

    protected void ddlRoomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetRoomCostBasedOnRoomType(ddlRoomType.SelectedValue);
        }
        catch (Exception ex)
        {
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = ex.Message;
        }
    }

    void GetRoomCostBasedOnRoomType(string roomId)
    {
        if ( !string.IsNullOrEmpty(txtTreatmentCost.Text))
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from HospitalRoomDetail where roomId=" + roomId + " and HospitalId=" + Convert.ToInt32(Session["LoginId"]) + "", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtRentPerDay.Text = dt.Rows[0]["RentPerDay"].ToString();
                txtRentCost.Text = (Convert.ToDecimal(dt.Rows[0]["RentPerDay"]) * Convert.ToInt32(txtTotalClearDays.Text)).ToString();
                txtFinalCharges.Text = (Convert.ToDecimal(txtRentPerDay.Text) * Convert.ToInt32(txtTotalClearDays.Text) + Convert.ToDecimal(txtTreatmentCost.Text)).ToString();
            }
            else
            {
                txtRentCost.Text = "0.00";
                txtRentPerDay.Text = "0.00";
                txtFinalCharges.Text = (Convert.ToDecimal(txtRentPerDay.Text) * Convert.ToInt32(txtTotalClearDays.Text) + Convert.ToDecimal(txtTreatmentCost.Text)).ToString();
            }
        }
    }


    protected void txtTreatmentCost_TextChanged(object sender, EventArgs e)
    {

        decimal treatmentCost = 0, totalRentCost = 0, totalFinalCost = 0;
        treatmentCost = Convert.ToDecimal(txtTreatmentCost.Text);
        totalRentCost= Convert.ToDecimal(txtRentCost.Text);
        totalFinalCost = treatmentCost + totalRentCost;
        txtFinalCharges.Text = totalFinalCost.ToString();
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
        BtnSave.Text = "Save";
        lblmsg.Text = "";
    }

    private void GetTreatmentTotal()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from PatientTreatments where PatientId='" + hfPatientIds.Value + "' and Discharged=0", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count>0)
        {
            txtTreatmentCost.Text = dt.Rows[0]["TotalCost"].ToString();
            txtFinalCharges.Text = Convert.ToDecimal(txtTreatmentCost.Text).ToString();
        }
        else
        {
            txtTreatmentCost.Text = "0.00";
            txtFinalCharges.Text = Convert.ToDecimal(txtTreatmentCost.Text).ToString();
        }
    }
}