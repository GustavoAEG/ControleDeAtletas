<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAtleta.aspx.cs" Inherits="ControleDeAtletas.CreateAtleta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastrar Novo Atleta</title>
    <style>
        .error-message {
            color: red;
            margin-top: 10px;
        }
        .form-field {
            margin-bottom: 10px;
        }
        .form-field label {
            display: block;
            margin-bottom: 5px;
        }
        .form-field input[type=text], 
        .form-field input[type=date] {
            width: 100%;
            box-sizing: border-box;
            padding: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> <!-- Adicionando ScriptManager aqui -->

        <div>
            <h2>Cadastrar Novo Atleta</h2>
            <hr />
            <div class="form-field">
                <label>Número da Camisa:</label>
                <asp:TextBox ID="TextBoxNumeroCamisa" runat="server"></asp:TextBox>
            </div>
            <div class="form-field">
                <label>Nome Completo:</label>
                <asp:TextBox ID="TextBoxNomeCompleto" runat="server"></asp:TextBox>
            </div>
            <div class="form-field">
                <label>Apelido:</label>
                <asp:TextBox ID="TextBoxApelido" runat="server"></asp:TextBox>
            </div>
            <div class="form-field">
                <label>Posição:</label>
                <asp:TextBox ID="TextBoxPosicao" runat="server"></asp:TextBox>
            </div>
            <div class="form-field">
                <label>Altura (em metros):</label>
                <asp:TextBox ID="TextBoxAltura" runat="server"></asp:TextBox>
            </div>
            <div class="form-field">
                <label>Peso (em kg):</label>
                <asp:TextBox ID="TextBoxPeso" runat="server"></asp:TextBox>
            </div>
            <div class="form-field">
                <label>Data de Nascimento:</label>
                <asp:TextBox ID="TextBoxDataNascimento" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxDataNascimento" Format="dd/MM/yyyy" />
            </div>
            <div class="form-field">
                <asp:Button ID="ButtonSalvar" runat="server" Text="Salvar" OnClick="ButtonSalvar_Click" />
                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" OnClick="ButtonCancelar_Click" />
            </div>
            <div class="form-field">
                <asp:Literal ID="LiteralErrorMessage" runat="server"></asp:Literal>
            </div>
        </div>
    </form>
</body>
</html>
