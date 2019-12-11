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

            ((Control)this.tabTurmas).Enabled = false;
            ((Control)this.tabModulos).Enabled = false;
            ((Control)this.tabDisciplinas).Enabled = false;
            ((Control)this.tabAcessos).Enabled = false;
        }

        Model.TurmaModel turmaModel = new Model.TurmaModel();
        Model.AnoLetivoModel anoLetivoModel = new AnoLetivoModel();

        private void LimparCamposAnoLetivo()
        {
            nudAno.Value = DateTime.Now.Year;
            dtpInicio.Value = DateTime.Now;
            cboStatus.Text = string.Empty;
            dtpFim.Value = DateTime.Now;
        }

        private void LimparCamposTurma()
        {
            cboTurmaPeriodo.Text = string.Empty;
            txtTurmaNome.Text = string.Empty;
            nudTurmaCapacidade.Value = 1;
            cboTurmaCurso.Text = string.Empty;
        }

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

                    MessageBox.Show("Alterado com sucesso", "Alterar Ano Letivo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.LimparCamposAnoLetivo();
                }
                else
                {
                    Model.AnoLetivoModel anoLetivo = api.Inserir(model);

                    MessageBox.Show("Cadastrado com sucesso", "Abrir Ano Letivo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    anoLetivoModel = anoLetivo;

                    CarregarGrid(anoLetivoModel.IdAnoLetivo);

                    this.LimparCamposAnoLetivo();

                    ((Control)this.tabTurmas).Enabled = true;
                    ((Control)this.tabModulos).Enabled = true;
                    ((Control)this.tabDisciplinas).Enabled = true;
                    ((Control)this.tabAcessos).Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTurmaAdd_Click(object sender, EventArgs e)
        {
            try
            {
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

                    MessageBox.Show("Alterado com sucesso", "Alterar Turma", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvTurma.DataSource = null;
                    this.CarregarGrid(anoLetivoModel.IdAnoLetivo);

                    this.LimparCamposTurma();
                }
                else
                {
                    api.InserirTurma(model);

                    MessageBox.Show("Cadastrado com sucesso", "Cadastrar Turma", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvTurma.DataSource = null;
                    this.CarregarGrid(anoLetivoModel.IdAnoLetivo);

                    this.LimparCamposTurma();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmAnoLetivoCadastrar_Load(object sender, EventArgs e)
        {
            cboTurmaPeriodo.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            cboModuloTipo.SelectedIndex = 0;

            if(anoLetivoModel.IdAnoLetivo > 0)
            {
                btnSalvar.Text = "Editar Ano Letivo";

                ((Control)this.tabTurmas).Enabled = true;
                ((Control)this.tabModulos).Enabled = true;
                ((Control)this.tabDisciplinas).Enabled = true;
                ((Control)this.tabAcessos).Enabled = true;
            }
        }

        private void dgvTurma_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                Model.TurmaResponse turma = dgvTurma.CurrentRow.DataBoundItem as Model.TurmaResponse;

                cboTurmaPeriodo.Text = turma.TpPeriodo;
                txtTurmaNome.Text = turma.NmTurma;
                nudTurmaCapacidade.Value = turma.NrCapacidadeMax;
                cboTurmaCurso.Text = turma.NmCurso;

                turmaModel.IdTurma = turma.IdTurma;
                turmaModel.NmTurma = turma.NmTurma;
                turmaModel.NrCapacidadeMax = turma.NrCapacidadeMax;
                turmaModel.TpPeriodo = turma.TpPeriodo;

                btnTurmaAdd.Text = "Alterar";
            }

            if (e.ColumnIndex == 5)
            {
                try
                {
                    Model.TurmaModel turma = dgvTurma.CurrentRow.DataBoundItem as Model.TurmaModel;

                    DialogResult r = MessageBox.Show("Deseja Remover?", "Remover Turma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        API.Client.TurmaAPI api = new API.Client.TurmaAPI();

                        api.Remover(turma.IdTurma);

                        MessageBox.Show("Removido com sucesso", "Remover Turma", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvTurma.DataSource = null;
                        this.CarregarGrid(anoLetivoModel.IdAnoLetivo);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus.Text == "Encerrado")
            {
                rdnFechado.Checked = true;
                rdnAberto.Checked = false;
            }

            if (cboStatus.Text == "Em andamento")
            {
                rdnFechado.Checked = false;
                rdnAberto.Checked = true;
            }
        }
    }
}