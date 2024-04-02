<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="WebApplication1.admin" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:GridView ID="GridView1"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="BookID"
        DataSourceID="booksSource"
        Style="align-content: start;">
        <columns>
            <%--                
            <asp:BoundField DataField="BookID" HeaderText="BookID" InsertVisible="False" ReadOnly="True" SortExpression="BookID" />--%>
            <asp:BoundField DataField="name" HeaderText="Название" SortExpression="name" HeaderStyle-Width="250px" />
            <asp:BoundField DataField="year" HeaderText="Год" SortExpression="year" HeaderStyle-Width="50px" />


            <asp:TemplateField HeaderText="Авторы" HeaderStyle-Width="250px">
                <itemtemplate>
                    <asp:Label ID="authors_label" runat="server" Text='<%# get_authors(Eval("BookID")) %>'></asp:Label>
                </itemtemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Жанры" HeaderStyle-Width="250px">
                <itemtemplate>
                    <asp:Label ID="genres_label" runat="server" Text='<%# get_genres(Eval("BookID")) %>'></asp:Label>
                </itemtemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Edit">
                <itemtemplate>
                    <asp:Button ID="edit_btn" runat="server" Width="100%" Height="100%" BorderStyle="None"
                        Text="edit" OnClick="edit_book" CommandArgument='<%# Eval("BookID") %>' />
                </itemtemplate>
            </asp:TemplateField>

        </columns>
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


    <asp:SqlDataSource ID="booksSource2" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT [BookID], [Name] as name, YEAR([YearPublished]) as year
         FROM [Books] " />


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:TextBox ID="book_name_edit" runat="server" Text='' />
    <br>
<%--    <asp:CheckBoxList runat="server"
        DataSourceID="SqlDataSource_authorslist" DataTextField="AuthorNameSurname" DataValueField="AuthorNameSurname">
    </asp:CheckBoxList>--%>
</asp:Content>

