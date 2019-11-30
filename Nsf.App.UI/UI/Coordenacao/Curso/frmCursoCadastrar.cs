using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Nsf.App.UI
{
    public partial class frmCursoCadastrar : NsfUserScreen
    {
        public frmCursoCadastrar()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Nsf.App.Model.CursoModel curso = new Model.CursoModel();
            curso.NmCurso = txtCurso.Text;
            curso.DsSigla = txtSigla.Text;
            curso.DsCategoria = cboCategoria.Text;
            curso.NrCapacidadeMaxima = Convert.ToInt32(nudCapacidade.Value);
            if (chkAtivo.Checked == true)
                curso.BtAtivo = 1;
            if (chkAtivo.Checked == false)
                curso.BtAtivo = 0;

            Nsf.App.API.Client.CursoApi api = new API.Client.CursoApi();
            api.InserirCurso(curso);

            MessageBox.Show("Curso insertido com sucesso");
        }

        private void chkAtivo_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}