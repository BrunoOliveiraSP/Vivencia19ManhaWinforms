﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.Model
{
    public partial class CursoModel
    {
        public int IdCurso { get; set; }

        public string NmCurso { get; set; }

        public string DsSigla { get; set; }

        public int NrCapacidadeMaxima { get; set; }

        public ulong BtAtivo { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime DtUltimaAlteracao { get; set; }

        public int IdFuncionarioAlteracao { get; set; }

        public string DsCategoria { get; set; }

    }
}
