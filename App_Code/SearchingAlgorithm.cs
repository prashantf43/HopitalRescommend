using System.Configuration;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for SearchingAlgorithm
/// </summary>
public class SearchingAlgorithm
{
    SqlDataAdapter da;
    public SearchingAlgorithm()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetHospital(int diseaseId, int treatmentId, decimal fromCost, decimal toCost, FilterType filterType, int facilityId, int rating = 0, int doctorId = 0, int cityId = 0, int stateId = 0, int districtId = 0, int talukaId = 0)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
        {
            if (FilterType.ByCost == filterType)
            {
                if ((fromCost == 0 || fromCost > 0) && toCost > 0 && doctorId > 0)
                {
                    da = new SqlDataAdapter("select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId inner join HospitalDoctors HD on HD.HospitalId=HR.HospitalId where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + " and HT.Cost between " + fromCost + " and " + toCost + " and HD.DoctorId=" + doctorId + ")", connection);
                }

                else if (fromCost == 0 && toCost == 0 && doctorId > 0)
                {
                    da = new SqlDataAdapter("select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId inner join HospitalDoctors HD on HD.HospitalId=HR.HospitalId where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + " and HD.DoctorId=" + doctorId + ")", connection);
                }

                else if (fromCost == 0 && toCost == 0)
                {
                    da = new SqlDataAdapter("select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + ")", connection);
                }
                else
                {
                    da = new SqlDataAdapter("select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + " and HT.Cost between " + fromCost + " and " + toCost + ")", connection);
                }
            }
            else if (FilterType.ByTreatment == filterType)
            {
                da = new SqlDataAdapter("select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + " )", connection);
            }
            else if (FilterType.ByFacility == filterType)
            {
                da = new SqlDataAdapter("select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId inner join HospitalFacility HF on HF.HospitalId=HR.HospitalId where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + " ) and HF.FacilityId=" + facilityId + "", connection);
            }

            else if (FilterType.ByRating == filterType)
            {
                if (facilityId > 0)
                {
                    da = new SqlDataAdapter("with CTE as ( select AVG(r.rating) as totalrating, HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State], DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR  inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId inner join HospitalFacility HF on HF.HospitalId=HR.HospitalId left join rating r on r.HospitalId=HR.HospitalId where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId =" + treatmentId + " ) and HF.FacilityId=" + facilityId + " group by HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost) select * from CTE where totalrating=" + rating + "", connection);
                }
                da = new SqlDataAdapter("with CTE as ( select AVG(r.rating) as totalrating, HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State], DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR  inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId left join rating r on r.HospitalId=HR.HospitalId where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId =" + treatmentId + " )  group by HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost) select * from CTE where totalrating=" + rating + "", connection);
            }
            else if (FilterType.ByDoctor == filterType)
            {
                da = new SqlDataAdapter("select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State], DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost   from HospitalRegistration HR   inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId    inner join StateMaster SM on SM.StateId = HR.StateId    inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId    inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId     inner join CityMaster CM on CM.CityId = HR.CityId inner join HospitalDoctors HD on HD.HospitalId=HR.HospitalId where HD.DoctorId=" + doctorId + " and HD.DiseaseId=" + diseaseId + "", connection);
            }
            else if (FilterType.ByLocation == filterType)
            {
                
                    da = new SqlDataAdapter("select HR.HospitalId,HR.HospitalName,HR.HospitalEmail,HR.PhoneNumber,SM.[State],DM.District,TM.Taluka,CM.City,HR.Address,HT.Cost from HospitalRegistration HR inner join HospitalTreatment HT on HT.HospitalId = HR.HospitalId inner join StateMaster SM on SM.StateId = HR.StateId inner join DistrictMaster DM on DM.DistrictId = HR.DistrictId inner join TalukaMaster TM on Tm.TalukaId = HR.TalukaId inner join CityMaster CM on CM.CityId = HR.CityId  where (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + "  and HR.StateId=" + stateId + "and HR.DistrictId=" + districtId + " and HR.TalukaId=" + talukaId + " and HR.CityId=" + cityId + ") or (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + "  and HR.StateId=" + stateId + "and HR.DistrictId=" + districtId + " and HR.TalukaId=" + talukaId + ") or (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + "  and HR.StateId=" + stateId + "and HR.DistrictId=" + districtId + ") or (HT.DiseaseId = " + diseaseId + " and HT.TreatmentId = " + treatmentId + "  and HR.StateId=" + stateId + ")", connection);
            }

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}