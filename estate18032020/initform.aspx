<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Initform.aspx.cs" Inherits="estate18032020.Initform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        YouTube URL:
                    </td>
                    <td>
                        <asp:TextBox ID="txtYouTubeURL" runat="server" Text="" Width="450" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="BtnProcess" Text="Process" runat="server" OnClick="BtnProcess_Click" Width="100" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlVideoFormats" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="BtnDownload" Text="Download" runat="server" OnClick="BtnDownload_Click" Width="100" Visible="false" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblMessage" Text="" runat="server" />
        </div>
    </form>
</body>
</html>
