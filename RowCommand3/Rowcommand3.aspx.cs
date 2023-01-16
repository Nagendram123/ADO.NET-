using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace RowCommand3
{
    public partial class Rowcommand3 : System.Web.UI.Page
    {
        void GetData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            string q = "select empid,empname,salary from emps5";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "emps5");
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack==false)
            {
                GetData();
            }
        }

      

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                Label l2 = (Label)row.FindControl("Label2");
                Label l3 = (Label)row.FindControl("Label3");
                TextBox1.Text = l1.Text;
                TextBox2.Text = l2.Text;
                TextBox3.Text = l3.Text;
                BtnInsertUpdate.Text = "Update";
            }
            else if(e.CommandName=="cmdDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label l1 = (Label)row.FindControl("Label1");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                con.Open();
                string q = "delete from emps5 where empid='"+l1.Text+"'";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();
                GetData();
            }
        }

        

        protected void BtnInsertUpate_Click(object sender, EventArgs e)
        {
            string q = "";
            if (BtnInsertUpdate.Text == "Insert")
            {
                q = "insert into emps5 values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "')";
            }
            else if (BtnInsertUpdate.Text == "Update")
            {
                q = "update emps5 set empname='" + TextBox2.Text + "', salary='" + TextBox3.Text + "' where empid='" + TextBox1.Text + "'";
                BtnInsertUpdate.Text = "Insert";

            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            con.Close();
            GetData();
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";


        }

    }
}