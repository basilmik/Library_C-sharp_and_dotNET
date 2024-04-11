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
            if (Convert.ToString(Session["is_in"]) == "yes")
            {
                order_book_btn.Visible = true;

            }
            else
                order_book_btn.Visible = false;
            if (string.IsNullOrEmpty(Session["userID"] as string))
            {
                Session["userID"] = "-1";
            }
            
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
            DropDownYear.SelectedIndex = 0;
            TextBoxName.Text = "";

            Response.Redirect("katalog.aspx");
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
            //test_Label.Text = crit;
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
                    
                    bookID_info.Visible = true;
                    bookID_info.Text =  reader.GetValue(0).ToString();
                    book_name_info.Text = reader.GetValue(1).ToString();
                    book_year_info.Text = reader.GetValue(2).ToString();
                    book_desc_info.Text = reader.GetValue(3).ToString();
                }
            }

            reader.Close();

            connect.Close();

            genres_label_info.Text = get_genres(((Button)sender).CommandArgument.ToString());
            authors_label_info.Text = get_authors(((Button)sender).CommandArgument.ToString());
            book_info_div.Visible = true;
            
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
            //test_Label.Text = this.DropDownYear.SelectedValue;
            Session["IDY"] = this.DropDownYear.SelectedValue.ToString();

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
        protected void order_book(string userID, string copyID)
        {
            // update copy state
            string sql = "UPDATE Copies SET CopyStateID='2' WHERE CopyID='" + copyID+"'";
            run_query(sql);
            // create new order with state забронировано
            sql = "INSERT INTO [Order] (UserID, CopyID, OrderStateID) VALUES ('" + userID + "', '" + copyID + "','2')";
            run_query(sql);
        }

        protected void order_bookClick(object sender, EventArgs e)
        {
            //info_label.Text = Convert.ToString(Session["userID"]);
            if (Convert.ToString(Session["userID"]) == "-1")
            {
                info_label.Text = "Необходимо авторизироваться для бронирования :)";
            }
            else
            {

                string bookID = bookID_info.Text;
                string sql = "SELECT min(CopyID) FROM [Copies] WHERE BookID = '" + bookID + "' AND CopyStateID ='1'";
                if (connect.State != ConnectionState.Open)
                    connect.Open();

                var mycom = new SqlCommand();
                mycom.CommandText = sql;
                mycom.Connection = connect;

                SqlDataReader reader = mycom.ExecuteReader();
                string res = "";
                if (reader.HasRows) // если есть данные
                {

                    reader.Read();
                    res = reader.GetValue(0).ToString();
                }

                reader.Close();
                connect.Close();
               
                if (res != "")
                {
                    test_Label.Text = "Забронировано";
                    order_book(Convert.ToString(Session["userID"]), res);

                }
                else
                {
                    test_Label.Text = "Нет доступных копий";
                }

            }
        }     
        
        protected void close_info_bookClick(object sender, EventArgs e)
        {
            book_info_div.Visible = false;

        }

    }
}