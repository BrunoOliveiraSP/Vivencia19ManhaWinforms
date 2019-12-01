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
            Carregarlbx();
		}

        public void CarregaarCampos(Model.ProfessorModel prof)
        {
            panelId.Visible = true;
            lblId.Text = prof.IdProfessor.ToString();
            txtNome.Text = prof.NmProfessor;
            txtPai.Text = prof.NmPai;
            txtMae.Text = prof.NmMae;
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Model.ProfessorModel prof = new Model.ProfessorModel();

                if (Convert.ToInt32(lblId.Text) != 0)
                {

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

                    API.Client.ProfessorApi api = new API.Client.ProfessorApi();
                    api.Alterar(prof);

                    MessageBox.Show("Alterado com sucesso", "NSF");
                }
                else
                {
                    prof.NmProfessor = txtNome.Text;
                    prof.NmPai = txtPai.Text;
                    prof.NmMae = txtMae.Text;
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

                    API.Client.ProfessorApi Api = new API.Client.ProfessorApi();
                    Api.Inserir(prof);

                    MessageBox.Show("Inserido com sucesso", "NSF");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Carregarlbx()
        {
            string a = string.Empty;

            API.Client.DisciplinaAPI db = new API.Client.DisciplinaAPI();
            List<Model.Model.DiciplinaModel> lista = db.ListarDisciplina(a);
            lbxDisciplinasDisponiveis.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
            foreach (Model.Model.DiciplinaModel item in lista)
            {
                lbxDisciplinasDisponiveis.Items.Add(item);
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(lbxDisciplinasDisponiveis.SelectedItem != null)
            {
                Model.Model.DiciplinaModel disciplina = lbxDisciplinasDisponiveis.SelectedItem as Model.Model.DiciplinaModel;

                lbxDisciplinasDoProfessor.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
                lbxDisciplinasDoProfessor.Items.Add(disciplina);

                lbxDisciplinasDisponiveis.Items.RemoveAt(lbxDisciplinasDisponiveis.SelectedIndex);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbxDisciplinasDoProfessor.SelectedItem != null)
            {
                Model.Model.DiciplinaModel disciplina = lbxDisciplinasDoProfessor.SelectedItem as Model.Model.DiciplinaModel;

                lbxDisciplinasDisponiveis.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
                lbxDisciplinasDisponiveis.Items.Add(disciplina);

                lbxDisciplinasDoProfessor.Items.RemoveAt(lbxDisciplinasDoProfessor.SelectedIndex);
            }
        }
    }
}