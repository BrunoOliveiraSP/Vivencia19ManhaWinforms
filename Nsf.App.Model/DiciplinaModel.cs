using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.Model.Model
{
     public class DiciplinaModel
    {
        public int IdDisciplina { get; set; }

        public string NmDisciplina { get; set; }

        public string DsSigla { get; set; }

        public ulong BtAtivo { get; set; }

        public DateTime DtUltimaAlteracao { get; set; }

        public DateTime DtInclusao { get; set; }

        public int IdFuncionarioAlteracao { get; set; }
    }
}
