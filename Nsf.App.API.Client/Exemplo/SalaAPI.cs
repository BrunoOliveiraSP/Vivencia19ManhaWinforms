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
    public class SalaAPI
    {
        HttpClient client = new HttpClient();

        public void Inserir(Nsf.App.Model.SalaModel modelo)
        {
            string json = JsonConvert.SerializeObject(modelo);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/Sala/", body).Result;
        }

        public void Alterar(Nsf.App.Model.SalaModel modelo)
        {
            string json = JsonConvert.SerializeObject(modelo);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/Sala/", body).Result;
        }

        public void Deletar(int id)
        {
            var resp = client.DeleteAsync("http://localhost:5000/Sala/" + id).Result;
        }

        public List<Model.SalaModel> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/Sala/").Result.Content.ReadAsStringAsync().Result;

            List<Model.SalaModel> list = JsonConvert.DeserializeObject<List<Model.SalaModel>>(json);

            return list;
        }
        public List<Model.SalaModel> ListarPorInstituto(string nome)
        {
            string json = client.GetAsync("http://localhost:5000/Sala/Buscar/" + nome).Result.Content.ReadAsStringAsync().Result;

            List<Model.SalaModel> list = JsonConvert.DeserializeObject<List<Model.SalaModel>>(json);

            return list;
        }
    }
}
