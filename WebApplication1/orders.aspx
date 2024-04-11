<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="orders.aspx.cs" Inherits="WebApplication1.orders" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div  style=" margin: auto;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="OrderID" Visible="false" HeaderText="OrderID" SortExpression="OrderID" />
            <asp:BoundField DataField="UserID" Visible="false" HeaderText="UserID" SortExpression="UserID" />
            <asp:BoundField DataField="OrderStateID" Visible="false" HeaderText="OrderStateID" SortExpression="OrderStateID" />
            <asp:BoundField DataField="BookID" Visible="false" HeaderText="BookID" SortExpression="BookID" />


            <asp:BoundField DataField="BName" HeaderText="Название" SortExpression="BName"   HeaderStyle-Width="40%"/>
            <asp:BoundField DataField="OrderState" HeaderText="Состояние" SortExpression="OrderState"   HeaderStyle-Width="40%"/>
             <asp:TemplateField HeaderText="Узнать больше" HeaderStyle-Width="40%">
                    <ItemTemplate>
                        <asp:Button ID="choose_bookbtn" runat="server" Width="100%" Height="100%" BorderStyle="None" 
                         Text="?" OnClick="choose_book" CommandArgument='<%# Eval("BookID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            <asp:TemplateField HeaderText="Отменить" HeaderStyle-Width="40%">
                    <ItemTemplate>
                        <asp:Button ID="cancel_btn" runat="server" Width="100%" Height="100%" BorderStyle="None" 
                         Text="Отмена" OnClick="cancel_orderClick" CommandArgument='<%# Eval("OrderID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>

    </asp:GridView>
        </div>


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
                         Text="Закрыть" OnClick="close_details" class="btn2" />
   </div>
</asp:Content>
