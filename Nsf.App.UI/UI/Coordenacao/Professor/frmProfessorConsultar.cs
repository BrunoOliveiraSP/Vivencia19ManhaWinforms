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
            }
        }

        private void frmProfessorConsultar_Load(object sender, EventArgs e)
        {
            API.Client.ProfessorApi db = new API.Client.ProfessorApi();
            List<Model.ProfessorModel> lista = db.ListarTodos();
            dgvProfessores.AutoGenerateColumns = false;
            dgvProfessores.DataSource = lista;
        }
    }
}