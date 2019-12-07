using Nsf.App.Model.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmDisciplinasConsultar : NsfUserScreen
	  {

        API.Client.DisciplinaAPI api;

        public frmDisciplinasConsultar()
        {
        	InitializeComponent();
            api = new API.Client.DisciplinaAPI();
            dgvDisciplinas.AutoGenerateColumns = false;
            dgvDisciplinas.DataSource = api.ListarTudo();
        }

        private void txtSigla_TextChanged(object sender, EventArgs e)
        {
            dgvDisciplinas.AutoGenerateColumns = false;

            if (txtSigla.Text.Length >= 1)
            {
                dgvDisciplinas.DataSource = api.ListarSigla(txtSigla.Text);
            }
        }

        private void dgvDisciplinas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DiciplinaModel diciplina = dgvDisciplinas.CurrentRow.DataBoundItem as DiciplinaModel;
                frmInicial.Current.OpenScreen(new frmDisciplinasCadastrar(diciplina));
                Hide();
            }
            if (e.ColumnIndex == 4)
            {
                DiciplinaModel diciplina = dgvDisciplinas.CurrentRow.DataBoundItem as DiciplinaModel;
                api.Remover(diciplina.IdDisciplina);
                dgvDisciplinas.RefreshEdit();



            }
        }

        private void txtDisciplina_TextChanged_1(object sender, EventArgs e)
        {
            dgvDisciplinas.AutoGenerateColumns = false;

            if (txtDisciplina.Text.Length >= 1)
            {
                dgvDisciplinas.DataSource = api.ListarDisciplina(txtDisciplina.Text);
            }
        }
    }
}