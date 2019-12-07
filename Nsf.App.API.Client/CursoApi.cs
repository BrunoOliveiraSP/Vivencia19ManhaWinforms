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
        HttpClient client = new HttpClient();

        public void InserirCurso(Nsf.App.Model.CursoModel curso)
        {
            string json = JsonConvert.SerializeObject(curso);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/Curso/", body)
                .Result
                .Content
                .ReadAsStringAsync()
                .Result
                ;
            this.VerificarErro(resp); 
        }

        public void AlterarCurso(Model.CursoModel curso)
        {
            string json = JsonConvert.SerializeObject(curso);

            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resposta = client.PutAsync("http://localhost:5000/Curso/", body)
                                            .Result
                                            .Content
                                            .ReadAsStringAsync()
                                            .Result;
            this.VerificarErro(resposta);
        }
        public List<Model.CursoModel> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/Curso/")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            this.VerificarErro(json);
            List<Model.CursoModel> cursos = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            return cursos;
        }

        public List<Model.CursoModel> ConsultarPorNome(string nome)
        {
            string json = client.GetAsync("http://localhost:5000/Curso/ConsultarPorNome/" + nome)
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            this.VerificarErro(json);
            List<Model.CursoModel> cursos = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            return cursos;
        }

        public void Remover(int id)
        {
            var json = client.DeleteAsync("http://localhost:5000/Curso/" + id)
                .Result
                .Content
                .ReadAsStringAsync()
                .Result;

            this.VerificarErro(json);

        }

        public List<Model.CursoModel> ConsultarPorSigla(string sigla)
        {
            string json = client.GetAsync("http://localhost:5000/Curso/ConsultarPorSigla/" + sigla)
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            this.VerificarErro(json);


            List<Model.CursoModel> cursos = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            return cursos;
        }

        private void VerificarErro(string respostaAPI)
        {
            if (respostaAPI.Contains("CodigoErro"))
            {
                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(respostaAPI);
                throw new ArgumentException(erro.Mensagem);
            }
        }
    }
}
