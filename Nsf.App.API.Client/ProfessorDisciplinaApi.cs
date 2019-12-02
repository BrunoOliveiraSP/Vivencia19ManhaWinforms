using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
   public class ProfessorDisciplinaApi
    {
        HttpClient client = new HttpClient();

        public void Inserir(Model.ProfessorDisciplinaModel professordisciplna)
        {
            string json = JsonConvert.SerializeObject(professordisciplna);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/ProfessorDisciplina/", body).Result;

            if (resp.IsSuccessStatusCode == false)
            {
                string jsonResposta = resp.Content
                                          .ReadAsStringAsync()
                                          .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }

        public void Alterar(Model.ProfessorDisciplinaModel professordisciplna)
        {
            string json = JsonConvert.SerializeObject(professordisciplna);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/ProfessorDisciplina/", body).Result;

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
            var resp = client.DeleteAsync("http://localhost:5000/ProfessorDisciplina/" + id).Result;

            if (resp.IsSuccessStatusCode == false)
            {
                string jsonResposta = resp.Content
                                          .ReadAsStringAsync()
                                          .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }

        public List<Model.ProfessorDisciplinaModel> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/ProfessorDisciplina/").Result.Content.ReadAsStringAsync().Result;

            List<Model.ProfessorDisciplinaModel> list = JsonConvert.DeserializeObject <List< Model.ProfessorDisciplinaModel >> (json);

            return list;
        }

        public List<Model.ProfessorDisciplinaModel> ListarPorIdProfessor(int id)
        {
            string json = client.GetAsync("http://localhost:5000/ProfessorDisciplina/idprofessor/" + id).Result.Content.ReadAsStringAsync().Result;

            List<Model.ProfessorDisciplinaModel> list = JsonConvert.DeserializeObject<List<Model.ProfessorDisciplinaModel>>(json);

            return list;
        }
    }
}
