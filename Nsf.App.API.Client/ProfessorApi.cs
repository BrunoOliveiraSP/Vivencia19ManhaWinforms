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

        public Model.ProfessorRequest Inserir(Model.ProfessorRequest professor)
        {
            string json = JsonConvert.SerializeObject(professor);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = client.PostAsync("http://localhost:5000/Professor/", body).Result;

            string jsonresposta = LerJsonResposta(resp);
            professor = JsonConvert.DeserializeObject<Model.ProfessorRequest>(jsonresposta);

            return professor;
        }

        public Model.ProfessorRequest Alterar(Model.ProfessorRequest professor)
        {
            string json = JsonConvert.SerializeObject(professor);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/Professor/", body).Result;

            string jsonresposta = LerJsonResposta(resp);
            professor = JsonConvert.DeserializeObject<Model.ProfessorRequest>(jsonresposta);

            return professor;
        }

        public void Deletar(int id)
        {
            var resp = client.DeleteAsync("http://localhost:5000/Professor/" + id).Result;

            string jsonresposta = LerJsonResposta(resp);
        }

        public List<Model.ProfessorResponse> ListarTodos()
        {
            HttpResponseMessage resp = client.GetAsync("http://localhost:5000/Professor/").Result;

            string jsonresposta = LerJsonResposta(resp);
            List<Model.ProfessorResponse> list = JsonConvert.DeserializeObject<List<Model.ProfessorResponse>>(jsonresposta);

            return list;
        }

        public List<Model.ProfessorResponse> ConsultarPorNome(string nome)
        {
            HttpResponseMessage resp = client.GetAsync("http://localhost:5000/Professor/" + nome).Result;

            string jsonresposta = LerJsonResposta(resp);
            List<Model.ProfessorResponse> list = JsonConvert.DeserializeObject<List<Model.ProfessorResponse>>(jsonresposta);

            return list;
        }

        private string LerJsonResposta(HttpResponseMessage resp)
        {
            string jsonResposta = resp.Content
                                      .ReadAsStringAsync()
                                      .Result;

            if (resp.IsSuccessStatusCode == false)
            {
                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }

            return jsonResposta;
        }



    }
}
