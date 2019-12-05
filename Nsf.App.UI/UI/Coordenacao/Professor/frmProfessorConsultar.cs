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
                tela.CarregaarCampos(model);

                frmInicial.Current.OpenScreen(tela);

                Hide();
            }

            if (e.ColumnIndex == 5)
            {
                Model.ProfessorResponse model = dgvProfessores.CurrentRow.DataBoundItem as Model.ProfessorResponse;

                DialogResult r = MessageBox.Show("Deseja excluir", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    API.Client.v2.ProfessorAPI api = new API.Client.v2.ProfessorAPI();
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
            try
            {
                string nome = txtNome.Text;

                if (nome != string.Empty)
                {
                    API.Client.v2.ProfessorAPI api = new API.Client.v2.ProfessorAPI();
                    List<Model.ProfessorResponse> lista = api.ConsultarPorNome(nome);
                    dgvProfessores.AutoGenerateColumns = false;
                    dgvProfessores.DataSource = lista;
                }
                else
                    CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CarregarGrid()
        {
            API.Client.v2.ProfessorAPI db = new API.Client.v2.ProfessorAPI();
            List<Model.ProfessorResponse> lista = db.ListarTodos();
            dgvProfessores.AutoGenerateColumns = false;
            dgvProfessores.DataSource = lista;
        }

        private void dgvProfessores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
