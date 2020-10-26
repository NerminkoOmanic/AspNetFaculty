using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class TakmicenjeUcesnik
    {
        public int TakmicenjeUcesnikID { get; set; }

        [ForeignKey(nameof(OdjeljenjeStavkaID))]
        public int OdjeljenjeStavkaID {get; set; }
        public virtual OdjeljenjeStavka OdjeljenjeStavka { get; set; }

        [ForeignKey(nameof(TakmicenjeID))]
        public int TakmicenjeID {get; set; }
        public virtual Takmicenje Takmicenje { get; set; }
        public bool Pristupio {get; set; }
        public int Bodovi {get; set; }
    }
}
