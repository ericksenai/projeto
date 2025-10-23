using System.Diagnostics;
using Ligação.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Cadastrar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        public IActionResult Editar()
        {
            return View();
        }
        public IActionResult Excluir()
        {
            return View();
        }
        public IActionResult Procurar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Cadastrar(string Usuario, string Senha)
        {
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Senha))
            {
                ViewData["Mensagem"] = "Usuário e Senha são obrigatórios.";
                return View();
            }

            string connectionString = "Server=localhost;Database=cad_login;User=root;Password=123456;";
            string query = "INSERT INTO login (Usuario, Senha) VALUES (@Usuario, @Senha)";

            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Usuario", Usuario);
                command.Parameters.AddWithValue("@Senha", Senha);

                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();

                ViewData["Mensagem"] = linhasAfetadas > 0
                    ? "Usuário cadastrado com sucesso!"
                    : "Falha ao cadastrar usuário.";
            }
            catch (MySqlException ex)
            {
                ViewData["Message"] = $"Erro ao conectar ao banco de dados: {ex.Message}";
            }
            return View();

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpPost]
        public IActionResult Editar(int IdLogin, string Usuario, string Senha)
        {
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Senha))
            {
                ViewData["Message"] = "O nome de usuário e a senha não podem estar vazios";
                return View();

            }
            string connectionString = "Server=localhost;Database=cad_login;Uid=root;Pwd=123456;";
            string query = "UPDATE login SET Usuario = @Usuario, Senha = @Senha WHERE idLogin = @IdLogin";

            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Usuario", Usuario);
                command.Parameters.AddWithValue("@Senha", Senha);
                command.Parameters.AddWithValue("@IdLogin", IdLogin);

                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();

                ViewData["Message"] = linhasAfetadas > 0
                    ? "Usuário alterado com sucesso!"
                    : "Ocorreu um erro e o Usuário não foi alterado.";
            }
            catch (Exception ex)
            {
                ViewData["Message"] = $"Erro ao conectar ao banco de dados: {ex.Message}";
            }
            return View();

        }
        [HttpPost]
        public IActionResult Excluir(int IdLogin, string Usuario, string Senha)
        {
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Senha))
            {
                ViewData["Message"] = "O nome de usuário e a senha não podem estar vazios";
                return View();

            }
            string connectionString = "Server=localhost;Database=cad_login;Uid=root;Pwd=123456;";
            string query = "DELETE FROM login WHERE Usuario = @Usuario AND Senha = @Senha AND idLogin = @IdLogin";

            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Usuario", Usuario);
                command.Parameters.AddWithValue("@Senha", Senha);
                command.Parameters.AddWithValue("@IdLogin", IdLogin);

                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();

                ViewData["Message"] = linhasAfetadas > 0
                    ? "Usuário excluido com sucesso!"
                    : "Ocorreu um erro e o Usuário não foi excluido.";
            }
            catch (Exception ex)
            {
                ViewData["Message"] = $"Erro ao conectar ao banco de dados: {ex.Message}";
            }
            return View();

        }
        [HttpPost]
        public IActionResult Procurar(int idLogin)
        {
            string connectionString = "Server=localhost;Database=cad_login;Uid=root;Pwd=123456;";
            string query = "SELECT * FROM login WHERE  idLogin = @IdLogin";

            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@idLogin", idLogin);
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ViewData["Message"] = "Usuário encontrado!";
                    ViewData["UsuarioEncontrado"] = reader["Usuario"].ToString();
                    ViewData["SenhaEncontrada"] = reader["Senha"].ToString();
                }
                else
                {
                    ViewData["Message"] = "Usuário não encontrado.";
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = $"Erro ao conectar ao banco de dados: {ex.Message}";
            }
            return View();

        }

    }

} 

