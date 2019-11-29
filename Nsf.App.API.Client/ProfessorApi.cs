﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class ProfessorApi
    {
        HttpClient client = new HttpClient();

        public void Inserir(Nsf.App.Model.ProfessorModel professor)
        {
            string json = JsonConvert.SerializeObject(professor);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/professor/", body).Result;
        }

        public void Alterar(Nsf.App.Model.ProfessorModel professor)
        {
            string json = JsonConvert.SerializeObject(professor);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/professor/", body).Result;
        }

        public void Deletar(int id)
        {
            var resp = client.DeleteAsync("http://localhost:5000/professor/" + id).Result;
        }

        public List<Nsf.App.Model.ProfessorModel> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/professor/").Result.Content.ReadAsStringAsync().Result;

            List<Nsf.App.Model.ProfessorModel> list = JsonConvert.DeserializeObject<List<Nsf.App.Model.ProfessorModel>>(json);

            return list;
        }

        public List<Nsf.App.Model.ProfessorModel> ConsultarPorNome(string nome)
        {
            string json = client.GetAsync("http://localhost:5000/professor/nome/" + nome).Result.Content.ReadAsStringAsync().Result;

            List<Nsf.App.Model.ProfessorModel> list = JsonConvert.DeserializeObject<List<Nsf.App.Model.ProfessorModel>>(json);

            return list;
        }


    }
}
