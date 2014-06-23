//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace Bio
//{
//    class SeekFood : CellBehaviourBase<SeekFood>, ISensor
//    {
//        private List<FoodKind> foodKinds;

//        private const double DefaultMaxSensedValue = 200000;
//        private const double DefaultHungerPerTick = .003;
//        private const double DeviationSetp = .05;

//        private double deviation = 1;

//        public double maxSensedValue = DefaultMaxSensedValue;

//        public Vector DesiredVector { get; private set; }

//        public SeekFood()
//        {
//            HungerPerTick = DefaultHungerPerTick;
//            MutationChance = .1;
//        }

//        public override void Tick(int timeCompression)
//        {
//            var vector = new Vector();

//            vector = foodKinds.Aggregate(
//                vector, (current, foodKind)
//                    => current + (PetriDish.FoodLevelGradient(Cell, foodKind) / maxSensedValue));

//            if (vector.Length > 1)
//                vector.Normalize();

//            DesiredVector = vector;
//        }

//        protected override void Mutate()
//        {
//            deviation = deviation * (1 + Math.Sign(Random.Next(2) - 1) * DeviationSetp * Random.Next(1));

//            maxSensedValue = DefaultMaxSensedValue / deviation;
//            HungerPerTick = DefaultHungerPerTick * Math.Pow(deviation, 2);
//        }

//        public override SeekFood Clone()
//        {
//            return new SeekFood
//            {
//                HungerPerTick = HungerPerTick,
//                maxSensedValue = maxSensedValue,
//                deviation = deviation
//            };
//        }

//        public override void OnInit()
//        {
//            foodKinds = new List<FoodKind>();

//            var foodList = Cell.GetBehaviours<IHungerBehaviour>().Select(b => b.FoodKind);
//            foreach (var foodKind in foodList)
//            {
//                if (!foodKinds.Contains(foodKind))
//                    foodKinds.Add(foodKind);
//            }
//        }
//    }
//}
