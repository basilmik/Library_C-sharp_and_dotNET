using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected SqlConnection connect;

        protected void Page_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection("Data Source = DESKTOP-6ES09P2\\SQLEXPRESS; Initial Catalog = library; Integrated Security = True");



            if (string.IsNullOrEmpty(Session["is_in"] as string))
            {
                Session["is_in"] = "no";
            }
            if (string.IsNullOrEmpty(Session["userID"] as string))
            {
                Session["userID"] = "-1";
            }
            

            if (Convert.ToString(Session["is_in"]) == "no")
            {
                //auth_inform.Text = "is not in";//Convert.ToString(Session["is_in"]);
                enter_form_div.Visible = true;
                auth_div.Visible = true;

                register_div.Visible = false;

                account_control_div.Visible = false;
                Button4.Visible = false;

            }

            if (Convert.ToString(Session["is_in"]) == "yes")
            {
                auth_div.Visible = false;
                register_div.Visible = false;
                enter_form_div.Visible = false;

                account_control_div.Visible = true;
                Button4.Visible = true;

            }
            auth_inform.Text = "";
            auth_inform.Text = Convert.ToString(Session["is_in"]);
        }


        protected void run_query(string query)
        {
            if (connect.State != ConnectionState.Open)
                connect.Open();
            var mycom = new SqlCommand();
            mycom.CommandText = query;
            mycom.Connection = connect;
            try
            {
                mycom.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception e)
            {
                string error_m = "An error occurred: " + e.Message;
                auth_inform.Text = error_m;
                throw e;
            }

            connect.Close();
        }



        protected void open_registerCLick(object sender, EventArgs e)
        {
            auth_div.Visible = false;
            register_div.Visible = true;
        }

        protected void enterCLick(object sender, EventArgs e)
        {
            string login = user_login.Text;
            string password = user_pass.Text;

            bool all_filled = login.Any() && password.Any();

            if (!all_filled)
            {
                auth_inform.Text = "Заполните все поля.";
                return;
            }
            string auth_sql = "SELECT UserID FROM Users WHERE Login='" + login + "' AND Password='" + password + "'";
            auth_inform.Text = auth_sql;

            if (connect.State != ConnectionState.Open)
                connect.Open();

            SqlDataReader reader;
            try
            {
                var mycom = new SqlCommand();
                mycom.CommandText = auth_sql;
                mycom.Connection = connect;
                reader = mycom.ExecuteReader();
            }
            catch
            {
                auth_inform.Text = "Ошибка " + auth_sql;
                connect.Close();
                return;
            }

            if (reader.HasRows) // если есть данные
            {
                reader.Read();
                Session["is_in"] = "yes";
                Session["userID"] = reader.GetValue(0).ToString();
                auth_div.Visible = false;
                register_div.Visible = false;
                enter_form_div.Visible = false;

                account_control_div.Visible = true;
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                auth_inform.Text = "Такого пользователя не существует. Проверьте данные.";
            }
            reader.Close();
        }

        protected void cancel_registerCLick(object sender, EventArgs e)
        {
            user_login_reg.Text = "";
            user_pass_reg.Text = "";
            user_name_reg.Text = "";
            user_surname_reg.Text = "";
            user_email_reg.Text = "";
            user_codeword_reg.Text = "";

            register_div.Visible = false;
            auth_div.Visible = true;

        }


        protected void do_registerCLick(object sender, EventArgs e)
        {
            string login = user_login_reg.Text;
            string password = user_pass_reg.Text;
            //string name = user_name_reg.Text;
            //string surname = user_surname_reg.Text;
            //string email = user_email_reg.Text;
            //string codeword = user_codeword_reg.Text;

            bool all_filled = login.Any() && password.Any();// && name.Any() && surname.Any() && email.Any() && codeword.Any();

            if (!all_filled)
            {
                auth_inform.Text = "Заполните все поля.";
                auth_div.Visible = false;
                register_div.Visible = true;
                return;
            }
            string check_sql = "SELECT UserID FROM Users WHERE Login='" + login + "'";

            if (connect.State != ConnectionState.Open)
                connect.Open();

            SqlDataReader reader;
            try
            {
                var mycom = new SqlCommand();
                mycom.CommandText = check_sql;
                mycom.Connection = connect;
                reader = mycom.ExecuteReader();
            }
            catch
            {
                auth_inform.Text = "Ошибка " + check_sql;
                connect.Close();
                return;
            }

            if (reader.HasRows) // если есть данные
            {
                auth_inform.Text = "Такой логин уже зарегистрирован. Попробуйте другой.";
                user_login_reg.Text = "";
                user_pass_reg.Text = "";

                enter_form_div.Visible = true;
                auth_div.Visible = false;
                register_div.Visible = true;
            }
            else
            {
                reader.Close();
                string insert_sql = "INSERT INTO Users (Login, Password) VALUES ('" + login + "', '" + password + "')";

                try
                {
                    run_query(insert_sql);
                }
                catch
                {
                    auth_inform.Text = "Ошибка " + insert_sql;
                    return;
                }

                //auth_inform.Text = insert_sql;
            }



        }

        protected void exit_acc_CLick(object sender, EventArgs e)
        {

            Session["is_in"] = "no";
            Session["userID"] = "-1";
            auth_inform.Text = "exit acc";//Convert.ToString(Session["is_in"]);

            enter_form_div.Visible = true;
            auth_div.Visible = true;

            register_div.Visible = false;

            account_control_div.Visible = false;


            if (Request.RawUrl != "/Index.aspx" && Request.RawUrl != "/katalog.aspx")
                Response.Redirect("Index.aspx");
            else
                Response.Redirect(Request.RawUrl);
        }











        protected void open_accountClick(Object sender, EventArgs e)
        {

            Response.Redirect("account.aspx");
        }

        protected void open_ordersClick(Object sender, EventArgs e)
        {

            Response.Redirect("orders.aspx");
        }


        protected void test_Click(Object sender, EventArgs e)
        {
            Label1.Text = "БИБЛИОТЕКА!";
        }

        protected void refresh_Click(Object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void main_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
        protected void katalog_Click(Object sender, EventArgs e)
        {
            Response.Redirect("katalog.aspx");
        }
        protected void admin_Click(Object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx");
        }

    }

}