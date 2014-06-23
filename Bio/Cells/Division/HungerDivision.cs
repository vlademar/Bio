using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.Cells;
using Bio.Cells.Division;

namespace Bio
{
    class HungerDivision : CellBehaviourBase, IDivision
    {
        [BehaviourProperty("Prop")]
        public double Prop { get; set; }

        public override void Tick(int timeCompression)
        {
            throw new NotImplementedException();
        }

        protected override CellBehaviourBase CloneInternal()
        {
            throw new NotImplementedException();
        }
    }
}
