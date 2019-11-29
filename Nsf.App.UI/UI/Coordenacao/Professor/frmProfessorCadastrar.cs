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
}