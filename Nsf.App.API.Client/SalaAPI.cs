using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class SalaAPI
    {
        HttpClient client = new HttpClient();


        public Nsf.App.Model.SalaModel Inserir(Nsf.App.Model.SalaModel modelo)
        {
            string json = JsonConvert.SerializeObject(modelo);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/Sala/", body).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(resp);

            Nsf.App.Model.SalaModel sala =
                JsonConvert.DeserializeObject<Nsf.App.Model.SalaModel>(resp);

            return sala;
        }
        public List<Nsf.App.Model.SalaModel> ListarPorLocal(string nome)
        {
            string json;

            if (string.IsNullOrEmpty(nome) == true)
            {
               json = client.GetAsync("http://localhost:5000/Sala/").Result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                json = client.GetAsync("http://localhost:5000/Sala/Buscar/" + nome).Result.Content.ReadAsStringAsync().Result;
            }
           
            VerificarErro(json);

            List<Nsf.App.Model.SalaModel> salas = JsonConvert.DeserializeObject<List<Nsf.App.Model.SalaModel>>(json);
            return salas;
        }

        public List<Nsf.App.Model.SalaModel> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/Sala/").Result.Content.ReadAsStringAsync().Result;
            VerificarErro(json);

            List<Nsf.App.Model.SalaModel> salas = JsonConvert.DeserializeObject<List<Nsf.App.Model.SalaModel>>(json);
            return salas;
        }

        public Nsf.App.Model.SalaModel BuscarPorID(int id)
        {
            string json = client.GetAsync("http://localhost:5000/Sala/" + id).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(json);

            Nsf.App.Model.SalaModel sala = JsonConvert.DeserializeObject<Nsf.App.Model.SalaModel>(json);
            return sala;
        }
        public Nsf.App.Model.SalaModel BuscarPorSala(string nome)
        {
            string json = client.GetAsync("http://localhost:5000/Sala/BuscarPorNome/" + nome).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(json);

            Nsf.App.Model.SalaModel sala = JsonConvert.DeserializeObject<Nsf.App.Model.SalaModel>(json);
            return sala;
        }

        public void Alterar(Nsf.App.Model.SalaModel modelo)
        {
            string json = JsonConvert.SerializeObject(modelo);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/Sala/", body).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(resp);
        }

        public void Deletar(int id)
        {
            var resp = client.DeleteAsync("http://localhost:5000/Sala/" + id).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(resp);
        }
        private void VerificarErro(string resposta)
        {
            if (resposta.ToLower().Contains("codigoerro"))
            {
                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(resposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }
    }
}
