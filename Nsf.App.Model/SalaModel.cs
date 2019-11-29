using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsf.App.Model
{
    public class SalaModel
    {
        public int idSala { get; set; }
        public string nmLocal { get; set; }
        public string nmSala { get; set; }
        public int nrCapacidadeMaxima { get; set; }
        public int btAtivo { get; set; }
        public DateTime dtInclusao { get; set; }
        public DateTime dtAlteracao { get; set; }
        public int idFuncionarioAlteracao { get; set; }
    }
}
