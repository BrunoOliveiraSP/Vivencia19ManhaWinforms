using System;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmSalaConsultar : NsfUserScreen
	{
		public frmSalaConsultar()
		{
			InitializeComponent();
		}

        //API.Client.SalaAPI api = new API.Client.SalaAPI();
        private void txtInstituicao_TextChanged(object sender, EventArgs e)
        {
            
            //try
            //{
            //    dgvSalas.AutoGenerateColumns = false;

            //    if (txtInstituicao.Text == string.Empty)
            //        dgvSalas.DataSource = api.ListarTodos();
            //    else
            //        dgvSalas.DataSource = api.ListarPorInstituto(txtInstituicao.Text);



            //}
            //catch(ArgumentException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dgvSalas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 5)
            //    {
            //        Nsf.App.Model.SalaModel modelo = dgvSalas.CurrentRow.DataBoundItem as Nsf.App.Model.SalaModel;

            //        if (MessageBox.Show("Deseja deletar a sala?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            api.Deletar(modelo.idSala);
            //            MessageBox.Show("Sala deletada com sucesso");
            //        }
            //    }
           
            //}
            //catch(ArgumentException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}