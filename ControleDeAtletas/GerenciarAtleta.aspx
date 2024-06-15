<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GerenciarAtleta.aspx.cs" Inherits="ControleDeAtletas.WebUI.GerenciarAtleta" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gerenciar Atleta</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Gerenciar Atleta</h2>
            <asp:Label ID="LabelNomeCompleto" runat="server" Text="Nome Completo:" />
            <asp:TextBox ID="TextBoxNomeCompleto" runat="server" />
            <br />
            <asp:Label ID="LabelApelido" runat="server" Text="Apelido:" />
            <asp:TextBox ID="TextBoxApelido" runat="server" />
            <br />
            <asp:Label ID="LabelDataNascimento" runat="server" Text="Data de Nascimento:" />
            <asp:TextBox ID="TextBoxDataNascimento" runat="server" />
            <br />
            <asp:Label ID="LabelAltura" runat="server" Text="Altura (em metros):" />
            <asp:TextBox ID="TextBoxAltura" runat="server" />
            <br />
            <asp:Label ID="LabelPeso" runat="server" Text="Peso (em kg):" />
            <asp:TextBox ID="TextBoxPeso" runat="server" />
            <br />
            <asp:Label ID="LabelPosicao" runat="server" Text="Posição:" />
            <asp:TextBox ID="TextBoxPosicao" runat="server" />
            <br />
            <asp:Label ID="LabelNumeroCamisa" runat="server" Text="Número da Camisa:" />
            <asp:TextBox ID="TextBoxNumeroCamisa" runat="server" />
            <br />
            <asp:Button ID="ButtonSalvar" runat="server" Text="Salvar" OnClick="ButtonSalvar_Click" />
            <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" OnClick="ButtonCancelar_Click" />
        </div>
    </form>
</body>
</html>
