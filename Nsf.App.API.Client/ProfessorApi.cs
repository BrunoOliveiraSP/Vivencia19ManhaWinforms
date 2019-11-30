using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class ProfessorApi
    {
        HttpClient client = new HttpClient();

        public void Inserir(Model.ProfessorModel professor)
        {
            string json = JsonConvert.SerializeObject(professor);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/Professor/", body).Result;

            if (resp.IsSuccessStatusCode == false)
            {
                string jsonResposta = resp.Content
                                          .ReadAsStringAsync()
                                          .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }

        public void Alterar(Model.ProfessorModel professor)
        {
            string json = JsonConvert.SerializeObject(professor);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/Professor/", body).Result;

            if (resp.IsSuccessStatusCode == false)
            {
                string jsonResposta = resp.Content
                                          .ReadAsStringAsync()
                                          .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }

        public void Deletar(int id)
        {
            var resp = client.DeleteAsync("http://localhost:5000/Professor/" + id).Result;

            if (resp.IsSuccessStatusCode == false)
            {
                string jsonResposta = resp.Content
                                          .ReadAsStringAsync()
                                          .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }

        public List<Model.ProfessorModel> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/Professor/").Result.Content.ReadAsStringAsync().Result;

            List<Model.ProfessorModel> list = JsonConvert.DeserializeObject<List<Model.ProfessorModel>>(json);

            return list;
        }

        public List<Model.ProfessorModel> ConsultarPorNome(string nome)
        {
            string json = client.GetAsync("http://localhost:5000/Professor/nome/" + nome).Result.Content.ReadAsStringAsync().Result;

            List<Model.ProfessorModel> list = JsonConvert.DeserializeObject<List<Model.ProfessorModel>>(json);

            return list;
        }
    }
}
