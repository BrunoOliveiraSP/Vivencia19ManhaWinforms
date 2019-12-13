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
            if (txtCurso.Text != string.Empty && txtSigla.Text != string.Empty)
            {
                string nome = txtCurso.Text;
                string sigla = txtSigla.Text;
                API.Client.CursoApi api = new API.Client.CursoApi();
                List<Model.CursoModel> cursos = api.ConsultarPorNomeSigla(nome, sigla);

                dgvCursos.DataSource = cursos;
            }
            else
            {
                CarregarGrid();
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
            try
            {
                if (e.ColumnIndex == 5)
                {
                    DialogResult r = MessageBox.Show("Deseja remover?", "NSF", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {

                        Model.CursoModel curso = dgvCursos.CurrentRow.DataBoundItem as Model.CursoModel;

                        API.Client.CursoApi api = new API.Client.CursoApi();
                        
                        api.Remover(curso.IdCurso);
                        MessageBox.Show("Curso removido do sistema com sucesso!", "NSF");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NSF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

        private void txtSigla_TextChanged(object sender, EventArgs e)
        {
            if (txtSigla.Text != string.Empty && txtCurso.Text != string.Empty)
            {
                string nome = txtCurso.Text;
                string sigla = txtSigla.Text;

                API.Client.CursoApi api = new API.Client.CursoApi();
                List<Model.CursoModel> consulta = api.ConsultarPorNomeSigla(nome, sigla);

                dgvCursos.DataSource = consulta;
            }
            else
            {
                CarregarGrid();
            }
        }

    }
}