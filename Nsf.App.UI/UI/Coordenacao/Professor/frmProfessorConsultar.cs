using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmProfessorConsultar : NsfUserScreen
	{

		public frmProfessorConsultar()
		{
			InitializeComponent();
		}

        private void dgvProfessores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                Model.ProfessorModel model = dgvProfessores.CurrentRow.DataBoundItem as Model.ProfessorModel;
                frmProfessorCadastrar tela = new frmProfessorCadastrar();
                tela.Show();
                tela.CarregaarCampos(model);
            }

            if (e.ColumnIndex == 5)
            {
                Model.ProfessorModel model = dgvProfessores.CurrentRow.DataBoundItem as Model.ProfessorModel;

              DialogResult r = MessageBox.Show("Deseja excluir", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(r == DialogResult.Yes)
                {
                    API.Client.ProfessorApi api = new API.Client.ProfessorApi();
                    api.Deletar(model.IdProfessor);
              
                    MessageBox.Show("Removido com Sucesso");

                    CarregarGrid();
                }

            }



        }

        private void frmProfessorConsultar_Load(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            string nome = txtNome.Text;

            if (nome != string.Empty)
            {
                API.Client.ProfessorApi db = new API.Client.ProfessorApi();
                List<Model.ProfessorModel> lista = db.ConsultarPorNome(nome);
                dgvProfessores.AutoGenerateColumns = false;
                dgvProfessores.DataSource = lista;
            }
            else
                CarregarGrid();
        }

        public void CarregarGrid()
        {
            API.Client.ProfessorApi db = new API.Client.ProfessorApi();
            List<Model.ProfessorModel> lista = db.ListarTodos();
            dgvProfessores.AutoGenerateColumns = false;
            dgvProfessores.DataSource = lista;
        }
       
        }


    }
