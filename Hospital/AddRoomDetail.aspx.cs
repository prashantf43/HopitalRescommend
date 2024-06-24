using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Hospital_AddRoomDetail : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRoomTypes();
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
                SqlCommand cmd = new SqlCommand("insert into HospitalRoomDetail values('" + ddlRoomType.SelectedValue + "','" + txtRent.Text + "'," + Convert.ToInt32(Session["LoginId"]) + ")", con);
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
                SqlCommand cmd = new SqlCommand("update HospitalRoomDetail set RoomId='" + ddlRoomType.SelectedValue + "',RentPerDay='" + txtRent.Text + "' where RoomDetailId=" + HfId.Value + "", con);
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
        SqlDataAdapter da = new SqlDataAdapter("Select * from HospitalRoomDetail where RoomId='" + ddlRoomType.SelectedValue + "' and HospitalId='" + Session["LoginId"].ToString() + "' and RoomDetailId!='" + HfId.Value + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Already Exist");
        }
    }


    void GetRecord()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select HRD.RoomDetailId,HRD.RoomId,RM.RoomType,HRD.RentPerDay from HospitalRoomDetail HRD left join RoomMaster RM on RM.RoomId=HRD.RoomId where HospitalId=" + Convert.ToInt32(Session["LoginId"]) + " order by HRD.RoomDetailId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

    void GetRoomTypes()
    {
        SqlDataAdapter da = new SqlDataAdapter("Select * from RoomMaster order by RoomId desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlRoomType.DataSource = dt;
        ddlRoomType.DataTextField = "RoomType";
        ddlRoomType.DataValueField = "RoomId";
        ddlRoomType.DataBind();
        ddlRoomType.Items.Insert(0, new ListItem("----Select-----", "0"));
        da.Dispose();
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Updates")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from HospitalRoomDetail where RoomDetailId='" + e.CommandArgument.ToString() + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                HfId.Value = dt.Rows[0]["RoomDetailId"].ToString();
                ddlRoomType.SelectedValue = dt.Rows[0]["RoomId"].ToString();
                txtRent.Text = dt.Rows[0]["RentPerDay"].ToString();
                BtnSave.Text = "Update";
            }

            else if (e.CommandName == "deletes")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from HospitalRoomDetail where RoomDetailId='" + e.CommandArgument.ToString() + "'", con);
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
        txtRent.Text = "";
        ddlRoomType.SelectedIndex = 0;
        BtnSave.Text = "Save";
        ddlRoomType.Focus();

    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        GetRecord();
    }
}