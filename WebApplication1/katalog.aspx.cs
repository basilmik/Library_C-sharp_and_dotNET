using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class katalog1 : System.Web.UI.Page
    {
        protected SqlConnection connect;
        protected string basa = "SELECT Books.BookID, Name AS name, YEAR(YearPublished) AS year FROM Books ";
        protected string basa2 = "SELECT Books.BookID, Name AS name, YEAR(YearPublished) as year ";
        protected void Page_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection("Data Source = DESKTOP-6ES09P2\\SQLEXPRESS; Initial Catalog = library; Integrated Security = True");

            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }
            if (!IsPostBack)
            {
                DropDownAuthors.AppendDataBoundItems = true;
                DropDownAuthors.Items.Insert(0, new ListItem(String.Empty));
                DropDownAuthors.SelectedIndex = 0;

                DropDownGenres.AppendDataBoundItems = true;
                DropDownGenres.Items.Insert(0, new ListItem(String.Empty));
                DropDownGenres.SelectedIndex = 0;


                DropDownYear.AppendDataBoundItems = true;
                DropDownYear.Items.Insert(0, new ListItem(String.Empty, "-1"));
                DropDownYear.SelectedIndex = 0;


            }
            Session["IDY"] = this.DropDownYear.SelectedValue.ToString();


            //var mycom = new SqlCommand();
            //mycom.CommandText = "SELECT [BookID], [Name] as name, YEAR([YearPublished]) as year FROM [Books]" +
            //    " WHERE(@YearPublished = '-1' OR YEAR([YearPublished]) = @YearPublished)";
            //mycom.Connection = connect;
            //booksSource.SelectCommand = mycom.CommandText;

            //GridView1.DataBind();

            //test_Label.Text = booksSource.SelectCommand;


        }

        protected void hide_Click(object sender, EventArgs e)
        {
            GridView1.Visible = !GridView1.Visible;

        }

        protected void search_clear_Click(object sender, EventArgs e)
        {
            string crit = basa;

            DropDownAuthors.SelectedIndex = 0;
            DropDownGenres.SelectedIndex = 0;
            TextBoxName.Text = "";
            
        }

        protected void search_Click(object sender, EventArgs e)
        {
            string crit1;
            string crit = basa;


            if (!string.IsNullOrEmpty(DropDownAuthors.SelectedValue))
            {
                crit += " JOIN Book_Author ON Book_Author.bookID = Books.BookID ";

                crit1 = " Book_Author.AuthorID = " + DropDownAuthors.SelectedValue;

                crit = crit + " AND " + crit1;
            }

            if (!string.IsNullOrEmpty(DropDownGenres.SelectedValue))
            {
                crit += " JOIN Book_Genre ON Book_Genre.bookID = Books.BookID";
                crit1 = "Book_Genre.GenreID = " + DropDownGenres.SelectedValue;

                crit = crit + " AND " + crit1;
            }

            if (!string.IsNullOrEmpty(TextBoxName.Text))
            {
                crit1 = " Books.Name like '%" + TextBoxName.Text + "%'";
                crit = crit + " AND " + crit1;
            }
            test_Label.Text = crit;
            booksSource.SelectCommand = crit;


            GridView1.DataBind();
        }


        protected void choose_book(object sender, EventArgs e)
        {
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

        protected void DropDownYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            test_Label.Text = this.DropDownYear.SelectedValue;
            Session["IDY"] = this.DropDownYear.SelectedValue.ToString();

        }

        //protected void DropDownGenres_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["ID_genre"] = this.DropDownGenres.SelectedValue;
        //}


    }
}