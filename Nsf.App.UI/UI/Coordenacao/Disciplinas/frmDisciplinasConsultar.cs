using Nsf.App.Model.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nsf.App.UI
{
	public partial class frmDisciplinasConsultar : NsfUserScreen
	  {

        API.Client.DisciplinaAPI API;

        public frmDisciplinasConsultar()
        {
        	InitializeComponent();
            API = new API.Client.DisciplinaAPI();
            dgvDisciplinas.AutoGenerateColumns = false;
            dgvDisciplinas.DataSource = API.ListarTudo();
        }

        private void txtSigla_TextChanged(object sender, EventArgs e)
        {
            if (txtSigla.Text.Length > 0)
            {
                string nmSigla = txtSigla.Text;
                string primeiraLetra = nmSigla.Substring(0, 1).ToUpper();
                string restoNm = nmSigla.Substring(1).ToLower();
                string nomeSigla = primeiraLetra + restoNm;
                dgvDisciplinas.DataSource = API.ListarSigla(nomeSigla);
            }
               
            else
                dgvDisciplinas.DataSource = API.ListarTudo();
        }

        private void dgvDisciplinas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    DiciplinaModel diciplina = dgvDisciplinas.CurrentRow.DataBoundItem as DiciplinaModel;
                    frmInicial.Current.OpenScreen(new frmDisciplinasCadastrar(diciplina));
                    Hide();
                }
                if (e.ColumnIndex == 4)
                {

                    DialogResult resposta = MessageBox.Show("Deseja realmente remover essa informação?", "NSF",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                    if (resposta == DialogResult.Yes)
                    {
                        DiciplinaModel diciplina = dgvDisciplinas.CurrentRow.DataBoundItem as DiciplinaModel;
                        API.Remover(diciplina.IdDisciplina);
                        dgvDisciplinas.DataSource = API.ListarTudo();
                        MessageBox.Show("Disciplina removida!", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }                                          

                    
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro. Entre em contato com o administrador.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtDisciplina_TextChanged_1(object sender, EventArgs e)
        {
            if (txtDisciplina.Text.Length != 0)
            {
                string nmdiciplina = txtDisciplina.Text;
                string primeiraLetra = nmdiciplina.Substring(0, 1).ToUpper();
                string restoNm = nmdiciplina.Substring(1).ToLower();
                string nomeDiciplina = primeiraLetra + restoNm;
                dgvDisciplinas.DataSource = API.ListarDisciplina(nomeDiciplina);
            }
           
            else
                dgvDisciplinas.DataSource = API.ListarTudo();
        }
    }
}