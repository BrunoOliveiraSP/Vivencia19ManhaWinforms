using Newtonsoft.Json;
using Nsf.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class TurmaAPI
    {
        public void InserirTurma(Nsf.App.Model.TurmaModel turma)
        {
            HttpClient client = new HttpClient();

            string json = JsonConvert.SerializeObject(turma);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/Turma/", body).Result;

            string jsonResposta = resp.Content.ReadAsStringAsync().Result;

            if (resp.IsSuccessStatusCode == false)
            {
                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }

        public List<Nsf.App.Model.TurmaResponse> ConsultarTurmaPorAnoLetivo(int id)
        {
            HttpClient client = new HttpClient();

            string json = client.GetAsync("http://localhost:5000/Turma/" + id).Result
                                                                         .Content
                                                                         .ReadAsStringAsync()
                                                                         .Result;

            List<TurmaResponse> Turma = JsonConvert.DeserializeObject<List<TurmaResponse>>(json);
            return Turma;
        }
        public void Alterar(Nsf.App.Model.TurmaModel model)
        {
            HttpClient client = new HttpClient();

            string json = JsonConvert.SerializeObject(model);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/Turma/", body).Result;

            if (resp.IsSuccessStatusCode == false)
            {
                string jsonResposta = resp.Content
                                          .ReadAsStringAsync()
                                          .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }
        public void Remover(int id)
        {
            HttpClient client = new HttpClient();
            var resp = client.DeleteAsync("http://localhost:5000/Turma/" + id).Result;

            string jsonResposta = resp.Content.ReadAsStringAsync().Result;

            if (resp.IsSuccessStatusCode == false)
            {
                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }
    }
}
