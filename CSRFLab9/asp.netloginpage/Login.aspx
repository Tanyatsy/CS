<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="asp.netloginpage.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body{
            background-color: #ffffff;
        }

        .table-wrapper {
            margin: auto;
            text-align:center;
            background-color: #fff8f0;
             border: 1px solid;
            border-radius: 5px;
            max-width: 400px;
           margin-top: 10%;
            font-size: 25px;
            padding: 62px;
        }

        .column {
            display: flex;
            flex-direction: column
        }

        .row span {
            font-size: 13px;
            font-weight: bold;
            text-align: left;

        }

        .row input {
            height: 43px;
            border-radius: 5px;
            border: 1px solid;

        }

        .row {
                margin-bottom: 20px;
                flex-direction: column;
                display: flex;
        }

        input[type=submit] {
               margin-bottom: 20px;
            border-radius: 5px;
            border: 1px solid #261818;
            padding: 12px 26px;
            width: 48%;
            background-color: #f9f9f9;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="table-wrapper">
        <p>LOGIN</p>
        <div class="column">
              <div class="row">
                    <asp:Label ID="Label1" runat="server" Text="Enter the username:"></asp:Label>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                     <asp:Label ID="Label2" runat="server" Text="Enter the password:"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </div>
        </div>
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />

        <asp:Label ID="lblErrorMessage" runat="server" Text="Incorrect User Credentials" ForeColor="Red"></asp:Label>
    </table>
    </div>
    </form>
</body>
</html>