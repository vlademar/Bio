using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Bio.Cells;

namespace Bio
{
    abstract class CellBehaviourBase
    {
        protected static Random Random = new Random();

        protected Deviation _deviation = new Deviation();

        public PetriDish PetriDish { get; set; }
        public Cell Cell { get; set; }

        public virtual double HungerPerTick { get; set; }
        public virtual double MutationChance { get; set; }
        public virtual double DeviationStep { get; set; }

        public abstract void Tick(int timeCompression);

        public void CellDivided()
        {
            if (Random.Next(1) < MutationChance * PetriDish.RadiationLevel)
                Mutate();

            OnDivide();
        }

        private void Mutate()
        {
            _deviation.Rool(DeviationStep);
            BehaviourPropertyAttribute.UpdateValues(this, _deviation);
            OnMutation(_deviation);
        }


        public CellBehaviourBase Clone()
        {
            var clone = CloneInternal();
            clone._deviation = _deviation;
            return clone;
        }

        protected abstract CellBehaviourBase CloneInternal();

        protected virtual void OnDivide() { }

        public virtual void CellDead() { }
        public virtual void OnInit() { }
        public virtual void OnMutation(Deviation deviation) { }

    }
}
