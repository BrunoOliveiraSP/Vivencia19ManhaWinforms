﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Nsf.App.UI
{
    public partial class frmExemploCadastrar : NsfUserScreen
    {
        public frmExemploCadastrar()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Model.ExemploModel exemplo = new Model.ExemploModel();
                exemplo.Id = Convert.ToInt32(nudId.Value);
                exemplo.Nome = txtNome.Text;

                API.Client.ExemploAPI api = new API.Client.ExemploAPI();
                api.Inserir(exemplo);

                MessageBox.Show("Exemplo inserido");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}