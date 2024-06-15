<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAtleta.aspx.cs" Inherits="ControleDeAtletas.CreateAtleta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastrar Novo Atleta</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Cadastrar Novo Atleta</h2>
            <hr />
            <div>
                <label>Número da Camisa:</label>
                <asp:TextBox ID="TextBoxNumeroCamisa" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>Nome Completo:</label>
                <asp:TextBox ID="TextBoxNomeCompleto" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>Apelido:</label>
                <asp:TextBox ID="TextBoxApelido" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>Posição:</label>
                <asp:TextBox ID="TextBoxPosicao" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>Altura (em metros):</label>
                <asp:TextBox ID="TextBoxAltura" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>Peso (em kg):</label>
                <asp:TextBox ID="TextBoxPeso" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>Data de Nascimento:</label><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:TextBox ID="TextBoxDataNascimento" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxDataNascimento" Format="dd/MM/yyyy" />
            </div>
            <div>
                <asp:Button ID="ButtonSalvar" runat="server" Text="Salvar" OnClick="ButtonSalvar_Click" />
                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" OnClick="ButtonCancelar_Click" />
            </div>
        </div>
    </form>
</body>
</html>
