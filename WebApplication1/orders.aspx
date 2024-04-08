<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="orders.aspx.cs" Inherits="WebApplication1.orders" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
        <Columns>
<%--            <asp:BoundField DataField="OrderID" HeaderText="OrderID" SortExpression="OrderID" />
            <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
            <asp:BoundField DataField="OrderStateID" HeaderText="OrderStateID" SortExpression="OrderStateID" />
            
            <asp:BoundField DataField="BookID" HeaderText="BookID" SortExpression="BookID" />--%>
            <asp:BoundField DataField="BName" HeaderText="BName" SortExpression="BName" />
            <asp:BoundField DataField="OrderState" HeaderText="OrderState" SortExpression="OrderState" />
             <asp:TemplateField HeaderText="Узнать больше" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Button ID="choose_bookbtn" runat="server" Width="100%" Height="100%" BorderStyle="None" 
                         Text="?" OnClick="choose_book" CommandArgument='<%# Eval("BookID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            <asp:TemplateField HeaderText="Отменить" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Button ID="cancel_btn" runat="server" Width="100%" Height="100%" BorderStyle="None" 
                         Text="-" OnClick="cancel_orderClick" CommandArgument='<%# Eval("BookID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>

    </asp:GridView>



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>" 
        SelectCommand="SELECT * FROM [order_copy_book_view] WHERE UserID=@userID">
                 <SelectParameters>
             <asp:SessionParameter DbType="String" Name="userID" SessionField="userID" />
         </SelectParameters>


    </asp:SqlDataSource>



</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
   <div id="detail_info" Visible="false" runat="server">
    <asp:Label ID="book_name_info" runat="server" Text=''></asp:Label><br>
    <br>
    <asp:Label ID="book_year_info" runat="server" Text=''></asp:Label><br>
    <br>
    <asp:Label ID="genres_label_info" runat="server" Text=''></asp:Label><br>
    <br>
    <asp:Label ID="authors_label_info" runat="server" Text=''></asp:Label><br>
    <br>
    <asp:Label ID="book_desc_info" runat="server" Text=''></asp:Label>
       <br>
<asp:Button ID="close_details_btn" runat="server" Width="100%" Height="100%" BorderStyle="None" 
                         Text="Закрыть" OnClick="close_details"  />
   </div>
</asp:Content>
