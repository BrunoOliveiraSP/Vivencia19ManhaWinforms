﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmProfessorCadastrar : NsfUserScreen
	{
        Model.ProfessorResponse modeloProf = new Model.ProfessorResponse();
        API.Client.ProfessorApi api = new API.Client.ProfessorApi();

        public frmProfessorCadastrar()
		{
			InitializeComponent();
            Carregarlbx();
		}

        public void CarregarCampos(Model.ProfessorResponse prof)
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
            txtLogin.Text = prof.Login.DsLogin;
            nudPrimeiroEmprego.Value = prof.NrAnoPrimeiroEmprego;
            cboContrato.Text = prof.TpContratacao;

            lbxDisciplinasDoProfessor.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
            lbxDisciplinasDoProfessor.DataSource = prof.DisciplinaProfessor;

            lbxDisciplinasDisponiveis.DisplayMember = nameof(Model.Model.DiciplinaModel.NmDisciplina);
            lbxDisciplinasDisponiveis.DataSource = prof.DisciplinaDisponiveis;

            modeloProf = prof;
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
            prof.NrAnoPrimeiroEmprego = Convert.ToInt32(nudPrimeiroEmprego.Value);
            prof.TpContratacao = cboContrato.Text;

            return prof;
        }

        private Model.LoginModel DadosLogin()
        {
            Model.LoginModel login = new Model.LoginModel();

            login.BtAtivo = true;
            login.BtTrocar = true;
            login.DsLogin = txtLogin.Text;
            login.DsSenha = "1234";
            login.DtInclusao = DateTime.Now;
            login.DtUltimoLogin = DateTime.Now;
            login.IdRole = 1;

            return login;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (modeloProf.IdProfessor != 0)
                {
                    Model.ProfessorRequest request = new Model.ProfessorRequest();

                    List<Model.Model.DiciplinaModel> disciplina = lbxDisciplinasDoProfessor.DataSource as List<Model.Model.DiciplinaModel>;
                    Model.ProfessorModel prof = DadosProfessor();
                    prof.IdProfessor = modeloProf.IdProfessor;

                    request.Disciplina = disciplina;
                    request.Professor = prof;
                    request.Login = modeloProf.Login;

                    request.Login.DsLogin = txtLogin.Text;
                    request.Login.BtAtivo = chkAtivo.Checked;

                    api.Alterar(request);

                    MessageBox.Show("Alterado com sucesso!", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Model.ProfessorRequest request = new Model.ProfessorRequest();

                    List<Model.Model.DiciplinaModel> disciplina = lbxDisciplinasDoProfessor.DataSource as List<Model.Model.DiciplinaModel>;
                    Model.ProfessorModel prof = DadosProfessor();
                    Model.LoginModel login = DadosLogin();

                    request.Disciplina = disciplina;
                    request.Professor = prof;
                    request.Login = login;

                    request = api.Inserir(request);

                    MessageBox.Show("Inserido com sucesso!", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    panelId.Visible = true;
                    lblId.Text = request.Professor.IdProfessor.ToString();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,"NSF",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro. Entre em contato com o administrador.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (modeloProf.Login.IdLogin != 0)
                {
                    api.ResetarSenha(modeloProf.Login);

                    MessageBox.Show("Senha resetada!", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "NSF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro. Entre em contato com o administrador.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}