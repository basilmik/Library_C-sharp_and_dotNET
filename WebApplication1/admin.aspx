<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="WebApplication1.admin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:Button ID="add_btn" runat="server" BorderStyle="None" Style="width: 100%; margin: 0px;"
        Text="add" OnClick="add_book" />

    <br />
    <br />
    <asp:GridView ID="GridView1"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="BookID"
        DataSourceID="booksSource"
        Style="width: 100%;">
        <Columns>
            <asp:BoundField DataField="name" HeaderText="Название" SortExpression="name" HeaderStyle-Width="250px" />
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


            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" BorderStyle="None" Style="width: 100%; margin: 0px;"
                        Text="edit" OnClick="edit_book" CommandArgument='<%# Eval("BookID") %>' />

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del">
                <ItemTemplate>
                    <asp:Button ID="del_btn" runat="server" BorderStyle="None" Style="width: 100%; margin: 0px;"
                        Text="del" OnClick="del_book" CommandArgument='<%# Eval("BookID") %>'/>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
    </asp:GridView>



    <asp:SqlDataSource ID="SqlDataSource_authorslist" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT [AuthorNameSurname], [BookID], [AuthorID] FROM [books_authors_view]" />

    <asp:SqlDataSource ID="SqlDataSource_genreslist" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT GenreID, Name FROM [Genres]" />

    <asp:SqlDataSource ID="yearsSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT DISTINCT YEAR(YearPublished) as year FROM Books ORDER BY YEAR(YearPublished) " />


    <asp:SqlDataSource ID="booksSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT [BookID], [Name] as name, YEAR([YearPublished]) as year
         FROM [Books] " />

    <asp:Label ID="test_label" runat="server" Text="Label"></asp:Label>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <asp:PlaceHolder ID="OneBookPlaceHolder" runat="server" Visible="false">
        <asp:Label ID="book_in_form_ID" runat="server" Text=""> book_in_form_ID </asp:Label>

        <table style="align-content: start; width: 100%;">
            <tr>
                <td class="border" style="align-content: start;" colspan="2">
                    <asp:Label ID="bookName_labelID" runat="server" Text="Название:">

                        <asp:TextBox ID="book_name_edit" runat="server" Text='' Style="width: 100%; height: 30px;" />
                    </asp:Label>

                </td>
            </tr>

            <tr>
                <td class="border" colspan="2">
                    <asp:Label ID="bookDesc_labelID" runat="server" Text="Описание:">
                        <asp:TextBox ID="desc_edit" runat="server" Text='' Style="width: 100%; height: 30px;" />
                    </asp:Label>

                </td>
            </tr>

            <tr>
                <td class="border" colspan="2">
                    <asp:Label ID="bookYear_labelID" runat="server" Text="Год публикации:">
                        <asp:TextBox ID="year_edit" runat="server" Text=''
                            Style="width: 100%; height: 30px;" />

                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="border" colspan="1" style="width: 50%; height: 30px;">
                    <asp:Label ID="bookAuthors_labelID" runat="server" Text="Авторы:">
                        <asp:CheckBoxList runat="server"
                            ID="author_edit" DataSourceID="SqlDataSource_authorslist2"
                            DataTextField="AuthorNameSurname" DataValueField="AuthorID" />

                    </asp:Label>
                </td>

                <td class="border" colspan="1">
                    <asp:Label ID="bookGenres_labelID" runat="server" Text="Жанры:">
                        <asp:CheckBoxList runat="server"
                            ID="genres_edit" DataSourceID="SqlDataSource_genreslist2"
                            DataTextField="name" DataValueField="GenreID" />
                    </asp:Label>
                </td>
            </tr>

        </table>

        <asp:Button ID="save_btn" runat="server" OnClick="save_OneBookPlaceHolder" Text="save" Style="width: 200px; height: 30px; font-size: 14pt;" />
        <br>
        <br>
        <asp:Button ID="cancel_btn" OnClick="close_OneBookPlaceHolder" runat="server" Text="cancel" Style="width: 200px; height: 30px; font-size: 14pt;" />


    </asp:PlaceHolder>


    <asp:SqlDataSource ID="SqlDataSource_authorslist2" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT DISTINCT [AuthorID], concat(Name, ' ', Surname) as [AuthorNameSurname] FROM [Authors]" />
    <asp:SqlDataSource ID="SqlDataSource_genreslist2" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT DISTINCT GenreID, Name as name FROM [Genres]" />



</asp:Content>
