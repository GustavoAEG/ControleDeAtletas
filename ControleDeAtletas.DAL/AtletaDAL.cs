using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ControleDeAtletas.DTO;
using ControleDeAtletas.DTO.ControleDeAtletas.DTO;

namespace ControleDeAtletas.DAL
{
    public class AtletaDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void InserirAtleta(AtletaDTO atletaDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Atletas (NumeroCamisa, NomeCompleto, Apelido, Posicao, Idade, Altura, Peso, IMC, ClassificacaoIMC) " +
                               "VALUES (@NumeroCamisa, @NomeCompleto, @Apelido, @Posicao, @Idade, @Altura, @Peso, @IMC, @ClassificacaoIMC)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeroCamisa", atletaDTO.NumeroCamisa);
                command.Parameters.AddWithValue("@NomeCompleto", atletaDTO.NomeCompleto);
                command.Parameters.AddWithValue("@Apelido", atletaDTO.Apelido);
                command.Parameters.AddWithValue("@Posicao", atletaDTO.Posicao);
                command.Parameters.AddWithValue("@Idade", atletaDTO.Idade);
                command.Parameters.AddWithValue("@Altura", atletaDTO.Altura);
                command.Parameters.AddWithValue("@Peso", atletaDTO.Peso);
                command.Parameters.AddWithValue("@IMC", atletaDTO.IMC);
                command.Parameters.AddWithValue("@ClassificacaoIMC", atletaDTO.ClassificacaoIMC);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<AtletaDTO> GetAtletas()
        {
            List<AtletaDTO> atletas = new List<AtletaDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, NomeCompleto, Apelido, Posicao, Idade, Altura, Peso, NumeroCamisa, DataNascimento, IMC, ClassificacaoIMC FROM Atletas";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        AtletaDTO atleta = new AtletaDTO
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            NomeCompleto = reader.GetString(reader.GetOrdinal("NomeCompleto")),
                            Apelido = reader.IsDBNull(reader.GetOrdinal("Apelido")) ? null : reader.GetString(reader.GetOrdinal("Apelido")),
                            Posicao = reader.IsDBNull(reader.GetOrdinal("Posicao")) ? null : reader.GetString(reader.GetOrdinal("Posicao")),
                            Idade = reader.GetInt32(reader.GetOrdinal("Idade")),
                            Altura = reader.IsDBNull(reader.GetOrdinal("Altura")) ? 0 : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Altura"))),
                            Peso = reader.IsDBNull(reader.GetOrdinal("Peso")) ? 0 : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Peso"))),
                            NumeroCamisa = reader.GetInt32(reader.GetOrdinal("NumeroCamisa")),
                            DataNascimento = reader.IsDBNull(reader.GetOrdinal("DataNascimento")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataNascimento")),
                            IMC = reader.IsDBNull(reader.GetOrdinal("Imc")) ? 0 : Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Imc"))),
                            ClassificacaoIMC = reader.GetString(reader.GetOrdinal("ClassificacaoIMC"))
                        };


                        atletas.Add(atleta);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao ler o registro do banco de dados: " + ex.Message);
                    }
                }
            }
            return atletas;
        }

        public void ExcluirAtleta(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Atletas WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void AtualizarAtleta(AtletaDTO atletaDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Atletas SET 
                                NomeCompleto = @NomeCompleto, 
                                Apelido = @Apelido, 
                                Altura = @Altura, 
                                Peso = @Peso, 
                                Posicao = @Posicao, 
                                NumeroCamisa = @NumeroCamisa, 
                                Idade = @Idade, 
                                IMC = @IMC, 
                                ClassificacaoIMC = @ClassificacaoIMC 
                            WHERE Id = @Id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NomeCompleto", atletaDTO.NomeCompleto);
                    command.Parameters.AddWithValue("@Apelido", atletaDTO.Apelido);
                    command.Parameters.AddWithValue("@Altura", atletaDTO.Altura);
                    command.Parameters.AddWithValue("@Peso", atletaDTO.Peso);
                    command.Parameters.AddWithValue("@Posicao", atletaDTO.Posicao);
                    command.Parameters.AddWithValue("@NumeroCamisa", atletaDTO.NumeroCamisa);
                    command.Parameters.AddWithValue("@Idade", atletaDTO.Idade);
                    command.Parameters.AddWithValue("@IMC", atletaDTO.IMC);
                    command.Parameters.AddWithValue("@ClassificacaoIMC", atletaDTO.ClassificacaoIMC);
                    command.Parameters.AddWithValue("@Id", atletaDTO.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na camada DAL ao atualizar o atleta", ex);
            }
        }

    }
}
