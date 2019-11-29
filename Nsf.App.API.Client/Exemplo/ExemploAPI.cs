using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class ExemploAPI
    {
        public List<Nsf.App.Model.ExemploModel> Listar()
        {
            HttpClient client = new HttpClient();

            string json = client.GetAsync("http://localhost:5000/Exemplo")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            List<Nsf.App.Model.ExemploModel> lista =
                JsonConvert.DeserializeObject<List<Nsf.App.Model.ExemploModel>>(json);

            return lista;
        }
    }
}
