using System;
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
    }
}