using System;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmSalaConsultar : NsfUserScreen
	{
		public frmSalaConsultar()
		{
			InitializeComponent();

            try
            {
                dgvSalas.AutoGenerateColumns = false;
                dgvSalas.DataSource = api.ListarTodos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        API.Client.SalaAPI api = new API.Client.SalaAPI();
        private void txtInstituicao_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                string instituicao = txtInstituicao.Text.Trim();
                txtInstituicao.Text = instituicao;
                

                dgvSalas.AutoGenerateColumns = false;

                if (instituicao == string.Empty)
                    dgvSalas.DataSource = api.ListarTodos();
                else
                    dgvSalas.DataSource = api.ListarPorLocal(instituicao);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvSalas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    Nsf.App.Model.SalaModel modelo = dgvSalas.CurrentRow.DataBoundItem as Nsf.App.Model.SalaModel;

                    if (MessageBox.Show("Deseja deletar a sala?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        api.Deletar(modelo.idSala);
                        MessageBox.Show("Sala deletada com sucesso.","NSF",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        string instituicao = txtInstituicao.Text.Trim();
                        txtInstituicao.Text = instituicao;


                        dgvSalas.AutoGenerateColumns = false;

                        if (instituicao == string.Empty)
                            dgvSalas.DataSource = api.ListarTodos();
                        else
                            dgvSalas.DataSource = api.ListarPorLocal(instituicao);
                    }
                }
                else if(e.ColumnIndex == 4)
                {
                    Nsf.App.Model.SalaModel modelo = dgvSalas.CurrentRow.DataBoundItem as Nsf.App.Model.SalaModel;
                    UI.frmSalaCadastrar sala = new frmSalaCadastrar();

                    sala.CarregarSala(modelo);
                    frmInicial.Current.OpenScreen(sala);
                }
           
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}