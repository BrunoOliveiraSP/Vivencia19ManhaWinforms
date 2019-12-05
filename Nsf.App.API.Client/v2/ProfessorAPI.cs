using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client.v2
{
    public class ProfessorAPI
    {
        HttpClient client = new HttpClient();

        public Model.ProfessorRequest Inserir(Model.ProfessorRequest professor)
        {
            string json = JsonConvert.SerializeObject(professor);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = client.PostAsync("http://localhost:5000/v2/Professor/", body).Result;

            string jsonresposta = LerJsonResposta(resp);
            professor = JsonConvert.DeserializeObject<Model.ProfessorRequest>(jsonresposta);

            return professor;
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
