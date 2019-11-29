using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Nsf.App.UI
{
    public partial class frmAnoLetivoCadastrar : NsfUserScreen
    {
        public frmAnoLetivoCadastrar()
        {
            InitializeComponent();
        }

        int id = 0;

        public void CarregarTela(Model.AnoLetivoModel model)
        {
            id = model.IdAnoLetivo;
            nudAno.Value = model.NrAno;
            dtpInicio.Value = model.DtInicio;
            dtpFim.Value = model.DtFim;
            cboStatus.Text = model.TpStatus;
        }

        Nsf.App.Model.AnoLetivoModel model = new Model.AnoLetivoModel();

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                model.NrAno = Convert.ToInt32(nudAno.Value);
                model.DtInicio = dtpInicio.Value;
                model.DtFim = dtpFim.Value;
                model.TpStatus = cboStatus.Text;

                if (rdnAberto.Checked == true)
                {
                    model.BtAtivo = true;
                }

                if (rdnFechado.Checked == true)
                {
                    model.BtAtivo = false;
                }

                Nsf.App.API.Client.AnoLetivoAPI api = new API.Client.AnoLetivoAPI();

                if (id > 0)
                    api.Alterar(model);

                else
                    api.Inserir(model);

                MessageBox.Show("Cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}