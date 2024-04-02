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

        protected string get_book_year_for_edit(object v)
        {
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }
            string res = "";
            string sqlQuery = "SELECT Year(YearPublished) AS year FROM Books WHERE Books.BookID = "
                + v.ToString();

            var mycom = new SqlCommand();
            mycom.CommandText = sqlQuery;
            mycom.Connection = connect;
            SqlDataReader reader = mycom.ExecuteReader();

            if (reader.HasRows) // если есть данные
                while (reader.Read()) // построчно считываем данные
                    res = reader.GetValue(0).ToString();

            reader.Close();
            connect.Close();
            return res;
        }

        protected string get_book_desc_for_edit(object v)
        {
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }
            string res = "";
            string sqlQuery = "SELECT Description AS year FROM Books WHERE Books.BookID = "
                + v.ToString();

            var mycom = new SqlCommand();
            mycom.CommandText = sqlQuery;
            mycom.Connection = connect;
            SqlDataReader reader = mycom.ExecuteReader();

            if (reader.HasRows) // если есть данные
                while (reader.Read()) // построчно считываем данные
                    res = reader.GetValue(0).ToString();

            reader.Close();
            connect.Close();
            return res;
        }

        protected List<String> get_checked_authors_list(object v)
        {
            List<String> checked_authors = new List<String>();
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }

            string sqlQuery =
                "SELECT [AuthorNameSurname], [BookID], [AuthorID] FROM [books_authors_view] WHERE books_authors_view.BookID = " + v;
            test_label.Text = sqlQuery;
            var mycom = new SqlCommand();
            mycom.CommandText = sqlQuery;
            mycom.Connection = connect;
            SqlDataReader reader = mycom.ExecuteReader();

            if (reader.HasRows) // если есть данные
                while (reader.Read()) // построчно считываем данные
                    checked_authors.Add(reader.GetValue(0).ToString());



            reader.Close();
            connect.Close();
            return checked_authors;
        }


        protected List<String> get_checked_genres_list(object v)
        {
            List<String> checked_genres = new List<String>();
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }

            string sqlQuery =
                "SELECT [GenreName] as name, [BookID], [GenreID] FROM [books_genres_view] WHERE books_genres_view.BookID = " + v;
            
            

            var mycom = new SqlCommand();
            mycom.CommandText = sqlQuery;
            mycom.Connection = connect;
            SqlDataReader reader = mycom.ExecuteReader();

            if (reader.HasRows) // если есть данные
                while (reader.Read()) // построчно считываем данные
                {
                    checked_genres.Add(reader.GetValue(0).ToString());

                    test_label.Text = reader.GetValue(0).ToString();
                }
            reader.Close();
            connect.Close();
            return checked_genres;
        }
        protected void set_edit_visible()
        {


            EditPlaceHolder.Visible = true;
        }

        protected void edit_book(object sender, EventArgs e)
        {
            string book_id = ((Button)sender).CommandArgument.ToString();
            List<String> checked_authors = get_checked_authors_list(book_id);
            List<String> checked_genres = get_checked_genres_list(book_id);
            set_edit_visible();


            book_name_edit.Text = get_book_name_for_edit(book_id);
            year_edit.Text = get_book_year_for_edit(book_id);
            desc_edit.Text = get_book_desc_for_edit(book_id);



            ListItemCollection all_list = author_edit.Items;
            for (int count = 0; count < all_list.Count; count++)
            {

                if (checked_authors.Contains(all_list[count].ToString()))
                {
                    all_list[count].Selected = true;
                }
                else
                    all_list[count].Selected = false;
            }


            ListItemCollection all_genres_list = genres_edit.Items;
            for (int count = 0; count < all_list.Count; count++)
            {

                if (checked_genres.Contains(all_genres_list[count].ToString()))
                {
                    all_genres_list[count].Selected = true;
                }
                else
                    all_genres_list[count].Selected = false;
            }


        }


    }
}