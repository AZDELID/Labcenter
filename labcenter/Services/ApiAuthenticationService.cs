using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using labcenter.Models;
using Newtonsoft.Json;

namespace labcenter.Services
{
    public class ApiAuthenticationService : IUserAuthenticationService, ISessionLifecycleService
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public ApiAuthenticationService(string baseUrl)
        {
            this.baseUrl = baseUrl.TrimEnd('/');

            client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(20)
            };
        }

        public async Task<AuthenticatedUser> LoginAsync(string codigo, string password)
        {
            var data = new
            {
                codigo,
                password,
                pc = Environment.MachineName
            };

            dynamic obj = await PostAsync("/login", data);

            if (obj != null && obj.ok == true)
            {
                return new AuthenticatedUser(
                    (int)obj.usuario.id,
                    (string)obj.usuario.nombre
                );
            }

            return null;
        }

        public async Task StartSessionAsync(int userId, int pcId)
        {
            var data = new
            {
                usuario_id = userId,
                pc_id = pcId
            };

            await PostAsync("/iniciar", data);
        }

        public async Task EndSessionAsync(int pcId)
        {
            var data = new
            {
                pc_id = pcId
            };

            await PostAsync("/finalizar", data);
        }

        private async Task<dynamic> PostAsync(string path, object data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                );

                HttpResponseMessage response =
                    await client.PostAsync(baseUrl + path, content);

                string result =
                    await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        $"Servidor respondió {response.StatusCode}\n\n{result}"
                    );
                }

                return JsonConvert.DeserializeObject(result);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(
                    "No se pudo conectar al servidor.\n\n" +
                    "Verifique su conexión a Internet.\n\n" +
                    ex.Message
                );
            }
            catch (TaskCanceledException)
            {
                throw new Exception(
                    "La conexión con el servidor agotó el tiempo de espera."
                );
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error:\n\n" + ex.Message
                );
            }
        }
    }
}