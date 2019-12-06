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

        public List<DiciplinaModel> ListarSigla(string text)
        {
            try
            {
                string json = client.GetAsync($"http://localhost:5000/Diciplina/Sigla/{text}")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;
                return JsonConvert.DeserializeObject<List<DiciplinaModel>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro, Tente Novamente, Messagem de erro: {ex.Message}");
                return null;
            }
            
        }

        public void Inserir(DiciplinaModel diciplina)
        {

            string json = JsonConvert.SerializeObject(diciplina);

            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage respota = client.PostAsync("http://localhost:5000/Diciplina/Inserir", body).Result;

            if (respota.IsSuccessStatusCode == false)
            {
                string jsonResposta = respota.Content
                                             .ReadAsStringAsync()
                                             .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }

        public void Alterar(DiciplinaModel diciplina)
        {
            try
            {

                string json = JsonConvert.SerializeObject(diciplina);

                StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage respota = client.PutAsync("http://localhost:5000/Diciplina/Alterar", body).Result;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Erro, Tente Novamente, Messagem de erro:{e.Message}");
            }
        }

        public void Remover(int id)
        {
            try
            {
                string json = JsonConvert.SerializeObject(id);

                HttpResponseMessage respota = client.DeleteAsync($"http://localhost:5000/Diciplina/Remover/{id}").Result;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Erro, Tente Novamente, Messagem de erro:{e.Message}");
            }
        }

        public List<DiciplinaModel> ListarDisciplina(string disciplina)
        {
            string json;

            if (string.IsNullOrEmpty(disciplina))
            {
                json = client.GetAsync("http://localhost:5000/Diciplina/ConsultarTudo")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;
                return JsonConvert.DeserializeObject<List<DiciplinaModel>>(json);
            }

            json = client.GetAsync($"http://localhost:5000/Diciplina/ConsultarPorDisciplina/{disciplina}")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;
            return JsonConvert.DeserializeObject<List<DiciplinaModel>>(json);
        }
    }
}
