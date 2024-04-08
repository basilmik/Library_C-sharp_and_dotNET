using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class account : System.Web.UI.Page
    {
        protected SqlConnection connect;

        protected void Page_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection("Data Source = DESKTOP-6ES09P2\\SQLEXPRESS; Initial Catalog = library; Integrated Security = True");

            if (string.IsNullOrEmpty(Session["userID"] as string))
            {
                Session["userID"] = "-1";
            }


        }

        protected void load_textboxes()
        {
           
            // account_inform.Text = Convert.ToString(Session["userID"]);
            string userID = Convert.ToString(Session["userID"]);
            string sql = "SELECT * FROM Users WHERE UserID=" + userID;
            //account_inform.Text = sql;

            if (connect.State != ConnectionState.Open)
                connect.Open();

            SqlDataReader reader;
            try
            {
                var mycom = new SqlCommand();
                mycom.CommandText = sql;
                mycom.Connection = connect;
                reader = mycom.ExecuteReader();
            }
            catch
            {
                account_inform.Text = "Ошибка " + sql;
                connect.Close();
                return;
            }

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    name_textbox.Text = reader.GetValue(1).ToString();
                    surname_textbox.Text = reader.GetValue(2).ToString();

                    login_textbox.Text = reader.GetValue(3).ToString();
                    login_textbox.ReadOnly = true;

                    pass_textbox.Text = reader.GetValue(4).ToString();
                    email_textbox.Text = reader.GetValue(5).ToString();
                    codeword_textbox.Text = reader.GetValue(6).ToString();
                }

            }

            reader.Close();
            connect.Close();

        }


        protected void run_query(string query)
        {
            if (connect.State != ConnectionState.Open)
                connect.Open();
            var mycom = new SqlCommand();
            mycom.CommandText = query;
            mycom.Connection = connect;

            mycom.ExecuteNonQuery();
            connect.Close();

        }


        protected void save_changesClick(object sender, EventArgs e)
        {
            //Session["name"] = name_textbox.Text;

            string name = name_textbox.Text;
            string surname = surname_textbox.Text;
            string login = login_textbox.Text;
            string pass = pass_textbox.Text;
            string email = email_textbox.Text;
            string codeword = codeword_textbox.Text;

            string sql = "UPDATE Users SET Name='" + name + "', Surname='" + surname + "', Password='" + pass + "', email='" + email + "', Codeword='" + codeword + "' WHERE UserID=" + Convert.ToString(Session["userID"]);

            run_query(sql);
            //load_textboxes();
            //Response.Redirect(Request.RawUrl);
        }
 
        protected void cancel_changesClick(object sender, EventArgs e)
        {
            load_textboxes();
        } 

        protected void show_dataClick(object sender, EventArgs e)
        {
            load_textboxes();
            show_data_btn.Visible = false;
            save_changes_btn.Visible = true;
            cancel_changes_btn.Visible = true;
            close_data.Visible = true;
        }
        protected void close_dataClick(object sender, EventArgs e)
        {
            name_textbox.Text = "";
            surname_textbox.Text = "";
            login_textbox.Text = "";
            pass_textbox.Text = "";
            email_textbox.Text = "";
            codeword_textbox.Text = "";

            show_data_btn.Visible = true;
            save_changes_btn.Visible = false;
            cancel_changes_btn.Visible = false;
            close_data.Visible = false;
        }

        

    }
}