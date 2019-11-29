using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class AnoLetivoAPI
    {
        public void Inserir(Nsf.App.Model.AnoLetivoModel model)
        {
            HttpClient client = new HttpClient();

            string json = JsonConvert.SerializeObject(model);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/AnoLetivo", body).Result;
        }

        public void Alterar(Nsf.App.Model.AnoLetivoModel model)
        {
            HttpClient client = new HttpClient();

            string json = JsonConvert.SerializeObject(model);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/AnoLetivo", body).Result;
        }

        public List<Nsf.App.Model.AnoLetivoModel> ConsultarTodos()
        {
            HttpClient client = new HttpClient();

            string json = client.GetAsync("http://localhost:5000/AnoLetivo")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            List<Nsf.App.Model.AnoLetivoModel> lista = JsonConvert.DeserializeObject<List<Nsf.App.Model.AnoLetivoModel>>(json);

            return lista;
        }

        public void Remover(int id)
        {
            HttpClient client = new HttpClient();
            var resp = client.DeleteAsync("http://localhost:5000/AnoLetivo" + id).Result;
        }
    }
}
