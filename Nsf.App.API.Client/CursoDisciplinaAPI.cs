using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    class CursoDisciplinaAPI
    {
         public List<Model.CursoModel>  Consultar(Nsf.App.Model.CursoDisciplinaRequest cursos)
        {
            HttpClient client = new HttpClient();
            string json = client.GetAsync("http://localhost:5000/CursoDisciplina/")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            List<Model.CursoModel> curso = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            return curso;

        }
    }
}
