using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    //bool bMode = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        //LoadEmployees();
        if (this.IsPostBack == false)
        {
        //        ViewState["bMode"] = true;
        ////    //ViewState["Search"] = "";
                LoadEmployees();
        } 
        //FillEmployees();   
    }
  
    void InsertEmployee()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = @"server=.\SQLEXPRESS2014;database=EmployeeDB;uid=sa;pwd=namlai120";
        //con.ConnectionString = @"server=.\SQLEXPRESS;database=BR004;uid=sa;pwd=123456";

        con.Open();
        //MessageBox.Show("success");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        string sql =
        string.Format("INSERT INTO Employees VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7})",
                            txtFullname.Text, txtDateOfBirth.Text, 
                            RBLGender.SelectedValue, DDLNational.SelectedValue,
                            txtPhone.Text, txtAddress.Text, 
                            ddlQualification.SelectedValue,
                             txtSalary.Text);
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        con.Close();

    }
    void UpdateEmployee()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = @"server=.\SQLEXPRESS2014;database=EmployeeDB;uid=sa;pwd=namlai120";
        //con.ConnectionString = @"server=.\SQLEXPRESS;database=BR004;uid=sa;pwd=123456";

        con.Open();
        //MessageBox.Show("success");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        string sql =
        string.Format("UPDATE Employees SET Fullname='{0}',DateOfBirth='{1}',Gender='{2}'," +
           "[National]='{3}',Qualification='{4}',Address='{5}',Salary={6} WHERE ID={7}",
                        txtFullname.Text, txtDateOfBirth.Text, RBLGender.SelectedValue, 
                        DDLNational.Text,ddlQualification.Text, 
                        txtAddress.Text,txtSalary.Text, lblID.Text);
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();

    }
    void LoadEmployees()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = @"server=.\SQLEXPRESS2014;database=EmployeeDB;uid=sa;pwd=namlai120";

        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employees", con);
        DataTable dt = new DataTable("Employees");
        da.Fill(dt);

        gvEmployees.AutoGenerateSelectButton = true;
        gvEmployees.AllowPaging = true;
        //gvEmployees.PageSize = 5;
        
        gvEmployees.DataSource = dt;
        gvEmployees.DataBind();
        
    }
    //protected void gvEmployee_SelectedIndexChanged(object sender, EventArgs e)
    //{
        

    //    //bMode = false;
    //    ViewState["bMode"] = false;
    //}
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //ViewState["bMode"] = true;
    }
    
    protected void btnSave_Click2(object sender, EventArgs e)
    {
        bool bMode = (bool)ViewState["bMode"];
        //bool bMode = bool.Parse(txtMode.Text);
        if (bMode == true)//Insert
        {
            InsertEmployee();
            LoadEmployees();
        }
        else//Update
        {
            UpdateEmployee();
            LoadEmployees();
        }
    }
    
    protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LoadEmployees();
        gvEmployees.PageIndex = e.NewPageIndex;
        gvEmployees.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void gvEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow r = gvEmployees.SelectedRow;
        lblID.Text = r.Cells[1].Text;
        txtFullname.Text = Server.HtmlDecode(r.Cells[2].Text);
        txtDateOfBirth.Text = r.Cells[3].Text;

        if (r.Cells[4].Text.Equals("M"))
            RBLGender.SelectedIndex = 0;
        else
            RBLGender.SelectedIndex = 1;

        DDLNational.SelectedItem.Text = r.Cells[5].Text;
        ddlQualification.SelectedItem.Text = r.Cells[6].Text;
        txtAddress.Text = Server.HtmlDecode(r.Cells[7].Text);
        txtSalary.Text = r.Cells[8].Text;

        //bMode = false;
        //txtMode.Text = "false";
        ViewState["bMode"] = false;
    }
    protected void btnNew_Click1(object sender, EventArgs e)
    {
        txtFullname.Text = "";
        txtPhone.Text = "";
        txtSalary.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //InsertEmployee();
        LoadEmployees();

        string msgScript = "<script>alert('Adding successful');</script>";
        //lblMsg.Text = msgScript;
        Response.Write(msgScript);
        //lblMsg.Text = msgScript;
        //Response.Redirect("");

    }
    protected void gvEmployees_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GridViewRow r = gvEmployees.SelectedRow;
        lblID.Text = r.Cells[1].Text;
        txtFullname.Text = Server.HtmlDecode(r.Cells[2].Text);
        txtDateOfBirth.Text = r.Cells[3].Text;

        if (r.Cells[4].Text.Equals("M"))
            RBLGender.SelectedIndex = 0;
        else
            RBLGender.SelectedIndex = 1;

        DDLNational.SelectedItem.Text = r.Cells[5].Text;
        txtPhone.Text = r.Cells[6].Text;
        txtAddress.Text = Server.HtmlDecode(r.Cells[7].Text);
        ddlQualification.SelectedItem.Text = r.Cells[8].Text;
        
        txtSalary.Text = r.Cells[9].Text;
    }
    protected void txtFullname_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //UpdateEmployee();
        LoadEmployees();
    }
    protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
