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

            var resp = client.PostAsync("http://localhost:5000/turma/", body).Result;
        }
        public List<Nsf.App.Model.TurmaModel> ListarTodos()
        {
            HttpClient client = new HttpClient();

            string json = client.GetAsync("http://localhost:5000/turma/").Result
                                                                         .Content
                                                                         .ReadAsStringAsync()
                                                                         .Result;
            List<TurmaModel> turma = JsonConvert.DeserializeObject<List<TurmaModel>>(json);
            return turma;
        }
    }
}
