using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Nsf.App.UI
{
    public partial class frmSalaVestibularCadastrar : NsfUserScreen
    {
        public frmSalaVestibularCadastrar()
        {
            InitializeComponent();
            CarregarCombo();
        }

        private void btnVestibularAdd_Click(object sender, EventArgs e)
        {

        }

        private void CarregarCombo()
        {
            API.Client.SalaAPI api = new API.Client.SalaAPI();
            List<Nsf.App.Model.SalaModel> salas = api.ListarTodos();
            salas.Insert(0, new Model.SalaModel()
            {
                nmSala = "Selecione"
            });

            cboVestibularSala.DisplayMember = nameof(Nsf.App.Model.SalaModel.nmSala);
            cboVestibularSala.DataSource = salas;
        }
    }
}