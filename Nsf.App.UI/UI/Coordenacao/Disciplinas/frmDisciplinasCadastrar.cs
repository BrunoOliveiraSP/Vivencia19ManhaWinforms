using System;
using System.Windows.Forms;
using Nsf.App.Model.Model;

namespace Nsf.App.UI
{
    public partial class frmDisciplinasCadastrar : NsfUserScreen
    {
        public frmDisciplinasCadastrar(DiciplinaModel diciplina)
        {
            InitializeComponent();
            carregar(diciplina);
        }

        int id;

        public void carregar(DiciplinaModel disciplina)
        {
            if (disciplina != null)
            {
                txtDisciplina.Text = disciplina.NmDisciplina;
                txtSigla.Text = disciplina.DsSigla;
                lblId.Text = disciplina.IdDisciplina.ToString();
                chkAtivo.Checked = Convert.ToBoolean(disciplina.BtAtivo);
                id = disciplina.IdDisciplina;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                DiciplinaModel model = new DiciplinaModel();

                if (id == 0)
                {
                    model.NmDisciplina = txtDisciplina.Text;
                    model.DsSigla = txtSigla.Text;
                    model.BtAtivo = Convert.ToUInt64(chkAtivo.Checked);
                    model.DtInclusao = DateTime.Now;

                    API.Client.DisciplinaAPI api = new API.Client.DisciplinaAPI();
                    id = api.Inserir(model);
                    lblId.Text = id.ToString();

                    MessageBox.Show("Diciplina inserida com sucesso!", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    model.IdDisciplina = id;
                    model.NmDisciplina = txtDisciplina.Text;
                    model.DsSigla = txtSigla.Text;
                    model.BtAtivo = Convert.ToUInt64(chkAtivo.Checked);
                    model.DtUltimaAlteracao = DateTime.Now;

                    API.Client.DisciplinaAPI API = new API.Client.DisciplinaAPI();
                    API.Alterar(model);

                    MessageBox.Show("Diciplina alterada com sucesso!", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro. Entre em contato com o administrador.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

}