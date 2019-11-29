using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nsf.App.UI
{
    public partial class frmCursoConsultar : NsfUserScreen
    {
        public frmCursoConsultar()
        {
            InitializeComponent();
        }

        private void txtCurso_TextChanged(object sender, EventArgs e)
        {


        }

        public void CarregarGrid()
        {
            API.Client.CursoApi api = new API.Client.CursoApi();
            List<Model.CursoModel> cursos = api.ListarTodos();

            dgvCursos.AutoGenerateColumns = false;
            dgvCursos.DataSource = cursos;
        }
    }
}