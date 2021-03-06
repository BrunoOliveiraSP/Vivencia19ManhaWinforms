﻿using System;
using System.Windows.Forms;
using Nsf.App.Model.Model;

namespace Nsf.App.UI
{
    public partial class frmDisciplinasCadastrar : NsfUserScreen
    {
        DiciplinaModel DiciplinaModel;
        public frmDisciplinasCadastrar(DiciplinaModel diciplina)
        {
            InitializeComponent();
            carregar(diciplina);
            DiciplinaModel = new DiciplinaModel();
        }

        public void carregar(DiciplinaModel disciplina)
        {
            if (disciplina != null)
            {
                txtDisciplina.Text = disciplina.NmDisciplina;
                txtSigla.Text = disciplina.DsSigla;
                lblId.Text = disciplina.IdDisciplina.ToString();
                chkAtivo.Checked = Convert.ToBoolean(disciplina.BtAtivo);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                DiciplinaModel model = new DiciplinaModel();

                if (DiciplinaModel.IdDisciplina == 0)
                {
                    model.NmDisciplina = txtDisciplina.Text;
                    model.DsSigla = txtSigla.Text;
                    model.BtAtivo = Convert.ToUInt64(chkAtivo.Checked);
                    model.DtInclusao = DateTime.Now;

                    API.Client.DisciplinaAPI api = new API.Client.DisciplinaAPI();
                    DiciplinaModel = api.Inserir(model);
                    lblId.Text = DiciplinaModel.IdDisciplina.ToString();

                    MessageBox.Show("Diciplina inserida com sucesso!", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    model.IdDisciplina = DiciplinaModel.IdDisciplina;
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
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro. Entre em contato com o administrador.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

}