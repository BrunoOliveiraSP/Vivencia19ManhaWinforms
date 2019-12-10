﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.Model
{
    public class SalaVestibularResponse
    {
        public int idSalaVestibular { get; set; }
        public int idSala { get; set; }
        public string dsPeriodo { get; set; }
        public int nrOrdem { get; set; }
        public int qtInscritos { get; set; }
        public object idSalaNavigation { get; set; }

        public string nmLocal { get; set; }
        public string nmSala { get; set; }
    }
}
