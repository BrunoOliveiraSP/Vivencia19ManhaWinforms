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
            CarregarGrid();
        }

        private void txtCurso_TextChanged(object sender, EventArgs e)
        {
            string cursoNome = txtCurso.Text;

            API.Client.CursoApi api = new API.Client.CursoApi();
       
            List<Model.CursoModel> cursos = api.ConsultarPorNome(cursoNome);

            dgvCursos.AutoGenerateColumns = false;
            dgvCursos.DataSource = cursos;
        }

        public void CarregarGrid()
        {
            API.Client.CursoApi api = new API.Client.CursoApi();
            List<Model.CursoModel> cursos = api.ListarTodos();

            dgvCursos.AutoGenerateColumns = false;
            dgvCursos.DataSource = cursos;
        }

        private void dgvCursos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                Model.CursoModel curso = dgvCursos.CurrentRow.DataBoundItem as Model.CursoModel;

                DialogResult r = MessageBox.Show("Deseja remover?", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    API.Client.CursoApi api = new API.Client.CursoApi();
                    api.Remover(curso.IdCurso);
                }

                CarregarGrid();

                

            }
         }

        private void txtSigla_TextChanged(object sender, EventArgs e)
        {
            string sigla = txtSigla.Text;

            API.Client.CursoApi api = new API.Client.CursoApi();
            List<Model.CursoModel> consulta = api.ConsultarPorSigla(sigla);

            dgvCursos.DataSource = consulta;
        }
    }
}