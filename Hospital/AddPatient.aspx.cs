using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Hospital_AddPatient : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtAadharNumber.Focus();
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
                SqlCommand cmd = new SqlCommand("insert into HospitalPatients(PatientId,DateOfAdmission,HospitalId) values('" + hfPatientIds.Value + "','" + Convert.ToDateTime(txtDateOfAdmission.Text).ToString("MM/dd/yyyy") + "'," + Convert.ToInt32(Session["LoginId"]) + ")", con);
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
                SqlCommand cmd = new SqlCommand("update HospitalPatients set PatientId='" + hfPatientIds.Value + "',DateOfAdmission='" + Convert.ToDateTime(txtDateOfAdmission.Text).ToString("MM/dd/yyyy") + "' where Id=" + HfId.Value + "", con);
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
        SqlDataAdapter da = new SqlDataAdapter("Select * from HospitalPatients where PatientId='" + hfPatientIds.Value + "' and DateOfDischarge is null and Id!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Patient is already admitted.Please discharge first!");
        }
    }

    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("select HP.Id,HP.PatientId,HP.DateOfAdmission,PR.AadharNumber,PR.Name,PR.Address,PR.DOB,PR.ContactNumber from HospitalPatients HP inner join PatientRegistration PR on PR.PatientId=HP.PatientId where HospitalId=" + Convert.ToInt32(Session["LoginId"]) + " order by HP.Id desc", con);
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
                SqlDataAdapter da = new SqlDataAdapter("select * from HospitalPatients where Id='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["Id"].ToString();
                GetPatientDetail(null, Convert.ToInt32(dt.Rows[0]["PatientId"].ToString()));
                txtDateOfAdmission.Text = Convert.ToDateTime(dt.Rows[0]["DateOfAdmission"]).ToString("dd-MM-yyyy");
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from HospitalPatients where Id='" + e.CommandArgument.ToString() + "'", con);
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
        txtAadharNumber.Text = "";
        txtName.Text = "";
        txtDOB.Text = "";
        txtAddress.Text = "";
        txtContactNumber.Text = "";
        txtDateOfAdmission.Text = "";
        BtnSave.Text = "Save";
        txtAadharNumber.Focus();
    }

    void GetPatientDetail(string aadharNumber,int patientId)
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from PatientRegistration where AadharNumber='" + aadharNumber + "' OR patientId=" + patientId + "", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hfPatientIds.Value = dt.Rows[0]["PatientId"].ToString();
            txtAadharNumber.Text = dt.Rows[0]["AadharNumber"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtDOB.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd-MM-yyyy");
            txtContactNumber.Text = dt.Rows[0]["ContactNumber"].ToString();
        }
        else
        {
            Clear();
            lblmsg.ForeColor = Color.Red;
            lblmsg.Text = "Patient Not Found";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetPatientDetail(txtAadharNumber.Text, 0);
        }
        catch {
        }
    }


    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}