using ControleDeAtletas.BLL;
using ControleDeAtletas.DTO.ControleDeAtletas.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleDeAtletas
{
    public partial class CreateAtleta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void ButtonSalvar_Click(object sender, EventArgs e)
        {
            int numeroCamisa;
            if (!int.TryParse(TextBoxNumeroCamisa.Text, out numeroCamisa))
            {
                Response.Write("Número da camisa inválido");
                return;
            }
            double altura;
            if (!double.TryParse(TextBoxAltura.Text.Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out altura))
            {

                Response.Write("Altura inválida");
                return;
            }

            double peso;
            if (!double.TryParse(TextBoxPeso.Text.Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out peso))
            {
                Response.Write("Peso inválido");
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
                DataNascimento = DateTime.ParseExact(TextBoxDataNascimento.Text, "dd/MM/yyyy", null),
    
            };

            AtletaBLL atletaBLL = new AtletaBLL();
            atletaBLL.InserirAtleta(novoAtletaDTO);

            Response.Redirect("ListarAtletas.aspx");
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarAtletas.aspx");
        }

    }
}
