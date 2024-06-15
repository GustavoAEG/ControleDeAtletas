using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeAtletas.DTO
{
 
    namespace ControleDeAtletas.DTO
    {
        public class AtletaDTO
        {
            public int Id { get; set; }
            public string NomeCompleto { get; set; }
            public string Apelido { get; set; }
            public DateTime? DataNascimento { get; set; }
            public double Altura { get; set; }
            public double Peso { get; set; }
            public string Posicao { get; set; }
            public int NumeroCamisa { get; set; }
            public int Idade { get; set; }
            public double IMC { get; set; }
            public string ClassificacaoIMC { get; set; }
        }
    }

}
