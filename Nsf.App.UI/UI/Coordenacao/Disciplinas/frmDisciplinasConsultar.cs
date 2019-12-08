using Nsf.App.Model.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmDisciplinasConsultar : NsfUserScreen
	  {

        API.Client.DisciplinaAPI API;

        public frmDisciplinasConsultar()
        {
        	InitializeComponent();
            API = new API.Client.DisciplinaAPI();
            dgvDisciplinas.AutoGenerateColumns = false;
            dgvDisciplinas.DataSource = API.ListarTudo();
        }

        private void txtSigla_TextChanged(object sender, EventArgs e)
        {
            if (txtSigla.Text.Length > 0)
                dgvDisciplinas.DataSource = API.ListarSigla(txtSigla.Text);
            else
                dgvDisciplinas.DataSource = API.ListarTudo();
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
                API.Remover(diciplina.IdDisciplina);
                dgvDisciplinas.DataSource = API.ListarTudo();
                MessageBox.Show("Disciplina removida!", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtDisciplina_TextChanged_1(object sender, EventArgs e)
        {
            if (txtDisciplina.Text.Length != 0)
                dgvDisciplinas.DataSource = API.ListarDisciplina(txtDisciplina.Text);
            else
                dgvDisciplinas.DataSource = API.ListarTudo();
        }
    }
}