using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmProfessorCadastrar : NsfUserScreen
	{
		public frmProfessorCadastrar()
		{
			InitializeComponent();
		}

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Nsf.App.Model.ProfessorModel prof = new Model.ProfessorModel();
            if (Convert.ToInt32(lblId.Text) != 0)
            {
                lblId.Visible = true;
                prof.IdProfessor = Convert.ToInt32(lblId.Text);
                prof.NmProfessor = txtNome.Text;
                prof.NmPai = txtPai.Text;
                prof.NmMae = txtNome.Text;
                prof.BtAtivo = chkAtivo.Checked;
                prof.DsCelular = txtCelular.Text;
                prof.DsCpf = txtCpf.Text;
                prof.DsCurso = txtCurso.Text;
                prof.DsCvLattes = txtCvLattes.Text;
                prof.DsEmail = txtEmail.Text;
                prof.DsEstado = txtEstadoNasc.Text;
                prof.DsFaculdade = txtFaculdade.Text;
                prof.DsRg = txtRG.Text;
                prof.DsRgEmissor = txtRGEmissao.Text;
                prof.DsRgOrgao = txtRGOrgao.Text;
                prof.DsTelefone = txtTelefone.Text;
                prof.DtFaculdadeFim = dtpFaculdadeFim.Value;
                prof.DtFaculdadeInicio = dtpFaculdadeInicio.Value;
                prof.DtNascimento = dtpNascimento.Value;
                prof.IdLogin = Convert.ToInt32(txtLogin.Text);
                prof.NrAnoPrimeiroEmprego = Convert.ToInt32(nudPrimeiroEmprego.Value);
                prof.TpContratacao = cboContrato.Text;

                Nsf.App.API.Client.ProfessorApi api = new API.Client.ProfessorApi();
                api.Alterar(prof);

                MessageBox.Show("Alterado com sucesso", "NSF");

            }
            else
            {
                prof.NmProfessor = txtNome.Text;
                prof.NmPai = txtPai.Text;
                prof.NmMae = txtNome.Text;
                prof.BtAtivo = chkAtivo.Checked;
                prof.DsCelular = txtCelular.Text;
                prof.DsCpf = txtCpf.Text;
                prof.DsCurso = txtCurso.Text;
                prof.DsCvLattes = txtCvLattes.Text;
                prof.DsEmail = txtEmail.Text;
                prof.DsEstado = txtEstadoNasc.Text;
                prof.DsFaculdade = txtFaculdade.Text;
                prof.DsRg = txtRG.Text;
                prof.DsRgEmissor = txtRGEmissao.Text;
                prof.DsRgOrgao = txtRGOrgao.Text;
                prof.DsTelefone = txtTelefone.Text;
                prof.DtFaculdadeFim = dtpFaculdadeFim.Value;
                prof.DtFaculdadeInicio = dtpFaculdadeInicio.Value;
                prof.DtNascimento = dtpNascimento.Value;
                prof.IdLogin = Convert.ToInt32(txtLogin.Text);
                prof.NrAnoPrimeiroEmprego = Convert.ToInt32(nudPrimeiroEmprego.Value);
                prof.TpContratacao = cboContrato.Text;

                Nsf.App.API.Client.ProfessorApi Api = new API.Client.ProfessorApi();
                Api.Inserir(prof);

                MessageBox.Show("Inserido com sucesso", "NSF");

            }

        }
        public void CarregaarCampos (Model.ProfessorModel prof)
        {
            lblId.Text = prof.IdProfessor.ToString();
            txtNome.Text =  prof.NmProfessor;
            txtPai.Text = prof.NmPai;
            txtNome.Text = prof.NmMae;
            chkAtivo.Checked = prof.BtAtivo;
            txtCelular.Text = prof.DsCelular;
            txtCpf.Text = prof.DsCpf;
            txtCurso.Text = prof.DsCurso;
            txtCvLattes.Text = prof.DsCvLattes;
            txtEmail.Text = prof.DsEmail;
            txtEstadoNasc.Text = prof.DsEstado;
            txtFaculdade.Text = prof.DsFaculdade;
            txtRG.Text = prof.DsRg;
            txtRGEmissao.Text = prof.DsRgEmissor;
            txtRGOrgao.Text = prof.DsRgOrgao;
            txtTelefone.Text = prof.DsTelefone;
            dtpFaculdadeFim.Value = prof.DtFaculdadeFim;
            dtpFaculdadeInicio.Value = prof.DtFaculdadeInicio;
            dtpNascimento.Value = prof.DtNascimento;
            txtLogin.Text = prof.IdLogin.ToString();
            nudPrimeiroEmprego.Value = prof.NrAnoPrimeiroEmprego;
            cboContrato.Text = prof.TpContratacao;
            
        }
    }
}