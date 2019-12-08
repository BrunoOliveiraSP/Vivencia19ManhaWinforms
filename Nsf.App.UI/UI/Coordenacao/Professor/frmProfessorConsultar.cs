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
            CarregarGrid();
		}

        private void dgvProfessores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                Model.ProfessorResponse model = dgvProfessores.CurrentRow.DataBoundItem as Model.ProfessorResponse;

                frmProfessorCadastrar tela = new frmProfessorCadastrar();
                tela.CarregarCampos(model);

                frmInicial.Current.OpenScreen(tela);

                Hide();
            }

            if (e.ColumnIndex == 5)
            {
                Model.ProfessorResponse model = dgvProfessores.CurrentRow.DataBoundItem as Model.ProfessorResponse;

                DialogResult r = MessageBox.Show("Deseja excluir", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
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
          //  CarregarGrid();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string nome = txtNome.Text;

                if (nome != string.Empty)
                {
                    API.Client.ProfessorApi api = new API.Client.ProfessorApi();

                    List<Model.ProfessorResponse> lista = api.ConsultarPorNome(nome);
                    dgvProfessores.AutoGenerateColumns = false;
                    dgvProfessores.DataSource = lista;
                }
                else
                    CarregarGrid();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro. Entre em contato com o administrador.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CarregarGrid()
        {
            API.Client.ProfessorApi db = new API.Client.ProfessorApi();
            List<Model.ProfessorResponse> lista = db.ListarTodos();
            dgvProfessores.AutoGenerateColumns = false;
            dgvProfessores.DataSource =  lista;
        }

        private void dgvProfessores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
