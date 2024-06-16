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

                SetDefaultRadioButtonSelection("NumeroCamisa");

                BindGridView();
            }
        }

        private void SetDefaultRadioButtonSelection(string value)
        {
            foreach (ListItem item in RadioButtonListFiltro.Items)
            {
                if (item.Value == value)
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        private void BindGridView()
        {
            try
            {
                List<AtletaDTO> atletas = GetAtletas();
                GridViewAtletas.DataSource = atletas;
                GridViewAtletas.DataBind();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Erro ao carregar atletas: " + ex.Message);
            }
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
                ShowErrorMessage("Erro ao filtrar atletas por número da camisa: " + ex.Message);
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
                ShowErrorMessage("Erro ao filtrar atletas por classificação de IMC: " + ex.Message);
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
            catch (ArgumentException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Erro ao filtrar atletas por apelido: " + ex.Message);
            }
        }

        protected void ButtonFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TextBoxFiltro.Text))
                {
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
            catch (Exception ex)
            {
                ShowErrorMessage("Erro ao filtrar atletas: " + ex.Message);
            }
        }

        protected void TextBoxFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TextBoxFiltro.Text))
                {
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
            catch (Exception ex)
            {
                ShowErrorMessage("Erro ao filtrar atletas: " + ex.Message);
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

            BindGridView();
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
                ShowErrorMessage("Erro ao editar atleta: " + ex.Message);
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

                    if (nomeCompleto.Any(char.IsDigit))
                    {
                        textBoxNomeCompleto.Text = ""; 

                        ScriptManager.RegisterStartupScript(this, GetType(), "ErroNomeCompletoNumeros", "alert('Nome inválido (não deve conter números).');", true);
                        GridViewAtletas.EditIndex = e.RowIndex; 
                        BindGridView();
                        return;
                    }

                    TextBox textBoxNumeroCamisa = row.FindControl("TextBoxNumeroCamisa") as TextBox;
                    if (textBoxNumeroCamisa != null)
                    {
                        string numeroCamisaText = textBoxNumeroCamisa.Text.Trim();
                        int numeroCamisa;
                        if (!int.TryParse(numeroCamisaText, out numeroCamisa))
                        {
                            textBoxNumeroCamisa.Text = "";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ErroNumeroCamisa", "alert('O número da camisa deve conter apenas números.');", true);
                            GridViewAtletas.EditIndex = e.RowIndex;
                            BindGridView();
                            return;
                        }
                    }

                    TextBox textBoxApelido = row.FindControl("TextBoxApelido") as TextBox;
                    if (textBoxApelido != null)
                    {
                        string apelido = textBoxApelido.Text.Trim();
                        if (!apelido.All(char.IsLetter))
                        {
                            textBoxApelido.Text = "";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ErroApelido", "alert('O apelido deve conter apenas letras.');", true);
                            GridViewAtletas.EditIndex = e.RowIndex;
                            BindGridView();
                            return;
                        }
                    }

                    TextBox textBoxPosicao = row.FindControl("TextBoxPosicao") as TextBox;
                    if (textBoxPosicao != null)
                    {
                        string posicao = textBoxPosicao.Text.Trim();
                        if (!string.IsNullOrWhiteSpace(posicao) && !posicao.All(char.IsLetter))
                        {
                            textBoxPosicao.Text = "";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ErroPosicao", "alert('A posição deve conter apenas letras.');", true);
                            GridViewAtletas.EditIndex = e.RowIndex; 
                            BindGridView();
                            return;
                        }
                    }

                    TextBox textBoxIdade = row.FindControl("TextBoxIdade") as TextBox;
                    if (textBoxIdade != null)
                    {
                        string idadeText = textBoxIdade.Text.Trim();
                        int idade;
                        if (!int.TryParse(idadeText, out idade) || idade < 0)
                        {
                            textBoxIdade.Text = ""; 

                            ScriptManager.RegisterStartupScript(this, GetType(), "ErroIdade", "alert('A idade deve ser um número inteiro positivo.');", true);
                            GridViewAtletas.EditIndex = e.RowIndex; 
                            BindGridView();
                            return;
                        }
                    }

                    TextBox textBoxAltura = row.FindControl("TextBoxAltura") as TextBox;
                    if (textBoxAltura != null)
                    {
                        string alturaText = textBoxAltura.Text.Trim();
                        double altura;
                        if (!double.TryParse(alturaText, out altura) || altura <= 0)
                        {
                            textBoxAltura.Text = "";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ErroAltura", "alert('A altura deve ser um número decimal.');", true);
                            GridViewAtletas.EditIndex = e.RowIndex;
                            BindGridView();
                            return;
                        }
                    }

                    TextBox textBoxPeso = row.FindControl("TextBoxPeso") as TextBox;
                    if (textBoxPeso != null)
                    {
                        string pesoText = textBoxPeso.Text.Trim();
                        double peso;
                        if (!double.TryParse(pesoText, out peso) || peso <= 0)
                        {
                            textBoxPeso.Text = "";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ErroPeso", "alert('O peso deve ser um número decimal positivo.');", true);
                            GridViewAtletas.EditIndex = e.RowIndex; 
                            BindGridView();
                            return;
                        }
                    }

                    AtletaDTO atletaDTO = new AtletaDTO
                    {
                        Id = id,
                        NomeCompleto = nomeCompleto,
                        NumeroCamisa = Convert.ToInt32(textBoxNumeroCamisa.Text),
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
                    ShowErrorMessage("Erro: Nome completo não encontrado.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Erro ao atualizar atleta: " + ex.Message);
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
                ShowErrorMessage("Erro ao excluir atleta: " + ex.Message);
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
                ShowErrorMessage("Erro ao excluir atleta: " + ex.Message);
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
                Label lblIMC = (Label)e.Row.FindControl("LabelIMC");
                Label lblClassificacaoIMC = (Label)e.Row.FindControl("LabelClassificacaoIMC");
                if (lblIMC != null && lblClassificacaoIMC != null)
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

        protected void ButtonCriarNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateAtleta.aspx");
        }

        private void ShowErrorMessage(string message)
        {
            LiteralErrorMessage.Text = $"<p class='error-message'>{message}</p>";
        }
    }
}
