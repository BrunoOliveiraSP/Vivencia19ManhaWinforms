using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nsf.App.UI
{
    public partial class frmAnoLetivoConsultar : NsfUserScreen
    {
        public frmAnoLetivoConsultar()
        {
            InitializeComponent();

            CarregarGrid();
		}

        private void CarregarGrid()
        {
            Nsf.App.API.Client.AnoLetivoAPI api = new API.Client.AnoLetivoAPI();

            List<Nsf.App.Model.AnoLetivoModel> lista = api.ConsultarTodos();

            dgvAnosLetivos.AutoGenerateColumns = false;
            dgvAnosLetivos.DataSource = lista;
        }

        private void dgvAnosLetivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                Model.AnoLetivoModel model = dgvAnosLetivos.CurrentRow.DataBoundItem as Model.AnoLetivoModel;

                Nsf.App.UI.frmAnoLetivoCadastrar tela = new frmAnoLetivoCadastrar();

                tela.CarregarTela(model);
                tela.CarregarGrid(model.IdAnoLetivo);

                frmInicial.Current.OpenScreen(tela);

                Hide();
            }

            if (e.ColumnIndex == 5)
            {
                try
                {
                    Model.AnoLetivoModel model = dgvAnosLetivos.CurrentRow.DataBoundItem as Model.AnoLetivoModel;

                    DialogResult r = MessageBox.Show("Deseja Remover?", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        API.Client.AnoLetivoAPI api = new API.Client.AnoLetivoAPI();

                        api.Remover(model.IdAnoLetivo);
                        MessageBox.Show("Removido com sucesso");

                        this.CarregarGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}