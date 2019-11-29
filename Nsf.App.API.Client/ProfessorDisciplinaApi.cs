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

        public void Inserir(Nsf.App.Model.ProfessorDisciplinaModel professordisciplna)
        {
            string json = JsonConvert.SerializeObject(professordisciplna);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/professordisciplna/", body).Result;
        }

        public void Alterar(Model.ProfessorDisciplinaModel professordisciplna)
        {
            string json = JsonConvert.SerializeObject(professordisciplna);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/professordisciplna/", body).Result;
        }

        public void Deletar(int id)
        {
            var resp = client.DeleteAsync("http://localhost:5000/professordisciplna/" + id).Result;
        }

        public List<Model.ProfessorDisciplinaModel> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/professordisciplna/").Result.Content.ReadAsStringAsync().Result;

            List<Model.ProfessorDisciplinaModel> list = JsonConvert.DeserializeObject <List< Model.ProfessorDisciplinaModel >> (json);

            return list;
        }








    }
}
