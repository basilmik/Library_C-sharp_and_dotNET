<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="account.aspx.cs" Inherits="WebApplication1.account" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="account_inform" runat="server" Text=""></asp:Label>
    <div id="account_detail_div" visible="true" runat="server" style="width: 30%; padding: 1%; margin: 1%;">

        <asp:Label ID="name_label" runat="server" Text="Имя:"> </asp:Label>
        <br>
        <asp:TextBox ID="name_textbox" runat="server" Text='' Style="width: 100%; height: 20px;" />
        <br>

        <asp:Label ID="surname_label" runat="server" Text="Фамилия:"> </asp:Label>
        <br>
        <asp:TextBox ID="surname_textbox" runat="server" Text='' Style="width: 100%; height: 20px;" />
        <br>

        <asp:Label ID="login_label" runat="server" Text="логин:"> </asp:Label>
        <br>
        <asp:TextBox ID="login_textbox" runat="server" Text='' Style="width: 100%; height: 20px;" />
        <br>

        <asp:Label ID="pass_label" runat="server" Text="пароль:"> </asp:Label>
        <br>
        <asp:TextBox ID="pass_textbox" runat="server" Text='' Style="width: 100%; height: 20px;" />
        <br>

        <asp:Label ID="email_label" runat="server" Text="почта:"> </asp:Label>
        <br>
        <asp:TextBox ID="email_textbox" runat="server" Text='' Style="width: 100%; height: 20px;" />
        <br>

        <asp:Label ID="codeword_login" runat="server" Text="Кодовое слово:"> </asp:Label>
        <br>
        <asp:TextBox ID="codeword_textbox" runat="server" Text='' Style="width: 100%; height: 20px;" />
        <br>
        <br>
        <asp:Button ID="show_data_btn" runat="server"
        Text="Увидеть данные"
        OnClick="show_dataClick"  Visible="true"
        BorderStyle="None" class="btn4" />

        <asp:Button ID="save_changes_btn" runat="server"
            Text="Сохранить изменения"
            OnClick="save_changesClick" Visible="false"
            BorderStyle="None" class="btn4" />

            <asp:Button ID="cancel_changes_btn" runat="server"
        Text="Отменить изменения"
        OnClick="cancel_changesClick"  Visible="false"
        BorderStyle="None" class="btn4" />



        <asp:Button ID="close_data" runat="server"
        Text="Закрыть данные"
        OnClick="close_dataClick"  Visible="false"
        BorderStyle="None" class="btn4" />


    </div>

    <%--        <asp:SqlDataSource ID="SqlDataSource_main_user" runat="server"
        ConnectionString="<%$ ConnectionStrings:libraryConnectionString %>"
        SelectCommand="SELECT * FROM Users WHERE UserID=@userID">
            <SelectParameters>
             <asp:SessionParameter DbType="String" Name="userID" SessionField="userID" />
         </SelectParameters>
    </asp:SqlDataSource>--%>
</asp:Content>


