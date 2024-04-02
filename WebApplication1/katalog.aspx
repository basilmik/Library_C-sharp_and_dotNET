<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="katalog.aspx.cs" Inherits="WebApplication1.katalog1" %>
   

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <table style="background-color: White; width: 100%">

        <tr>
            <td class="border" style="align-content: start;">

                <asp:Label ID="SearchNameLabel" runat="server" Text="Название:"></asp:Label>
                <br>
                <asp:TextBox ID="TextBoxName" Text="" runat="server" Style="width: 173px; height: 27px;"></asp:TextBox>
                <br>
                <asp:Label ID="AuthorSearchLabel" runat="server" Text="Авторы:"></asp:Label>
                <br>
                <asp:DropDownList ID="DropDownAuthors" runat="server"
                    DataSourceID="SqlDataSource_authorslist" DataTextField="name" DataValueField="AuthorID" Style="width: 180px; height: 30px;">
                </asp:DropDownList>

                <br>
                <asp:Label ID="GenreSearchLabel" runat="server" Text="Жанры:"></asp:Label>
                <br>
                <asp:DropDownList ID="DropDownGenres" runat="server"
                    DataSourceID="SqlDataSource_genreslist" DataTextField="Name" DataValueField="GenreID" Style="width: 180px; height: 30px;">
                </asp:DropDownList>
                <br>
                <asp:Label ID="YearSearchLabel" runat="server" Text="Год:"></asp:Label>
                <br>
                <asp:DropDownList ID="DropDownYear" runat="server"
                    DataSourceID="yearsSource" DataTextField="year" DataValueField="year" Style="width: 180px; height: 30px;" 
                    OnSelectedIndexChanged="DropDownYear_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>

                <br>
                <br>
                <asp:Button runat="server" Text="search" OnClick="search_Click" Style="width: 180px; height: 30px; font-size: 14pt;" />
                <br>
                <asp:Button runat="server" Text="clear search" OnClick="search_clear_Click" Style="width: 180px; height: 30px; font-size: 14pt;" />
                <br>
                <br>
                <br>
                <asp:Label ID="test_Label" runat="server" Text="Label"></asp:Label>
                <br>
                <br>
            </td>

     <asp:SqlDataSource ID="booksSource" runat="server"
                ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
                SelectCommand="SELECT [BookID], [Name] as name, YEAR([YearPublished]) as year FROM [Books]
         WHERE (@YearPublished = '-1' OR YEAR([YearPublished]) = @YearPublished)
         ">
         <SelectParameters>
             <asp:SessionParameter DbType="String" Name="YearPublished" SessionField="IDY" />
         </SelectParameters>
    </asp:SqlDataSource>
           

            <td class="border" style="align-content: start; width: 100%; height: 100%;">
                <asp:GridView ID="GridView1"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="BookID"
                    DataSourceID="booksSource"
                    Style="align-content: start;">
                    <Columns>
                                   
            <%--   <asp:BoundField DataField="BookID" HeaderText="BookID" InsertVisible="False" ReadOnly="True" SortExpression="BookID" />--%>
                <asp:BoundField DataField="name" HeaderText="Название" SortExpression="name" HeaderStyle-Width="250px"/>
                <asp:BoundField DataField="year" HeaderText="Год" SortExpression="year" HeaderStyle-Width="50px"/>


                <asp:TemplateField HeaderText="Авторы" HeaderStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="authors_label" runat="server" Text='<%# get_authors(Eval("BookID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Жанры" HeaderStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="genres_label" runat="server" Text='<%# get_genres(Eval("BookID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Узнать больше" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Button ID="choose_bookbtn" runat="server" Width="100%" Height="100%" BorderStyle="None" 
                         Text="?" OnClick="choose_book" CommandArgument='<%# Eval("BookID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                </asp:GridView>

            </td>
        </tr>
    </table>

         <asp:SqlDataSource ID="SqlDataSource_authorslist" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT AuthorID, books_authors_view.AuthorNameSurname AS name FROM [books_authors_view]" />

    <asp:SqlDataSource ID="SqlDataSource_genreslist" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT GenreID, Name FROM [Genres]" />

    <asp:SqlDataSource ID="yearsSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT DISTINCT YEAR(YearPublished) as year FROM Books ORDER BY YEAR(YearPublished) " />




</asp:Content>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            width: 652px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Label ID="book_name_info" runat="server" Text=''></asp:Label><br>
    <br>
    <asp:Label ID="book_year_info" runat="server" Text=''></asp:Label><br>
    <br>
    <asp:Label ID="genres_label_info" runat="server" Text=''></asp:Label><br>
    <br>
    <asp:Label ID="authors_label_info" runat="server" Text=''></asp:Label><br>
    <br>

    <asp:Label ID="book_desc_info" runat="server" Text=''></asp:Label>


</asp:Content>

