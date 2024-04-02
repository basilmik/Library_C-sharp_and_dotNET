<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication1.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
              <%--SELECT Books.*,  
                concat(Authors.Name, ' ', Authors.Surname) AS authorname,  
                Genres.Name AS genrename 
                FROM [Books] 
                JOIN Book_Author on Books.BookID = Book_Author.BookID
                JOIN Authors on Book_Author.AuthorID = Authors.AuthorID
                JOIN Book_Genre on Books.BookID = Book_Genre.BookID
                JOIN Genres on Book_Genre.GenreID = Genres.GenreID
                
                SELECT c.name AS category_name,
       string_agg(p.name, ', ' ORDER BY p.name) AS products
  FROM category c,
       product p
 WHERE p.category_id = c.category_id
 GROUP BY c.category_id,
          c.name


                    SELECT b.BookID, b.Name AS bname,
string_agg(ba.AuthorNameSurname, ', ') AS banames
FROM Books b,  books_authors_view ba
 WHERE b.BookID = ba.BookID
 GROUP BY ba.BookID, b.Name, b.BookID

                    SELECT b.BookID, b.Name AS bname,
string_agg(ba.AuthorNameSurname, ', ') AS banames,
string_agg(bgv.GenreName, ', ') AS gnames
FROM Books b,  
books_authors_view ba, 
books_genres_view bgv
WHERE b.BookID = ba.BookID
AND bgv.BookID = b.BookID
GROUP BY ba.BookID, b.Name, b.BookID



SELECT Books.BookID, Books.Name AS bname,
string_agg(BAV.AuthorNameSurname, ', ') AS banames,
string_agg(books_genres_view.GenreName, ', ') AS gnames
FROM Books
JOIN (SELECT DISTINCT * FROM books_authors_view) AS BAV on BAV.BookID =  Books.BookID
JOIN (SELECT DISTINCT * FROM books_genres_view)  on  books_genres_view.BookID = Books.BookID

GROUP BY BAV.BookID, Books.Name, Books.BookID
                    --%>
</div>

</asp:Content>