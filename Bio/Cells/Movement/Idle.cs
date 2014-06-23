//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bio
//{
//    /// <summary>
//    /// Клетка не умеет двигаться
//    /// </summary>
//    class Idle : CellBehaviourBase<Idle>, IMovementBehaviour
//    {
//        public Idle()
//        {
//            base.HungerPerTick = 0;
//            base.MutationChance = 0;
//        }

//        public override void Tick(int timeCompression) { }

//        protected override void Mutate() { }

//        public override Idle Clone()
//        {
//            return new Idle();
//        }
//    }
//}
