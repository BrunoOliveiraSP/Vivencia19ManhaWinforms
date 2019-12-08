using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Nsf.App.Model.Model;

namespace Nsf.App.API.Client
{
    public class DisciplinaAPI
    {
        HttpClient client = new HttpClient();

        public List<DiciplinaModel> ListarSigla(string Sigla)
        {

            string json = client.GetAsync($"http://localhost:5000/Diciplina/Sigla/{Sigla}")
                            .Result
                            .Content
                            .ReadAsStringAsync()
                            .Result;
            return JsonConvert.DeserializeObject<List<DiciplinaModel>>(json);
        }

        public int Inserir(DiciplinaModel Disciplina)
        {

            string json = JsonConvert.SerializeObject(Disciplina);

            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage respota = client.PostAsync("http://localhost:5000/Diciplina/Inserir", body).Result;

            string JsonResposta = LerJson(respota);
            Disciplina = JsonConvert.DeserializeObject<DiciplinaModel>(JsonResposta);

            return Disciplina.IdDisciplina;
        }

        public void Alterar(DiciplinaModel Disciplina)
        {
         
            string json = JsonConvert.SerializeObject(Disciplina);
         
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
         
            HttpResponseMessage respota = client.PutAsync("http://localhost:5000/Diciplina/Alterar", body).Result;

            string JsonResposta = LerJson(respota);
        }

        public void Remover(int id)
        {
            string json = JsonConvert.SerializeObject(id);
         
            HttpResponseMessage respota = client.DeleteAsync($"http://localhost:5000/Diciplina/Remover/{id}").Result;

            if (respota.IsSuccessStatusCode == false)
            {
                string jsonResposta = respota.Content
                                             .ReadAsStringAsync()
                                             .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }

        public List<DiciplinaModel> ListarDisciplina(string disciplina)
        {

            string json = client.GetAsync($"http://localhost:5000/Diciplina/ConsultarPorDisciplina/{disciplina}")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;
            return JsonConvert.DeserializeObject<List<DiciplinaModel>>(json);
        }

        public List<DiciplinaModel> ListarTudo()
        {
            string json;

            json = client.GetAsync("http://localhost:5000/Diciplina/ConsultarTudo")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;
            return JsonConvert.DeserializeObject<List<DiciplinaModel>>(json);
        }

        public string LerJson(HttpResponseMessage RespostaAPI)
        {
            string Json = RespostaAPI.Content.ReadAsStringAsync().Result;

            if (RespostaAPI.IsSuccessStatusCode == false)
            {
                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(Json);
                throw new ArgumentException(erro.Mensagem);
            }

            return Json;
        }
    }
}
