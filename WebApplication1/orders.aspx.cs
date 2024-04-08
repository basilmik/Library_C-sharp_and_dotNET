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
    public partial class orders : System.Web.UI.Page
    {
        protected SqlConnection connect;
        protected string basa = "SELECT Books.BookID, Name AS name, YEAR(YearPublished) AS year FROM Books ";
        protected string basa2 = "SELECT Books.BookID, Name AS name, YEAR(YearPublished) as year ";
        protected void Page_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection("Data Source = DESKTOP-6ES09P2\\SQLEXPRESS; Initial Catalog = library; Integrated Security = True");

            if (string.IsNullOrEmpty(Session["userID"] as string))
            {
                Session["userID"] = "-1";
            }
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

        protected void cancel_orderClick(object sender, EventArgs e)
        {
            string orderID = ((Button)sender).CommandArgument.ToString();
            string sql = "UPDATE [Order] SET OrderStateID = '5' WHERE OrderID = '" + orderID+"'";
            Label1.Text = sql;
            run_query(sql);
            Response.Redirect(Request.RawUrl);
        }

        protected void choose_book(object sender, EventArgs e)
        {
            detail_info.Visible = true;
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }

            string sqlQuery = basa2 + ", Books.Description FROM Books WHERE Books.BookID = "
                + ((Button)sender).CommandArgument.ToString();

            var mycom = new SqlCommand();
            mycom.CommandText = sqlQuery;
            mycom.Connection = connect;

            SqlDataReader reader = mycom.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    book_name_info.Text = reader.GetValue(1).ToString();
                    book_year_info.Text = reader.GetValue(2).ToString();
                    book_desc_info.Text = reader.GetValue(3).ToString();
                }
            }

            reader.Close();

            connect.Close();

            genres_label_info.Text = get_genres(((Button)sender).CommandArgument.ToString());
            authors_label_info.Text = get_authors(((Button)sender).CommandArgument.ToString());

        }

        protected string get_authors(object v)
        {
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }
            string res = "";
            string sqlQuery = "SELECT string_agg(books_authors_view.AuthorNameSurname, ', ') as authors "
               + " FROM books_authors_view "
               + " JOIN Books ON Books.BookID = books_authors_view.BookID "
               + " WHERE Books.BookID = "
               + v.ToString();

            var mycom = new SqlCommand();
            mycom.CommandText = sqlQuery;
            mycom.Connection = connect;

            SqlDataReader reader = mycom.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    res = reader.GetValue(0).ToString();
                }
            }

            reader.Close();
            return res;
        }


        protected string get_genres(object v)
        {
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }
            string res = "";
            string sqlQuery = "SELECT string_agg(books_genres_view.GenreName, ', ') as genres "
               + " FROM books_genres_view  "
               + " JOIN Books ON Books.BookID = books_genres_view.BookID "
               + " WHERE Books.BookID = "
               + v.ToString();

            var mycom = new SqlCommand();
            mycom.CommandText = sqlQuery;
            mycom.Connection = connect;

            SqlDataReader reader = mycom.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    res = reader.GetValue(0).ToString();
                }
            }

            reader.Close();
            return res;
        }

        protected void close_details(object sender, EventArgs e)
        {
            detail_info.Visible = false;
        }
            


    }
}