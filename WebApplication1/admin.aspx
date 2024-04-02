<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="WebApplication1.admin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:GridView ID="GridView1"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="BookID"
        DataSourceID="booksSource"
        Style="align-content: start;">
        <Columns>
            <%--                
            <asp:BoundField DataField="BookID" HeaderText="BookID" InsertVisible="False" ReadOnly="True" SortExpression="BookID" />--%>
            <asp:BoundField DataField="name" HeaderText="Название" SortExpression="name" HeaderStyle-Width="250px" />
            <asp:BoundField DataField="year" HeaderText="Год" SortExpression="year" HeaderStyle-Width="50px" />


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
                    <asp:Button ID="edit_btn" runat="server" Width="100%" Height="100%" BorderStyle="None"
                        Text="edit" OnClick="edit_book" CommandArgument='<%# Eval("BookID") %>' />
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

    <asp:PlaceHolder ID="EditPlaceHolder" runat="server" Visible="false" >

        <asp:Table runat="server" Style="align-content: start;">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:TextBox ID="book_name_edit" runat="server" Text='' Style="width: 400px; height: 30px;" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:TextBox ID="desc_edit" runat="server" Text='' Style="width: 400px; height: 30px;" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:TextBox ID="year_edit" runat="server" Text='' Style="width: 400px; height: 30px;" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="1">
                    <asp:CheckBoxList runat="server"
                ID="author_edit" DataSourceID="SqlDataSource_authorslist2" 
                        DataTextField="AuthorNameSurname" DataValueField="AuthorID" />

                    </asp:TableCell>

                <asp:TableCell ColumnSpan="1">
                <asp:CheckBoxList runat="server"
                ID="genres_edit" DataSourceID="SqlDataSource_genreslist2" DataTextField="name" DataValueField="GenreID" />

                

                    </asp:TableCell>

            </asp:TableRow>


        </asp:Table>

            <asp:Button runat="server" Text="save" Style="width: 200px; height: 30px; font-size: 14pt;" />
            <br>
            <br>
            <asp:Button runat="server" Text="cancel" Style="width: 200px; height: 30px; font-size: 14pt;" />


    </asp:PlaceHolder>
    
    
    <asp:SqlDataSource ID="SqlDataSource_authorslist2" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT DISTINCT [AuthorID], concat(Name, ' ', Surname) as [AuthorNameSurname] FROM [Authors]" />
    <asp:SqlDataSource ID="SqlDataSource_genreslist2" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT DISTINCT GenreID, Name as name FROM [Genres]" />



</asp:Content>
