using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Nsf.App.API.Client.API
{
    public class DisciplinaAPI
    {
        HttpClient client = new HttpClient();

        public List<DisciplinaAPI> ConsultarDisciplina(string text)
        {
            string json;
            if (string.IsNullOrEmpty(text))
            {
                json = client.GetAsync("http://localhost:5000/Diciplina/ConsultarTudo")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;
                return JsonConvert.DeserializeObject<List<DisciplinaAPI>>(json);
            }

            json = client.GetAsync($"http://localhost:5000/Diciplina/ConsultarPorDisciplina/?genero={text}")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;
            return JsonConvert.DeserializeObject<List<DisciplinaAPI>>(json);
        }
        //    public void Inserir(FilmeModels Filme)
        //{
        //    string json = JsonConvert.SerializeObject(Filme);
        
        //    StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        //    HttpResponseMessage respota = client.PostAsync("http://localhost:5000/Filme/Inserir", body).Result;
        //}
        
        //internal void Alterar(FilmeModels filme)
        //{
        //    string json = JsonConvert.SerializeObject(filme);
        
        //    StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        //    HttpResponseMessage respota = client.PutAsync("http://localhost:5000/Filme/Alterar", body).Result;
        //}
        
        //internal void Remover(int id)
        //{
        //    string json = JsonConvert.SerializeObject(id);
        
        //    HttpResponseMessage respota = client.DeleteAsync("http://localhost:5000/Filme/Remover/?id=" + id).Result;
        //}

        //internal List<DisciplinaAPI> ListarGenero(string genero)
        //{
        //    string json;
        //    if (string.IsNullOrEmpty(genero))
        //    {
        //        json = client.GetAsync("http://localhost:5000/Filme/ListarTudo")
        //                        .Result
        //                        .Content
        //                        .ReadAsStringAsync()
        //                        .Result;
        //        return JsonConvert.DeserializeObject<List<FilmeModels>>(json);
        //    }

        //    json = client.GetAsync($"http://localhost:5000/Filme/ListarGenero/?genero={genero}")
        //                        .Result
        //                        .Content
        //                        .ReadAsStringAsync()
        //                        .Result;
        //    return JsonConvert.DeserializeObject<List<FilmeModels>>(json);
        //}
    
    }
}
