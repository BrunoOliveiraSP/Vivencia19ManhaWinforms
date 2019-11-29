using System;
using System.Windows.Forms;
using Nsf.App.Model.Model;

namespace Nsf.App.UI
{
    public partial class frmDisciplinasCadastrar : NsfUserScreen
    {

        public frmDisciplinasCadastrar()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DiciplinaModel model = new DiciplinaModel();
                model.NmDisciplina = txtDisciplina.Text;
                model.DsSigla = txtSigla.Text;
                model.BtAtivo = Convert.ToUInt64(chkAtivo.Checked);
                //falta so colocar data de inclusao

                API.Client.DisciplinaAPI api = new API.Client.DisciplinaAPI();
                api.Inserir(model);

                MessageBox.Show("Diciplina inserida");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro,Tente Novamante");
            }
        }
    }

}