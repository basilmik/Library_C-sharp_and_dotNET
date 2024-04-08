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
            if (connect.State != ConnectionState.Open)
            {
                connect.Open();
            }
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

        protected void data_bind_authors()
        {
            if (author_edit.Items.Count == 0)
            {
                SqlDataSource_authorslist2.SelectCommand = "SELECT DISTINCT [AuthorID], concat(Name, ' ', Surname) as [AuthorNameSurname] FROM [Authors]";
                author_edit.DataBind();
            }
        }

        protected void data_bind_genres()
        {
            if (genres_edit.Items.Count == 0)
            {
                SqlDataSource_genreslist2.SelectCommand = "SELECT DISTINCT GenreID, Name as name FROM [Genres]";
                genres_edit.DataBind();
            }
        }

        protected void edit_book(object sender, EventArgs e)
        {
            if (OneBookPlaceHolder.Visible == false)
                OneBookPlaceHolder.Visible = true;
            if (OneBookPlaceHolder.Visible == true)
            {
                string book_id = ((Button)sender).CommandArgument.ToString();
                List<String> checked_authors = get_checked_authors_list(book_id);
                List<String> checked_genres = get_checked_genres_list(book_id);

                book_in_form_ID.Text = book_id;
                book_name_edit.Text = get_book_name_for_edit(book_id);
                year_edit.Text = get_book_year_for_edit(book_id);
                desc_edit.Text = get_book_desc_for_edit(book_id);

                data_bind_authors();
                data_bind_genres();

                ListItemCollection all_list = author_edit.Items;

                for (int count = 0; count < all_list.Count; count++)
                {
                    bool does_contain = checked_authors.Contains(all_list[count].ToString());
                    all_list[count].Selected = does_contain;
                }


                ListItemCollection all_genres_list = genres_edit.Items;
                for (int count = 0; count < all_genres_list.Count; count++)
                {
                    bool does_contain = checked_genres.Contains(all_genres_list[count].ToString());
                    all_genres_list[count].Selected = does_contain;
                }

            }
        }

        protected void set_author_edit_items_empty()
        {
            ListItemCollection all_authors_list = author_edit.Items;
            for (int count = 0; count < all_authors_list.Count; count++)
            {
                all_authors_list[count].Selected = false;
            }
        }

        protected void set_genres_edit_items_empty()
        {
            ListItemCollection all_genres_list = genres_edit.Items;
            for (int count = 0; count < all_genres_list.Count; count++)
            {
                all_genres_list[count].Selected = false;
            }
        }

        protected void set_OneBookPlaceHolder_empty()
        {
            book_in_form_ID.Text = "";
            book_name_edit.Text = "";
            year_edit.Text = "";
            desc_edit.Text = "";

            set_author_edit_items_empty();
            set_genres_edit_items_empty();
        }

        protected void add_book(object sender, EventArgs e)
        {
            if (OneBookPlaceHolder.Visible == false)
                OneBookPlaceHolder.Visible = true;

            set_OneBookPlaceHolder_empty();
        }

        protected void close_OneBookPlaceHolder(object sender, EventArgs e)
        {
            set_OneBookPlaceHolder_empty();
            OneBookPlaceHolder.Visible = false;
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
        protected string get_last_addedBookID()
        {
            if (connect.State != ConnectionState.Open)
                connect.Open();

            string res = "";
            string sqlQuery =
            "SELECT MAX(BookID) FROM Books";

            var mycom = new SqlCommand();
            mycom.CommandText = sqlQuery;
            mycom.Connection = connect;
            SqlDataReader reader = mycom.ExecuteReader();

            if (reader.HasRows) // если есть данные
                while (reader.Read()) // построчно считываем данные
                {
                    res = reader.GetValue(0).ToString();
                }

            reader.Close();
            connect.Close();
            return res;
        }

        protected void clear_author_genre_book(string book_id)
        {
            if (book_id.Any())
            {
                // test_label.Text = "YES";
                string delete_sql = "DELETE FROM Book_Author WHERE BookID = '" + book_id + "'";
                try
                {
                    run_query(delete_sql);
                }
                catch
                {
                    test_label.Text = "Ошибка " + book_id + " " + delete_sql;
                    return;
                }

                delete_sql = "DELETE FROM Book_Genre WHERE BookID = '" + book_id + "'";
                try
                {
                    run_query(delete_sql);
                }
                catch
                {
                    test_label.Text = "Ошибка " + book_id + " " + delete_sql;
                    return;
                }
            }



        }
        protected void save_OneBookPlaceHolder(object sender, EventArgs e)
        {
            string book_id = book_in_form_ID.Text;
            string name = book_name_edit.Text;
            string year = year_edit.Text + "-01-01";
            string desc = desc_edit.Text;

            string sql_query = "";
            //test_label.Text = book_in_form_ID.Text;
            if (book_id.Any())
            {
               
                // edit
                string update_sql = "UPDATE Books SET ";
                sql_query = update_sql
                    + "Name = '" + name + "', "
                    + "YearPublished = '" + year + "', "
                    + "Description = '" + desc + "'";
                sql_query = sql_query + " WHERE BookID = '" + book_id + "'";
                
            }
            else
            {
                // add
                string insert_sql = "INSERT INTO Books ( Name, YearPublished, Description) VALUES (";
                sql_query = insert_sql
                    + "'" + name + "', "
                    + "'" + year + "', "
                    + "'" + desc + "')";
            }

            test_label.Text = sql_query;
            // ADD OR EDIT
            try
            {
                run_query(sql_query);
            }
            catch
            {
                test_label.Text = "Ошибка " + sql_query;
                return;
            }

            // DELETE ALL BOOK-AUTHOR and BOOK-GENRE CONNECTION
            clear_author_genre_book(book_id);
            if (!book_id.Any())
            {
                book_id = get_last_addedBookID();
            }

            test_label.Text = "";
            // update genres
            ListItemCollection all_genres_list = genres_edit.Items;
            for (int count = 0; count < all_genres_list.Count; count++)
            {
                if (all_genres_list[count].Selected == true)
                {
                    string add_book_genre_sql = "INSERT INTO Book_Genre (BookID, GenreID) VALUES ('"
                        + book_id + "', '" + all_genres_list[count].Value + "')";
                    test_label.Text = test_label.Text + " " + add_book_genre_sql;
                    try
                    {
                        run_query(add_book_genre_sql);
                        
                    }
                    catch
                    {
                        test_label.Text = "Ошибка " + add_book_genre_sql;
                        return;
                    }
                }
            }

            // update authors
            ListItemCollection all_authors_list = author_edit.Items;
            for (int count = 0; count < all_authors_list.Count; count++)
            {
                if (all_authors_list[count].Selected == true)
                {
                    string add_book_author_sql = "INSERT INTO Book_Author (BookID, AuthorID) VALUES ('"
                        + book_id + "', '" + all_authors_list[count].Value + "')";
                    test_label.Text = test_label.Text + " " + add_book_author_sql;
                    try
                    {
                        run_query(add_book_author_sql);
                    }
                    catch
                    {
                        test_label.Text = "Ошибка " + add_book_author_sql;
                        return;
                    }
                }
            }


            Response.Redirect("admin.aspx");
        }


        protected void del_book(object sender, EventArgs e)
        {
            string book_id = ((Button)sender).CommandArgument.ToString();

            clear_author_genre_book(book_id);

            string delete_sql = "DELETE FROM Books WHERE BookID = '"+ book_id + "'";
            try
            {
                run_query(delete_sql);
            }
            catch
            {
                test_label.Text = "Ошибка " + book_id+" "+ delete_sql;
                return;
            }
           Response.Redirect("admin.aspx");
        }

    }
}