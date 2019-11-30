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
         public void Inserir(Nsf.App.Model.CursoDisciplinaRequest curso)
        {
            HttpClient client = new HttpClient();
            string json = client.GetAsync("http://localhost:5000/CursoDisciplina/")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            List<Model.CursoModel> cursos = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            //return cursos;

        }
    }
}
