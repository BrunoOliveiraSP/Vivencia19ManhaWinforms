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

        public void CarregaarCampos(Model.ProfessorResponse prof)
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

            lbxDisciplinasDoProfessor.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
            lbxDisciplinasDoProfessor.DataSource = prof.Disciplina;

            List<Model.Model.DiciplinaModel> disponiveis = lbxDisciplinasDisponiveis.DataSource as List<Model.Model.DiciplinaModel>;

            foreach (Model.Model.DiciplinaModel item in prof.Disciplina)
            {
                Model.Model.DiciplinaModel disciplina = disponiveis.FirstOrDefault(x => x.IdDisciplina == item.IdDisciplina);
                disponiveis.Remove(disciplina);
            }

            lbxDisciplinasDisponiveis.DataSource = null;
            lbxDisciplinasDisponiveis.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
            lbxDisciplinasDisponiveis.DataSource = disponiveis;
        }

        private Model.ProfessorModel DadosProfessor()
        {
            Model.ProfessorModel prof = new Model.ProfessorModel();

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

            return prof;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lblId.Text) != 0)
                {
                    Model.ProfessorRequest request = new Model.ProfessorRequest();

                    List<Model.Model.DiciplinaModel> disciplina = lbxDisciplinasDoProfessor.DataSource as List<Model.Model.DiciplinaModel>;
                    Model.ProfessorModel prof = DadosProfessor();
                    prof.IdProfessor = Convert.ToInt32(lblId.Text);

                    request.Disciplina = disciplina;
                    request.Professor = prof;

                    API.Client.v2.ProfessorAPI api = new API.Client.v2.ProfessorAPI();
                    api.Alterar(request);

                    MessageBox.Show("Alterado com sucesso", "NSF");
                }
                else
                {
                    Model.ProfessorRequest request = new Model.ProfessorRequest();

                    List<Model.Model.DiciplinaModel> disciplina = lbxDisciplinasDoProfessor.DataSource as List<Model.Model.DiciplinaModel>;
                    Model.ProfessorModel prof = DadosProfessor();

                    request.Disciplina = disciplina;
                    request.Professor = prof;

                    API.Client.v2.ProfessorAPI api = new API.Client.v2.ProfessorAPI();
                    request = api.Inserir(request);

                    MessageBox.Show("Inserido com sucesso", "NSF");

                    panelId.Visible = true;
                    lblId.Text = request.Professor.IdProfessor.ToString();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Carregarlbx()
        {
            if (lbxDisciplinasDisponiveis.DataSource == null && lbxDisciplinasDoProfessor.DataSource == null)
            {
                string a = string.Empty;

                API.Client.DisciplinaAPI db = new API.Client.DisciplinaAPI();
                List<Model.Model.DiciplinaModel> lista = db.ListarDisciplina(a);
                lbxDisciplinasDisponiveis.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
                lbxDisciplinasDisponiveis.DataSource = lista;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(lbxDisciplinasDisponiveis.SelectedItem != null)
            {
                List<Model.Model.DiciplinaModel> list = new List<Model.Model.DiciplinaModel>();
                

                if (lbxDisciplinasDoProfessor.DataSource != null)
                    list = lbxDisciplinasDoProfessor.DataSource as List<Model.Model.DiciplinaModel>;

                lbxDisciplinasDoProfessor.DataSource = null;
                Model.Model.DiciplinaModel disciplina = lbxDisciplinasDisponiveis.SelectedItem as Model.Model.DiciplinaModel;
                list.Add(disciplina);

                lbxDisciplinasDoProfessor.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
                lbxDisciplinasDoProfessor.DataSource = list;

                List<Model.Model.DiciplinaModel> disponiveis = lbxDisciplinasDisponiveis.DataSource as List<Model.Model.DiciplinaModel>;
                disponiveis.Remove(disciplina);

                lbxDisciplinasDisponiveis.DataSource = null;
                lbxDisciplinasDisponiveis.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
                lbxDisciplinasDisponiveis.DataSource = disponiveis;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbxDisciplinasDoProfessor.SelectedItem != null)
            {
                List<Model.Model.DiciplinaModel> list = new List<Model.Model.DiciplinaModel>();

                if (lbxDisciplinasDisponiveis.DataSource != null)
                    list = lbxDisciplinasDisponiveis.DataSource as List<Model.Model.DiciplinaModel>;

                lbxDisciplinasDisponiveis.DataSource = null;
                Model.Model.DiciplinaModel disciplina = lbxDisciplinasDoProfessor.SelectedItem as Model.Model.DiciplinaModel;
                list.Add(disciplina);

                lbxDisciplinasDisponiveis.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
                lbxDisciplinasDisponiveis.DataSource = list;

                List<Model.Model.DiciplinaModel> doprofessor = lbxDisciplinasDoProfessor.DataSource as List<Model.Model.DiciplinaModel>;
                doprofessor.Remove(disciplina);

                lbxDisciplinasDoProfessor.DataSource = null;
                lbxDisciplinasDoProfessor.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
                lbxDisciplinasDoProfessor.DataSource = doprofessor;
            }
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}