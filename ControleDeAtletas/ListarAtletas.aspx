<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarAtletas.aspx.cs" Inherits="ControleDeAtletas.WebUI.ListarAtletas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .readonly-textbox {
            background-color: #f8d7da;
            color: #721c24; 
            cursor: not-allowed;
            padding: 5px 10px; 
            font-weight: bold;
        }

        .imc-alto {
            background-color: #ffc107; 
            color: #212529;
            font-weight: bold; 
            padding: 3px 5px;
            border-radius: 4px;

            .radioListHorizontal

        {
            display: flex;
            list-style: none; 
            padding: 0;
        }

        .radioListHorizontal {
            display: flex; 
            list-style: none; 
            padding: 0;
        }

            .radioListHorizontal label {
                margin-right: 15px;
            }
    </style>
    <title>Listar Atletas</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4>Controle de Atletas</h4>
            <h2>Listar Atletas</h2>
          
            <div>
                <asp:RadioButtonList ID="RadioButtonListFiltro" runat="server" CssClass="radioListHorizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListFiltro_SelectedIndexChanged">
                    <asp:ListItem Text="Número de Camisa" Value="NumeroCamisa" />
                    <asp:ListItem Text="Apelido" Value="Apelido" />
                    <asp:ListItem Text="Classificação IMC" Value="ClassificacaoIMC" />
                </asp:RadioButtonList>
                <br />
            </div>

            <asp:Button ID="ButtonCriarNovo" runat="server" Text="Cadastrar novo atleta" OnClick="ButtonCriarNovo_Click" />

            <asp:TextBox ID="TextBoxFiltro" runat="server" placeholder="Digite o número da camisa..." AutoPostBack="true"></asp:TextBox>
            <asp:Button ID="ButtonFiltrar" runat="server" Text="Filtrar" OnClick="ButtonFiltrar_Click" />

            <br />
            <br />

            <br />
            <br />

            <asp:GridView ID="GridViewAtletas" runat="server" AutoGenerateColumns="False"
                OnRowCancelingEdit="GridViewAtletas_RowCancelingEdit"
                OnRowEditing="GridViewAtletas_RowEditing" OnRowDeleting="GridViewAtletas_RowDeleting"
                OnRowUpdating="GridViewAtletas_RowUpdating" OnRowDataBound="GridViewAtletas_RowDataBound"
                DataKeyNames="Id">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:TemplateField HeaderText="Nome Completo">
                        <ItemTemplate>
                            <asp:Label ID="LabelNomeCompleto" runat="server" Text='<%# Bind("NomeCompleto") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxNomeCompleto" runat="server" Text='<%# Bind("NomeCompleto") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Número da Camisa">
                        <ItemTemplate>
                            <asp:Label ID="LabelNumeroCamisa" runat="server" Text='<%# Bind("NumeroCamisa") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxNumeroCamisa" runat="server" Text='<%# Bind("NumeroCamisa") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Apelido">
                        <ItemTemplate>
                            <asp:Label ID="LabelApelido" runat="server" Text='<%# Bind("Apelido") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxApelido" runat="server" Text='<%# Bind("Apelido") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Posição">
                        <ItemTemplate>
                            <asp:Label ID="LabelPosicao" runat="server" Text='<%# Bind("Posicao") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxPosicao" runat="server" Text='<%# Bind("Posicao") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Idade">
                        <ItemTemplate>
                            <asp:Label ID="LabelIdade" runat="server" Text='<%# Bind("Idade") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxIdade" runat="server" Text='<%# Bind("Idade") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Altura">
                        <ItemTemplate>
                            <asp:Label ID="LabelAltura" runat="server" Text='<%# Bind("Altura") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxAltura" runat="server" Text='<%# Bind("Altura") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Peso">
                        <ItemTemplate>
                            <asp:Label ID="LabelPeso" runat="server" Text='<%# Bind("Peso") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxPeso" runat="server" Text='<%# Bind("Peso") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IMC">
                        <ItemTemplate>
                            <asp:Label ID="LabelIMC" runat="server" Text='<%# Bind("IMC") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxIMC" runat="server" Text='<%# Bind("IMC") %>' ReadOnly="true" CssClass="readonly-textbox"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Classificação IMC">
                        <ItemTemplate>
                            <asp:Label ID="LabelClassificacaoIMC" runat="server" Text='<%# Bind("ClassificacaoIMC") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxClassificacaoIMC" runat="server" Text='<%# Bind("ClassificacaoIMC") %>' ReadOnly="true" CssClass="readonly-textbox"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>

    </form>
</body>
</html>
