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
            try
            {
                string cursoNome = txtCurso.Text;

                API.Client.CursoApi api = new API.Client.CursoApi();

                List<Model.CursoModel> cursos = api.ConsultarPorNome(cursoNome);

                dgvCursos.AutoGenerateColumns = false;
                dgvCursos.DataSource = cursos;

                if (txtCurso.Text == string.Empty && txtSigla.Text == string.Empty)
                    CarregarGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NSF", MessageBoxButtons.OK);
            }
        }

        public void CarregarGrid()
        {
            try
            {
                API.Client.CursoApi api = new API.Client.CursoApi();
                List<Model.CursoModel> cursos = api.ListarTodos();

                dgvCursos.AutoGenerateColumns = false;
                dgvCursos.DataSource = cursos;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NSF", MessageBoxButtons.OK);                       
            }
        }

        private void dgvCursos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 5)
            {
                DialogResult r = MessageBox.Show("Deseja remover?", "NSF", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    try
                    {
                        Model.CursoModel curso = dgvCursos.CurrentRow.DataBoundItem as Model.CursoModel;

                        API.Client.CursoApi api = new API.Client.CursoApi();
                        api.Remover(curso.IdCurso);

                        MessageBox.Show("Curso removido do sistema com sucesso", "NSF");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "NSF", MessageBoxButtons.OK);
                    }

                }

                    
                CarregarGrid();
            }
            if (e.ColumnIndex == 4)
            {
                DialogResult r2 = MessageBox.Show("Deseja alterar os dados deste curso?", "NSF", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r2 == DialogResult.Yes)
                {
                    Model.CursoModel curso = dgvCursos.CurrentRow.DataBoundItem as Model.CursoModel;

                    frmInicial.Current.OpenScreen(new frmCursoCadastrar(curso));
                    Hide();
                }
            }
                        
         }

        private void txtSigla_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sigla = txtSigla.Text;

                API.Client.CursoApi api = new API.Client.CursoApi();
                List<Model.CursoModel> consulta = api.ConsultarPorSigla(sigla);

                dgvCursos.DataSource = consulta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NSF", MessageBoxButtons.OK);
            }
        }

    }
}