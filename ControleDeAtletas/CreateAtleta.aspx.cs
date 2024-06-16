using ControleDeAtletas.BLL;
using ControleDeAtletas.DTO.ControleDeAtletas.DTO;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace ControleDeAtletas
{
    public partial class CreateAtleta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
    
            }
        }

        protected void ButtonSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int numeroCamisa;
                if (!int.TryParse(TextBoxNumeroCamisa.Text, out numeroCamisa))
                {
                    LiteralErrorMessage.Text = "<p class='error-message'>Número da camisa inválido</p>";
                    return;
                }

                if (!ValidarCampoSemNumeros(TextBoxNomeCompleto.Text))
                {
                    LiteralErrorMessage.Text = "<p class='error-message'>Nome inválido (não deve conter números)</p>";
                    return;
                }

                if (!ValidarCampoSemNumeros(TextBoxApelido.Text))
                {
                    LiteralErrorMessage.Text = "<p class='error-message'>Apelido inválido (não deve conter números)</p>";
                    return;
                }

                if (!ValidarCampoSemNumeros(TextBoxPosicao.Text))
                {
                    LiteralErrorMessage.Text = "<p class='error-message'>Posição inválida (não deve conter números)</p>";
                    return;
                }

                double altura;
                if (!double.TryParse(TextBoxAltura.Text.Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out altura))
                {
                    LiteralErrorMessage.Text = "<p class='error-message'>Altura inválida</p>";
                    return;
                }

                double peso;
                if (!double.TryParse(TextBoxPeso.Text.Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out peso))
                {
                    LiteralErrorMessage.Text = "<p class='error-message'>Peso inválido</p>";
                    return;
                }

                DateTime dataNascimento;
                if (!DateTime.TryParseExact(TextBoxDataNascimento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
                {
                    LiteralErrorMessage.Text = "<p class='error-message'>Data de nascimento inválida</p>";
                    return;
                }

                AtletaDTO novoAtletaDTO = new AtletaDTO
                {
                    NumeroCamisa = numeroCamisa,
                    Apelido = TextBoxApelido.Text,
                    Posicao = TextBoxPosicao.Text,
                    NomeCompleto = TextBoxNomeCompleto.Text,
                    Altura = altura,
                    Peso = peso,
                    DataNascimento = dataNascimento
                };

                AtletaBLL atletaBLL = new AtletaBLL();
                atletaBLL.InserirAtleta(novoAtletaDTO);

                Response.Redirect("ListarAtletas.aspx");
            }
            catch (Exception ex)
            {
                LiteralErrorMessage.Text = $"<p class='error-message'>Erro ao salvar o atleta: {ex.Message}</p>";
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarAtletas.aspx");
        }

        private bool ValidarCampoSemNumeros(string texto)
        {
            Regex regex = new Regex(@"^[^\d]+$");

            return regex.IsMatch(texto);
        }
    }
}
