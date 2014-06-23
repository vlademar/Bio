//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bio
//{
//    class Regeneration : CellBehaviourBase<Regeneration>, IHealthBehaviour
//    {
//        private const double DefaultMaxRegenHunger = .9;
//        private const double DefaultMinRegenHunger = .5;
//        private const double DefaultRegenRate = .5;
//        private const double DefaultHungerPerTickIdle = .002;
//        private const double DefaultHungerPerRegenCycle = .1;
//        private const double DeviationSetp = .1;

//        private double deviation = 1;

//        public double maxRegenHunger = DefaultMaxRegenHunger;
//        public double minRegenHunger = DefaultMinRegenHunger;
//        public double regenRate = DefaultRegenRate;
//        public double hungerPerTickIdle = DefaultHungerPerTickIdle;
//        public double hungerPerRegenCycle = DefaultHungerPerRegenCycle;

//        public Regeneration()
//        {
//            HungerPerTick = hungerPerTickIdle;
//            MutationChance = .02;
//        }

//        public override void Tick(int timeCompression)
//        {
//            if (Cell.Hunger < Cell.MaxHungerLevel * minRegenHunger)
//                return;

//            var efficency = (Cell.Hunger - minRegenHunger) / (maxRegenHunger - minRegenHunger);
//            efficency = Math.Min(1, efficency) * timeCompression;

//            Cell.Health += regenRate * efficency;
//            Cell.Hunger -= hungerPerRegenCycle * efficency;
//        }

//        protected override void Mutate()
//        {
//            deviation = deviation * (1 + Math.Sign(Random.Next(2) - 1) * DeviationSetp * Random.Next(1));

//            maxRegenHunger = DefaultMaxRegenHunger / deviation;
//            minRegenHunger = DefaultMinRegenHunger / deviation;
//            regenRate = DefaultRegenRate * deviation;
//            hungerPerTickIdle = DefaultHungerPerTickIdle * deviation;
//            hungerPerRegenCycle = DefaultHungerPerRegenCycle * Math.Pow(deviation, .5);
//        }

//        public override Regeneration Clone()
//        {
//            return new Regeneration
//            {
//                maxRegenHunger = maxRegenHunger,
//                minRegenHunger = minRegenHunger,
//                regenRate = regenRate,
//                hungerPerTickIdle = hungerPerTickIdle,
//                hungerPerRegenCycle = hungerPerRegenCycle,
//                deviation = deviation
//            };
//        }
//    }
//}
