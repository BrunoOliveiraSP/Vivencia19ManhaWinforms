using Nsf.App.Model;
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
            CarregarGrid();
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

        private void btnTurmaAdd_Click(object sender, EventArgs e)
        {
            Nsf.App.Model.TurmaModel turma = new Model.TurmaModel();
            turma.TpPeriodo = cboTurmaPeriodo.Text;
            turma.NmTurma = txtTurmaNome.Text;
            turma.NrCapacidadeMax = Convert.ToInt32(nudTurmaCapacidade.Value);

            Nsf.App.API.Client.TurmaAPI api = new Nsf.App.API.Client.TurmaAPI();
            api.InserirTurma(turma);

            MessageBox.Show("Turma cadastrada");
        }

        public void CarregarGrid()
        {
            API.Client.TurmaAPI API = new App.API.Client.TurmaAPI();
            List<TurmaModel> turma = API.ListarTodos();

            dgvTurma.AutoGenerateColumns = false;
            dgvTurma.DataSource = turma;
        }
    }
}