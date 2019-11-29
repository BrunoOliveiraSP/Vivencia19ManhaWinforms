using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class CursoApi
    {
        public List<Model.CursoModel> ListarTodos()
        {
            HttpClient client = new HttpClient();

            string json = client.GetAsync("http://localhost:5000/Curso")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            List<Model.CursoModel> cursos = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            return cursos;
        }
        public void Remover(int id)
        {
            HttpClient client = new HttpClient();
            var resp = client.DeleteAsync("http:localhost:5000/Curso/" + id).Result;
        }
    }
}
