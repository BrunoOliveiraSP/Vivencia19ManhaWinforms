using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Nsf.App.UI
{
    public partial class frmSalaVestibularCadastrar : NsfUserScreen
    {
        API.Client.SalaAPI apiSala = new API.Client.SalaAPI();
        API.Client.SalaVestibularAPI apiSalaVestibular = new API.Client.SalaVestibularAPI();
        Model.SalaVestibularModel modeloSalaVestibular = new Model.SalaVestibularModel();
        Model.SalaVestibularResponse response = new Model.SalaVestibularResponse();
        Model.SalaModel modeloSala = new Model.SalaModel();
        

        public frmSalaVestibularCadastrar()
        {
            InitializeComponent();

            try
            {
                response.idSala = 0;
                cboVestibularSala.Enabled = false;
                LimparCombos();

            }
            catch
            {

            }
        }
       

        private void btnVestibularAdd_Click(object sender, EventArgs e)
        {
           try
            {
                if (cboPeriodos.Text == "Selecione")
                    throw new ArgumentException("Selecione um Período");

                if (response.idSalaVestibular == 0)
                {
                    CarregarModelo();
                    modeloSalaVestibular.qtInscritos = 1;
                    modeloSalaVestibular.nrOrdem = 999;
                    apiSalaVestibular.Inserir(modeloSalaVestibular);
                    MessageBox.Show("Sala inserida com sucesso.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSalasVestibular.AutoGenerateColumns = false;
                    List<Model.SalaVestibularResponse> salas = apiSalaVestibular.ListarTodos();
                    dgvSalasVestibular.DataSource = salas;
                    LimparCombos();
                }
                else
                {
                    CarregarModelo();
                    if (MessageBox.Show("Deseja realmente alterar os dados da sala '" + cboVestibularSala.Text + "'?,", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modeloSalaVestibular.nrOrdem = 999;
                        modeloSalaVestibular.qtInscritos = 1;


                        apiSalaVestibular.Alterar(modeloSalaVestibular);
                        response.idSalaVestibular = 0;
                        MessageBox.Show("Sala alterada com sucesso.", "NSF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                        dgvSalasVestibular.AutoGenerateColumns = false;
                        List<Model.SalaVestibularResponse> salas = apiSalaVestibular.ListarTodos();
                        dgvSalasVestibular.DataSource = salas;

                        label5.Text = "  Cadastrar Salas Vestibular  ";
                        btnVestibularAdd.Text = "Adicionar";
                        LimparCombos();

                    

                }
                label5.Text = "  Cadastrar Salas Vestibular  ";
                btnVestibularAdd.Text = "Adicionar";
                LimparCombos();
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarCombo()
        {
            try
            {
                List<Nsf.App.Model.SalaModel> salas = apiSala.ListarPorLocal(cboVestibularInstituicao.Text);
                salas.Insert(0, new Model.SalaModel()
                {
                    nmSala = "Selecione"
                });

                cboVestibularSala.DisplayMember = nameof(Nsf.App.Model.SalaModel.nmSala);
                cboVestibularSala.DataSource = salas;
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        public void CarregarModelo()
        {
            modeloSala = apiSala.BuscarPorSala(cboVestibularSala.Text);
            modeloSalaVestibular.idSala = modeloSala.idSala;
            modeloSalaVestibular.dsPeriodo = cboPeriodos.Text;
            modeloSalaVestibular.idSalaVestibular = response.idSalaVestibular;

        }

        private void cboVestibularInstituicao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVestibularInstituicao.Text != "Selecione" || cboVestibularInstituicao.Text != string.Empty)
            {
                cboVestibularSala.Enabled = true;
                CarregarCombo();
            }
            
        }

        private void dgvSalasVestibular_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    response = dgvSalasVestibular.CurrentRow.DataBoundItem as Nsf.App.Model.SalaVestibularResponse;
                    cboPeriodos.Text = response.dsPeriodo;
                    cboVestibularInstituicao.Text = response.nmLocal;
                    cboVestibularSala.Text = response.nmSala;
                    label5.Text = "  Alterar Salas Vestibular  ";
                    btnVestibularAdd.Text = "Alterar";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao carregar os dados da sala, contate o administrador do sistema");
            }
        }

        private void frmSalaVestibularCadastrar_Load(object sender, EventArgs e)
        {
            try
            {
                dgvSalasVestibular.AutoGenerateColumns = false;
                List<Model.SalaVestibularResponse> salas = apiSalaVestibular.ListarTodos();
                dgvSalasVestibular.DataSource = salas;
            }
            catch(Exception)
            {

            }
        }
        private void LimparCombos()
        {
            cboVestibularInstituicao.Text = "Selecione";
            cboPeriodos.Text = "Selecione";
            cboVestibularSala.Text = "Selecione";
        }
    }
}