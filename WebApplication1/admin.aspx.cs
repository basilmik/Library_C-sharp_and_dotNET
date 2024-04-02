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
    public partial class admin : System.Web.UI.Page
    {
        protected SqlConnection connect;
        protected string basa = "SELECT Books.BookID, Name AS name, YEAR(YearPublished) as year FROM Books ";
        protected string basa2 = "SELECT Books.BookID, Name AS name, YEAR(YearPublished) as year ";
        protected void Page_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection("Data Source = DESKTOP-6ES09P2\\SQLEXPRESS; Initial Catalog = library; Integrated Security = True");



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

        protected string get_book_name_for_edit(object v)
        {
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }
            string res = "";
            string sqlQuery = "SELECT Name AS name FROM Books WHERE Books.BookID = "
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
            connect.Close();
            return res;
        }


        protected void edit_book(object sender, EventArgs e)
        {
            //if (connect.State != ConnectionState.Open)
            //{
            //    connect.Open();
            //}

            //get_book_name_for_edit(Eval("BookID"))


            //string sqlQuery = basa2 + ", Books.Description FROM Books WHERE Books.BookID = "
            //    + ((Button)sender).CommandArgument.ToString();

            //var mycom = new SqlCommand();
            //mycom.CommandText = sqlQuery;
            //mycom.Connection = connect;

            //SqlDataReader reader = mycom.ExecuteReader();

            //if (reader.HasRows) // если есть данные
            //{
            //    while (reader.Read()) // построчно считываем данные
            //    {
            //        book_name_info.Text = reader.GetValue(1).ToString();
            //        book_year_info.Text = reader.GetValue(2).ToString();
            //        book_desc_info.Text = reader.GetValue(3).ToString();
            //    }
            //}

            //reader.Close();

            //connect.Close();

            //genres_label_info.Text = get_genres(((Button)sender).CommandArgument.ToString());
            //authors_label_info.Text = get_authors(((Button)sender).CommandArgument.ToString());

        }


    }
}