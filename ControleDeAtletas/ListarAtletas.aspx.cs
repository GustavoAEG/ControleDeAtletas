using ControleDeAtletas.BLL;
using ControleDeAtletas.DTO;
using ControleDeAtletas.DTO.ControleDeAtletas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleDeAtletas.WebUI
{
    public partial class ListarAtletas : System.Web.UI.Page
    {
        protected Literal LiteralErrorMessage; 

        protected void Page_Load(object sender, EventArgs e)
        {
            LiteralErrorMessage = (Literal)FindControl("LiteralErrorMessage");

            if (!IsPostBack)
            {
                foreach (ListItem item in RadioButtonListFiltro.Items)
                {
                    if (item.Value == "NumeroCamisa")
                    {
                        item.Selected = true;
                        break;
                    }
                }

                BindGridView();
            }
        }

        private void BindGridView()
        {
            List<AtletaDTO> atletas = GetAtletas();
            GridViewAtletas.DataSource = atletas;
            GridViewAtletas.DataBind();
        }

        private List<AtletaDTO> GetAtletas()
        {
            AtletaBLL atletaBLL = new AtletaBLL();
            return atletaBLL.GetAtletas();
        }
        private void FiltrarAtletas(int numeroCamisa)
        {
            try
            {
                AtletaBLL atletaBLL = new AtletaBLL();
                List<AtletaDTO> atletasFiltrados = atletaBLL.FiltrarAtletasPorNumeroCamisa(numeroCamisa);

                GridViewAtletas.DataSource = atletasFiltrados;
                GridViewAtletas.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar atletas por número da camisa", ex);
            }
        }
        private void FiltrarAtletasPorClassificacaoIMC(string classificacaoIMC)
        {
            try
            {
                AtletaBLL atletaBLL = new AtletaBLL();
                List<AtletaDTO> atletasFiltrados = atletaBLL.FiltrarAtletasPorClassificacaoIMC(classificacaoIMC);

                GridViewAtletas.DataSource = atletasFiltrados;
                GridViewAtletas.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar atletas por classificação de IMC", ex);
            }
        }

        private void FiltrarAtletasPorApelido(string apelido)
        {
            try
            {
                if (!apelido.All(char.IsLetter))
                {
                    throw new ArgumentException("O apelido deve conter apenas letras.");
                }

                AtletaBLL atletaBLL = new AtletaBLL();
                List<AtletaDTO> atletasFiltrados = atletaBLL.FiltrarAtletasPorApelido(apelido);

                GridViewAtletas.DataSource = atletasFiltrados;
                GridViewAtletas.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar atletas por apelido", ex);
            }
        }

        protected void ButtonFiltrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxFiltro.Text))
            {
                TextBoxFiltro.Text = "";
                BindGridView();
                return;
            }

            if (RadioButtonListFiltro.SelectedValue == "NumeroCamisa")
            {
                if (!int.TryParse(TextBoxFiltro.Text, out int numeroCamisa))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErroNumeroCamisa", "alert('O número da camisa deve conter apenas números.');", true);
                    return;
                }

                FiltrarAtletas(numeroCamisa);
            }
            else if (RadioButtonListFiltro.SelectedValue == "ClassificacaoIMC")
            {
                string filtro = TextBoxFiltro.Text.Trim();

                if (!filtro.All(char.IsLetter))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErroClassificacaoIMC", "alert('A classificação IMC deve conter apenas letras.');", true);
                    return;
                }

                FiltrarAtletasPorClassificacaoIMC(filtro);
            }
            else if (RadioButtonListFiltro.SelectedValue == "Apelido")
            {
                string filtro = TextBoxFiltro.Text.Trim();

                if (!filtro.All(char.IsLetter))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErroApelido", "alert('O apelido deve conter apenas letras.');", true);
                    return;
                }

                FiltrarAtletasPorApelido(filtro);
            }
        }

        protected void TextBoxFiltro_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxFiltro.Text))
            {
                TextBoxFiltro.Text = "";
                BindGridView();
                return;
            }

            if (RadioButtonListFiltro.SelectedValue == "NumeroCamisa" && int.TryParse(TextBoxFiltro.Text, out int numeroCamisa))
            {
                FiltrarAtletas(numeroCamisa);
            }
            else if (RadioButtonListFiltro.SelectedValue == "ClassificacaoIMC")
            {
                FiltrarAtletasPorClassificacaoIMC(TextBoxFiltro.Text.Trim());
            }
            else if (RadioButtonListFiltro.SelectedValue == "Apelido")
            {
                FiltrarAtletasPorApelido(TextBoxFiltro.Text.Trim());
            }
        }

        protected void RadioButtonListFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxFiltro.Text = "";

            if (RadioButtonListFiltro.SelectedValue == "Apelido")
            {
                TextBoxFiltro.Attributes["placeholder"] = "Digite o apelido...";
            }
            else if (RadioButtonListFiltro.SelectedValue == "NumeroCamisa")
            {
                TextBoxFiltro.Attributes["placeholder"] = "Digite o número da camisa...";
            }
            else if (RadioButtonListFiltro.SelectedValue == "ClassificacaoIMC")
            {
                TextBoxFiltro.Attributes["placeholder"] = "Digite a classificação do IMC...";
            }
        }

        protected void GridViewAtletas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewAtletas.EditIndex = e.NewEditIndex;
                BindGridView();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar atleta", ex);
            }
        }

        protected void GridViewAtletas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridViewAtletas.Rows[e.RowIndex];
                int id = Convert.ToInt32(GridViewAtletas.DataKeys[e.RowIndex].Value);

                TextBox textBoxNomeCompleto = row.FindControl("TextBoxNomeCompleto") as TextBox;
                if (textBoxNomeCompleto != null)
                {
                    string nomeCompleto = textBoxNomeCompleto.Text.Trim();

                    if (string.IsNullOrWhiteSpace(nomeCompleto))
                    {

                        LiteralErrorMessage.Text = "<p class='error-message'>Nome completo não pode estar vazio.</p>";
                        return; 
                    }

                    if (nomeCompleto.Any(char.IsDigit))
                    {
                        string script = "alert('Nome inválido (não deve conter números).');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErroNomeCompletoNumeros", script, true);              
                    }

                    AtletaDTO atletaDTO = new AtletaDTO
                    {
                        Id = id,
                        NomeCompleto = nomeCompleto,
                        NumeroCamisa = Convert.ToInt32((row.FindControl("TextBoxNumeroCamisa") as TextBox)?.Text),
                        Apelido = (row.FindControl("TextBoxApelido") as TextBox)?.Text,
                        Posicao = (row.FindControl("TextBoxPosicao") as TextBox)?.Text,
                        Idade = Convert.ToInt32((row.FindControl("TextBoxIdade") as TextBox)?.Text),
                        Altura = Convert.ToDouble((row.FindControl("TextBoxAltura") as TextBox)?.Text),
                        Peso = Convert.ToDouble((row.FindControl("TextBoxPeso") as TextBox)?.Text),
                        IMC = Convert.ToDouble((row.FindControl("TextBoxIMC") as TextBox)?.Text),
                        ClassificacaoIMC = (row.FindControl("TextBoxClassificacaoIMC") as TextBox)?.Text
                    };

                    AtletaBLL atletaBLL = new AtletaBLL();
                    atletaBLL.AtualizarAtleta(atletaDTO);

                    GridViewAtletas.EditIndex = -1;
                    BindGridView();
                }
                else
                {

                    LiteralErrorMessage.Text = "<p class='error-message'>Erro: Nome completo não encontrado.</p>";
                }
            }
            catch (Exception ex)
            {

                LiteralErrorMessage.Text = $"<p class='error-message'>Erro ao atualizar atleta: {ex.Message}</p>";
            }
        }

        protected void GridViewAtletas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GridViewAtletas.DataKeys[e.RowIndex].Value);
                ExcluirAtleta(id);
                BindGridView();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir atleta", ex);
            }
        }
        protected void ExcluirAtleta(int id)
        {
            try
            {
                AtletaBLL atletaBLL = new AtletaBLL();
                atletaBLL.ExcluirAtleta(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir atleta", ex);
            }
        }
        protected void GridViewAtletas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewAtletas.EditIndex = -1;
            BindGridView();
        }

        protected void GridViewAtletas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == 0)
                {
                    Label lblIMC = (Label)e.Row.FindControl("LabelIMC");
                    Label lblClassificacaoIMC = (Label)e.Row.FindControl("LabelClassificacaoIMC");
                    if (lblIMC != null)
                    {
                        double imcValue;
                        if (double.TryParse(lblIMC.Text, out imcValue))
                        {
                            double imcLimite = 25;

                            if (imcValue > imcLimite)
                            {
                                lblIMC.CssClass = "imc-alto";
                                lblClassificacaoIMC.CssClass = "imc-alto";
                            }
                        }
                    }
                }
            }
        }

        protected void ButtonCriarNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateAtleta.aspx");
        }
    }
}
