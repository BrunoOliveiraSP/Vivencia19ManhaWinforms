using System;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmSalaCadastrar : NsfUserScreen
	{
        Model.SalaModel modeloSala = new Model.SalaModel();
        API.Client.SalaAPI api = new API.Client.SalaAPI();

        public frmSalaCadastrar()
		{
			InitializeComponent();

            modeloSala.idSala = Convert.ToInt32(lblId.Text);
            panelId.Visible = true;
		}

        private void cboInstituicao_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (modeloSala.idSala == 0)
                {
                    modeloSala.dtInclusao = DateTime.Now;
                    PreencherModelo();
                    modeloSala = api.Inserir(modeloSala);

                    lblId.Text = modeloSala.idSala.ToString();

                    MessageBox.Show("Sala inserida com sucesso.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PreencherModelo();
                    if(MessageBox.Show("Deseja realmente alterar as informações?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        api.Alterar(modeloSala);
                        MessageBox.Show("Sala alterada com sucesso.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Falha na operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CarregarSala(Model.SalaModel modelo)
        {
            cboInstituicao.Text = modelo.nmLocal;
            txtSala.Text = modelo.nmSala;
            nudCapacidade.Value = modelo.nrCapacidadeMaxima;
            chkAtivo.Checked = Convert.ToBoolean(modelo.btAtivo);
            lblId.Text = modelo.idSala.ToString();
            label5.Text = "  Alterar Sala ";

            modeloSala = modelo;
        }
        public void PreencherModelo()
        {
            modeloSala.nmLocal = cboInstituicao.Text;
            modeloSala.nmSala = txtSala.Text;
            modeloSala.nrCapacidadeMaxima = Convert.ToInt32(nudCapacidade.Value);
            modeloSala.btAtivo = Convert.ToInt32(chkAtivo.Checked);
            modeloSala.idFuncionarioAlteracao = 1;
            modeloSala.dtAlteracao = DateTime.Now;
        }
    }
}