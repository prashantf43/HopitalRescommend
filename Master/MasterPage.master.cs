using System;
using System.Data;

public partial class Master_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoginId"] != null && Session["Role"] != null)
        {
            User user = new User();
            if (Session["Role"].ToString().Equals("Admin"))
            {
                DataTable dt = user.GetUserDetail("Admin", Convert.ToInt32(Session["LoginId"]));
                string userName = dt.Rows[0]["FullName"].ToString();
                lblUserName.Text = "Welcome - " + userName;
            }
            else if (Session["Role"].ToString().Equals("Hospital"))
            {
                DataTable dt = user.GetUserDetail("Hospital", Convert.ToInt32(Session["LoginId"]));
                string userName = dt.Rows[0]["HospitalName"].ToString();
                lblUserName.Text = "Welcome - " + userName;
            }
            else if (Session["Role"].ToString().Equals("Guest"))
            {
                lblUserName.Text = "Welcome - Guest";
            }
            HideShowMenu();
        }
        else
        {
            Response.Redirect("~/SessionExpired.aspx");
        }
    }

    private void HideShowMenu()
    {
        if (Session["LoginId"] != null && Session["Role"] != null)
        {
            if (Session["Role"].ToString().Equals("Admin"))
            {
                AdminDashboardId.Visible = true;
                HospitalDashboardId.Visible = false;
                HospitalRegistrationId.Visible = true;
                FacilityMasterId.Visible = true;
                DiseaseMasterId.Visible = true;
                TreatmentMasterId.Visible = true;
                DoctorMasterId.Visible = true;
                RoomMasterId.Visible = true;
                PatientStatusMasterId.Visible = true;
                AddFacilityId.Visible = false;
                AddRoomDetailId.Visible = false;
                AddTreatmentAvaliableId.Visible = false;
                AddDoctorAvaliableId.Visible = false;
                AddPatientId.Visible = false;
                PatientTreatmentId.Visible = false;
                DischargePatientId.Visible = false;
                ViewRiewId.Visible = false;
                PatientRegistrationId.Visible = true;
                ReviewId.Visible = false;
                PatientDashBoardId.Visible = false;
                StateMasterId.Visible = true;
                DistrictMasterId.Visible = true;
                TalukaMasterId.Visible = true;
                CityMasterId.Visible = true;
                SearchHospitalByTreatmentId.Visible = false;
                TreatmentsId.Visible = false;
            }
            else if (Session["Role"].ToString().Equals("Hospital"))
            {
                AdminDashboardId.Visible = false;
                HospitalDashboardId.Visible = true;
                HospitalRegistrationId.Visible = false;
                FacilityMasterId.Visible = false;
                DiseaseMasterId.Visible = false;
                TreatmentMasterId.Visible = false;
                DoctorMasterId.Visible = false;
                RoomMasterId.Visible = false;
                PatientStatusMasterId.Visible = false;
                AddFacilityId.Visible = true;
                AddRoomDetailId.Visible = true;
                AddTreatmentAvaliableId.Visible = true;
                AddDoctorAvaliableId.Visible = true;
                AddPatientId.Visible = true;
                PatientTreatmentId.Visible = true;
                DischargePatientId.Visible = true;
                ViewRiewId.Visible = true;
                PatientRegistrationId.Visible = false;
                ReviewId.Visible = false;
                PatientDashBoardId.Visible = false;
                StateMasterId.Visible = false;
                DistrictMasterId.Visible = false;
                TalukaMasterId.Visible = false;
                CityMasterId.Visible = false;
                SearchHospitalByTreatmentId.Visible = false;
                TreatmentsId.Visible = false;
            }
            else if (Session["Role"].ToString().Equals("Patient"))
            {
                AdminDashboardId.Visible = false;
                HospitalDashboardId.Visible = false;
                HospitalRegistrationId.Visible = false;
                FacilityMasterId.Visible = false;
                DiseaseMasterId.Visible = false;
                TreatmentMasterId.Visible = false;
                DoctorMasterId.Visible = false;
                RoomMasterId.Visible = false;
                PatientStatusMasterId.Visible = false;
                AddFacilityId.Visible = false;
                AddRoomDetailId.Visible = false;
                AddTreatmentAvaliableId.Visible = false;
                AddDoctorAvaliableId.Visible = false;
                AddPatientId.Visible = false;
                PatientTreatmentId.Visible = false;
                DischargePatientId.Visible = false;
                ViewRiewId.Visible = false;
                PatientRegistrationId.Visible = false;
                ReviewId.Visible = false;
                PatientDashBoardId.Visible = true;
                StateMasterId.Visible = false;
                DistrictMasterId.Visible = false;
                TalukaMasterId.Visible = false;
                CityMasterId.Visible = false;
                SearchHospitalByTreatmentId.Visible = false;
                TreatmentsId.Visible = true;
            }
            else if (Session["Role"].ToString().Equals("Guest"))
            {
                AdminDashboardId.Visible = false;
                HospitalDashboardId.Visible = false;
                HospitalRegistrationId.Visible = false;
                FacilityMasterId.Visible = false;
                DiseaseMasterId.Visible = false;
                TreatmentMasterId.Visible = false;
                DoctorMasterId.Visible = false;
                RoomMasterId.Visible = false;
                PatientStatusMasterId.Visible = false;
                AddFacilityId.Visible = false;
                AddRoomDetailId.Visible = false;
                AddTreatmentAvaliableId.Visible = false;
                AddDoctorAvaliableId.Visible = false;
                AddPatientId.Visible = false;
                PatientTreatmentId.Visible = false;
                DischargePatientId.Visible = false;
                ViewRiewId.Visible = false;
                PatientRegistrationId.Visible = false;
                ReviewId.Visible = false;
                PatientDashBoardId.Visible = false;
                StateMasterId.Visible = false;
                DistrictMasterId.Visible = false;
                TalukaMasterId.Visible = false;
                CityMasterId.Visible = false;
                ChangePasswordId.Visible = false;
                SearchHospitalByTreatmentId.Visible = true;
                TreatmentsId.Visible = false;
            }
            }
        else
        {
            Response.Redirect("~/SessionExpired.aspx");
        }
    }
    protected void LinkBtnLogout_Click(object sender, EventArgs e)
    {
        if (Session["Role"].ToString().Equals("Admin"))
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
        else if (Session["Role"].ToString().Equals("Hospital"))
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/HospitalLogin.aspx");
        }
        else if (Session["Role"].ToString().Equals("Patient"))
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/PatientLogin.aspx");
        }
        else if (Session["Role"].ToString().Equals("Guest"))
        {
            Response.Redirect("~/Default.aspx");
        }
      
    }
}
