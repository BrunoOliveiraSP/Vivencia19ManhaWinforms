﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nsf.App.API.Client
{
    public class CursoApi
    {
        HttpClient client = new HttpClient();

        public Model.CursoModel InserirCurso(Nsf.App.Model.CursoModel curso)
        {
            
            string json = JsonConvert.SerializeObject(curso);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = client.PostAsync("http://localhost:5000/Curso/", body).Result;

            string jsonResposta = this.VerificarErro(resp);
            curso = JsonConvert.DeserializeObject<Model.CursoModel>(jsonResposta);

            return curso;

        }

        public void AlterarCurso(Model.CursoModel curso)
        {
            
            string json = JsonConvert.SerializeObject(curso);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = client.PutAsync("http://localhost:5000/Curso/", body).Result;

            if (resposta.IsSuccessStatusCode == false)
            {
                string jsonResposta = resposta.Content
                                      .ReadAsStringAsync()
                                      .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
                throw new Exception(erro.Mensagem);
            }
        }
        public List<Model.CursoModel> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/Curso/ConsultarTodos")
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            
            List<Model.CursoModel> cursos = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            return cursos;
        }

        public List<Model.CursoModel> ConsultarPorNomeSigla(string nome, string sigla)
        {
            string json = client.GetAsync("http://localhost:5000/Curso?nome=" + nome + "&sigla=" + sigla)
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            List<Model.CursoModel> cursos = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            return cursos;

        }

        public void Remover(int id)
        {
            var json = client.DeleteAsync("http://localhost:5000/Curso/" + id).Result;

            if (json.IsSuccessStatusCode == false)
            {
                string jsonResposta = json.Content
                                      .ReadAsStringAsync()
                                      .Result;

                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(jsonResposta);
            }
        }

        public List<Model.CursoModel> ConsultarPorSigla(string sigla)
        {
            string json = client.GetAsync("http://localhost:5000/Curso/ConsultarPorSigla/" + sigla)
                                .Result
                                .Content
                                .ReadAsStringAsync()
                                .Result;

            
            List<Model.CursoModel> cursos = JsonConvert.DeserializeObject<List<Model.CursoModel>>(json);
            return cursos;
        }

        private string VerificarErro(HttpResponseMessage resp)
        {
            string json = resp
                          .Content
                          .ReadAsStringAsync()
                          .Result;
            if (resp.IsSuccessStatusCode == false)
            {
                
                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(json);
                throw new Exception(erro.Mensagem);
            }

            return json;
        }
    }
}
