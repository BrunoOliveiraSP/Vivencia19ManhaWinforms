﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.API.Client
{
    public class TurmaResponse
    {
        public int IdTurma { get; set; }
        public int IdCurso { get; set; }
        public int IdAnoLetivo { get; set; }
        public string NmTurma { get; set; }
        public string TpPeriodo { get; set; }
        public int NrCapacidadeMax { get; set; }

        public int NrAno { get; set; }

        public string NmCurso { get; set; }
    }
}