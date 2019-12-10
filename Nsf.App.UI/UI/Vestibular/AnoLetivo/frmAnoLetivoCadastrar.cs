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
            CarregarCruso();
        }

        Model.TurmaModel turmaModel = new Model.TurmaModel();
        Model.AnoLetivoModel anoLetivoModel = new AnoLetivoModel();

        private void CarregarCruso()
        {
            API.Client.CursoApi api = new API.Client.CursoApi();

            List<Model.CursoModel> lista = api.ListarTodos();

            cboTurmaCurso.DisplayMember = nameof(CursoModel.NmCurso);
            cboTurmaCurso.DataSource = lista;
        }

        public void CarregarTela(Model.AnoLetivoModel model)
        {
            dtpInicio.Value = model.DtInicio;
            cboStatus.Text = model.TpStatus;
            nudAno.Value = model.NrAno;
            dtpFim.Value = model.DtFim;
            anoLetivoModel = model;
        }

        Nsf.App.Model.AnoLetivoModel model = new Model.AnoLetivoModel();
        

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                model.NrAno = Convert.ToInt32(nudAno.Value);
                model.DtInicio = dtpInicio.Value;
                model.TpStatus = cboStatus.Text;
                model.DtFim = dtpFim.Value;

                if (rdnAberto.Checked == true)
                    model.BtAtivo = true;

                if (rdnFechado.Checked == true)
                    model.BtAtivo = false;

                Nsf.App.API.Client.AnoLetivoAPI api = new API.Client.AnoLetivoAPI();

                if (anoLetivoModel.IdAnoLetivo > 0)
                {
                    model.IdAnoLetivo = anoLetivoModel.IdAnoLetivo;
                    api.Alterar(model);

                    MessageBox.Show("Alterado com sucesso");
                }
                else
                {
                    Model.AnoLetivoModel anoLetivo = api.Inserir(model);

                    MessageBox.Show("Cadastrado com sucesso");

                    anoLetivoModel = anoLetivo;

                    CarregarGrid(anoLetivoModel.IdAnoLetivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTurmaAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(anoLetivoModel.IdAnoLetivo == 0)
                {
                    throw new ArgumentException("Impossível criar ou alterar uma turma sem antes selecionar um Ano Letivo");
                }

                Nsf.App.Model.TurmaModel model = new Model.TurmaModel();

                Model.CursoModel comboCurso = cboTurmaCurso.SelectedItem as Model.CursoModel;

                model.TpPeriodo = cboTurmaPeriodo.Text;
                model.NmTurma = txtTurmaNome.Text;
                model.NrCapacidadeMax = Convert.ToInt32(nudTurmaCapacidade.Value);
                model.IdAnoLetivo = anoLetivoModel.IdAnoLetivo;
                model.IdCurso = comboCurso.IdCurso;

                Nsf.App.API.Client.TurmaAPI api = new Nsf.App.API.Client.TurmaAPI();

                if (turmaModel.IdTurma > 0)
                {
                    model.IdTurma = turmaModel.IdTurma;
                    api.Alterar(model);

                    MessageBox.Show("Alterado com sucesso");

                    dgvTurma.DataSource = null;
                    this.CarregarGrid(anoLetivoModel.IdAnoLetivo);
                }
                else
                {
                    api.InserirTurma(model);

                    MessageBox.Show("Cadastrado com sucesso");

                    dgvTurma.DataSource = null;
                    this.CarregarGrid(anoLetivoModel.IdAnoLetivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CarregarGrid(int idAnoLetivo)
        {
            try
            {
                API.Client.TurmaAPI API = new App.API.Client.TurmaAPI();
                List<TurmaResponse> turma = API.ConsultarTurmaPorAnoLetivo(idAnoLetivo);

                dgvTurma.AutoGenerateColumns = false;
                dgvTurma.DataSource = turma;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvTurma_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                Model.TurmaModel turma = dgvTurma.CurrentRow.DataBoundItem as Model.TurmaModel;

                cboTurmaPeriodo.Text = turma.TpPeriodo;
                txtTurmaNome.Text = turma.NmTurma;
                nudTurmaCapacidade.Value = turma.NrCapacidadeMax;

                turmaModel = turma;
            }

            if(e.ColumnIndex == 5)
            {
                try
                {
                    Model.TurmaModel turma = dgvTurma.CurrentRow.DataBoundItem as Model.TurmaModel;

                    DialogResult r = MessageBox.Show("Deseja Remover?", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        API.Client.TurmaAPI api = new API.Client.TurmaAPI();

                        api.Remover(turma.IdTurma);

                        MessageBox.Show("Removido com sucesso");

                        dgvTurma.DataSource = null;
                        this.CarregarGrid(anoLetivoModel.IdAnoLetivo);
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