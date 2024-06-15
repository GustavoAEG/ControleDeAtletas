using System;
using System.Collections.Generic;
using ControleDeAtletas.DAL;
using ControleDeAtletas.DTO;
using ControleDeAtletas.DTO.ControleDeAtletas.DTO;

namespace ControleDeAtletas.BLL
{
    public class AtletaBLL
    {
        private AtletaDAL atletaDAL = new AtletaDAL();

        public void InserirAtleta(AtletaDTO atletaDTO)
        {
            atletaDTO.IMC = CalcularIMC(atletaDTO.Altura, atletaDTO.Peso);
            atletaDTO.ClassificacaoIMC = ClassificarIMC(atletaDTO.IMC);
            atletaDTO.Idade = CalcularIdade((DateTime)atletaDTO.DataNascimento);

            atletaDAL.InserirAtleta(atletaDTO);
        }

        public List<AtletaDTO> GetAtletas()
        {
            List<AtletaDTO> atletas = atletaDAL.GetAtletas();
            foreach (var atleta in atletas)
            {
                atleta.ClassificacaoIMC = ClassificarIMC(atleta.IMC);
            }
            return atletas;
        }

        private int CalcularIdade(DateTime DataNascimento)
        {
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - DataNascimento.Year;
            if (DataNascimento.Date > hoje.AddYears(-idade))
                idade--;
            return idade;
        }

        public void ExcluirAtleta(int id)
        {
            try
            {
                atletaDAL.ExcluirAtleta(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na BLL ao excluir atleta", ex);
            }
        }

        public void AtualizarAtleta(AtletaDTO atletaDTO)
        {
            try
            {
                atletaDTO.IMC = CalcularIMC(atletaDTO.Altura, atletaDTO.Peso);
                atletaDTO.ClassificacaoIMC = ClassificarIMC(atletaDTO.IMC);

                atletaDAL.AtualizarAtleta(atletaDTO);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o atleta", ex);
            }
        }

        public List<AtletaDTO> FiltrarAtletasPorNumeroCamisa(int numeroCamisa)
        {
            List<AtletaDTO> atletasFiltrados = new List<AtletaDTO>();

            try
            {
                List<AtletaDTO> todosAtletas = atletaDAL.GetAtletas();

                foreach (var atleta in todosAtletas)
                {
                    if (atleta.NumeroCamisa == numeroCamisa)
                    {
                        atletasFiltrados.Add(atleta);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar atletas por número de camisa na BLL", ex);
            }

            return atletasFiltrados;
        }

        public List<AtletaDTO> FiltrarAtletasPorApelido(string apelido)
        {
            List<AtletaDTO> atletasFiltrados = new List<AtletaDTO>();

            try
            {
                List<AtletaDTO> todosAtletas = atletaDAL.GetAtletas();

                foreach (var atleta in todosAtletas)
                {
                    if (atleta.Apelido.Equals(apelido, StringComparison.OrdinalIgnoreCase))
                    {
                        atletasFiltrados.Add(atleta);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar atletas por apelido na BLL", ex);
            }

            return atletasFiltrados;
        }

        public List<AtletaDTO> FiltrarAtletasPorClassificacaoIMC(string classificacaoIMC)
        {
            List<AtletaDTO> atletasFiltrados = new List<AtletaDTO>();

            try
            {
                List<AtletaDTO> todosAtletas = atletaDAL.GetAtletas();

                foreach (var atleta in todosAtletas)
                {
                    if (atleta.ClassificacaoIMC.Equals(classificacaoIMC, StringComparison.OrdinalIgnoreCase))
                    {
                        atletasFiltrados.Add(atleta);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar atletas por classificação de IMC na BLL", ex);
            }

            return atletasFiltrados;
        }

        public static double CalcularIMC(double altura, double peso)
        {
            return peso / (altura * altura);
        }

        public static string ClassificarIMC(double imc)
        {
            if (imc < 18.5)
            {
                return "Abaixo do peso normal";
            }
            else if (imc >= 18.5 && imc < 25)
            {
                return "Peso normal";
            }
            else if (imc >= 25 && imc < 30)
            {
                return "Sobrepeso";
            }
            else if (imc >= 30 && imc < 35)
            {
                return "Obesidade Classe I";

            }
            else if (imc >= 35 && imc < 40)
            {
                return "Obesidade Classe II";
            }
            else
            {
                return "Obesidade Classe III";
            }
        }
    }
}
