using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class SalaVestibularAPI
    {
        HttpClient client = new HttpClient();


        public void Inserir(Model.SalaVestibularModel modelo)
        {
            string json = JsonConvert.SerializeObject(modelo);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PostAsync("http://localhost:5000/SalaVestibular/", body).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(resp);
        }
        public List<Model.SalaVestibularResponse> ListarTodos()
        {
            string json = client.GetAsync("http://localhost:5000/SalaVestibular/").Result.Content.ReadAsStringAsync().Result;
            VerificarErro(json);

            List<Model.SalaVestibularResponse> salas = JsonConvert.DeserializeObject<List<Model.SalaVestibularResponse>>(json);
            return salas;
        }
        public Model.SalaVestibularModel BuscarPorID(int id)
        {
            string json = client.GetAsync("http://localhost:5000/SalaVestibular/" + id).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(json);

            Model.SalaVestibularModel sala = JsonConvert.DeserializeObject<Model.SalaVestibularModel>(json);
            return sala;
        }
        public void Alterar(Nsf.App.Model.SalaVestibularModel modelo)
        {
            string json = JsonConvert.SerializeObject(modelo);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = client.PutAsync("http://localhost:5000/SalaVestibular/", body).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(resp);
        }
        public void Deletar(int id)
        {
            var resp = client.DeleteAsync("http://localhost:5000/SalaVestibular/" + id).Result.Content.ReadAsStringAsync().Result;
            VerificarErro(resp);
        }
        private void VerificarErro(string resposta)
        {
            if (resposta.Contains("codigoErro"))
            {
                Model.ErroModel erro = JsonConvert.DeserializeObject<Model.ErroModel>(resposta);
                throw new ArgumentException(erro.Mensagem);
            }
        }
    }
}
