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

        public void carregar(DiciplinaModel diciplina)
        {
            if (diciplina != null)
            {
                txtDisciplina.Text = diciplina.NmDisciplina;
                txtSigla.Text = diciplina.DsSigla;
                lblId.Text = diciplina.IdDisciplina.ToString();
                chkAtivo.Checked = Convert.ToBoolean(diciplina.BtAtivo);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                //
                int id = int.Parse(lblId.Text);
                DiciplinaModel model = new DiciplinaModel();

                if (id == 0)
                {
                    model.NmDisciplina = txtDisciplina.Text;
                    model.DsSigla = txtSigla.Text;
                    model.BtAtivo = Convert.ToUInt64(chkAtivo.Checked);
                    model.DtInclusao = DateTime.Now;

                    API.Client.DisciplinaAPI api = new API.Client.DisciplinaAPI();
                    api.Inserir(model);

                    MessageBox.Show("Diciplina inserida");
                }
                else
                {
                    model.IdDisciplina = int.Parse(lblId.Text);
                    model.NmDisciplina = txtDisciplina.Text;
                    model.DsSigla = txtSigla.Text;
                    model.BtAtivo = Convert.ToUInt64(chkAtivo.Checked);
                    model.DtUltimaAlteracao = DateTime.Now;

                    API.Client.DisciplinaAPI api = new API.Client.DisciplinaAPI();
                    api.Alterar(model);

                    MessageBox.Show("Diciplina alterada com sucesso!");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro. Entre em contato com o administrador.");
            }
        }
    }

}