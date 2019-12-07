using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Nsf.App.UI
{
    public partial class frmCursoCadastrar : NsfUserScreen
    {
        public frmCursoCadastrar(Model.CursoModel curso)
        {
            InitializeComponent();
            CarregarCurso(curso);
        }

        public void CarregarCurso(Model.CursoModel curso)
        {
            if (curso != null)
            {

                lblId.Text = Convert.ToString(curso.IdCurso);
                txtCurso.Text = curso.NmCurso;
                txtSigla.Text = curso.DsSigla;
                if (curso.DsCategoria == "Livre")
                    cboCategoria.Text = "Livre";
                if (curso.DsCategoria == "Técnico")
                    cboCategoria.Text = "Técnico";
                if (curso.DsCategoria == "Qualificação")
                    cboCategoria.Text = "Qualificação";
                cboCategoria.Text = curso.DsCategoria;
                nudCapacidade.Value = Convert.ToDecimal(curso.NrCapacidadeMaxima);
                if (curso.BtAtivo == 0)
                    chkAtivo.Checked = false;
                if (curso.BtAtivo == 1)
                    chkAtivo.Checked = true;
            }
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(lblId.Text);
                if (id == 0)
                {

                    Nsf.App.Model.CursoModel curso = new Model.CursoModel();

                    string nmcurso = txtCurso.Text;
                    string primeiraLetra = nmcurso.Substring(0, 1).ToUpper();
                    string restoNm= nmcurso.Substring(1).ToLower();
                    string nomeCurso = primeiraLetra + restoNm;
                    curso.NmCurso = nomeCurso;
                    curso.DsSigla = txtSigla.Text.ToUpper();
                    curso.DsCategoria = cboCategoria.Text;
                    curso.NrCapacidadeMaxima = Convert.ToInt32(nudCapacidade.Value);
                    if (chkAtivo.Checked == true)
                        curso.BtAtivo = 1;
                    if (chkAtivo.Checked == false)
                        curso.BtAtivo = 0;

                    Nsf.App.API.Client.CursoApi api = new API.Client.CursoApi();
                    api.InserirCurso(curso);
                    MessageBox.Show("Curso inserido no sistema com sucesso", "NSF");

                    LimparCampos();

                }
                else
                {
                    Nsf.App.Model.CursoModel curso = new Model.CursoModel();
                    curso.IdCurso = Convert.ToInt32(lblId.Text);

                    string nmcurso = txtCurso.Text;
                    string primeiraLetra = nmcurso.Substring(0, 1).ToUpper();
                    string restoNm = nmcurso.Substring(1).ToLower();
                    string cursoNome = primeiraLetra + restoNm;
                    curso.NmCurso = cursoNome; ;
                    curso.DsSigla = txtSigla.Text.ToUpper();
                    curso.DsCategoria = cboCategoria.Text;
                    curso.NrCapacidadeMaxima = Convert.ToInt32(nudCapacidade.Value);
                    if (chkAtivo.Checked == true)
                        curso.BtAtivo = 1;
                    if (chkAtivo.Checked == false)
                        curso.BtAtivo = 0;

                    Nsf.App.API.Client.CursoApi api = new API.Client.CursoApi();
                    api.AlterarCurso(curso);
                    MessageBox.Show("Dados do curso alterados com sucesso", "NSF");

                    LimparCampos();


                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NSF", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

        }
        
            public void LimparCampos()
            {
                lblId.Text = "0";
                txtCurso.Text = string.Empty;
                txtSigla.Text = string.Empty;
                cboCategoria.Text = "Selecione";
                nudCapacidade.Value = 50;
                chkAtivo.Checked = true;
            }

        private void lbxDisciplinasDisponiveis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}