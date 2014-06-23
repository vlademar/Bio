//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using Bio.Cells;

//namespace Bio
//{
//    /// <summary>
//    /// Клетка -- аутотроф, питается неорганическими веществами 
//    /// </summary>
//    sealed class Autotroph : CellBehaviourBase, IHungerBehaviour
//    {
//        [BehaviourProperty(DeviationGroup.A, DefaultValue = .1)]
//        public double HungerRegenPerTick { get; set; }

//        [BehaviourProperty(DeviationGroup.A, DefaultValue = .1)]
//        public double MaxMineralConsumption { get; set; }

//        [BehaviourProperty(DeviationGroup.A, DefaultValue = 50000, DeviationPow = 3)]
//        public double MinMineralLevelForMaxEfficency { get; set; }

//        [BehaviourProperty(DeviationGroup.A, DefaultValue = .005, DeviationPow = 3)]
//        public override double HungerPerTick { get; set; }

//        public FoodKind FoodKind { get { return FoodKind.Mineral; } }

//        public Autotroph()
//        {
//            MutationChance = .01;
//            DeviationStep = .1;
//        }

//        public override void Tick(int timeCompression)
//        {
//            // TODO: позволить клетке съедать больше Cell.MaxHungerLevel
//            // TODO: при сытости больше Cell.MaxHungerLevel * X выделять вовне FoodKind.Proteins
//            // TODO: сделать график ( реген сытости | концентрация еды ) гладким

//            if (Cell.Hunger > Cell.MaxHungerLevel)
//                return;

//            double foodLevel = PetriDish.FoodLevel(Cell, FoodKind.Mineral);

//            double efficency = Math.Min(1, foodLevel / MinMineralLevelForMaxEfficency) * timeCompression;

//            Cell.Hunger += HungerPerTick * efficency;
//            PetriDish.ChangeFoodLevel(Cell, FoodKind.Mineral, -MaxMineralConsumption * efficency);
//        }

//        public override void CellDead()
//        {
//            PetriDish.ChangeFoodLevel(Cell, FoodKind.Proteins, Cell.Hunger * .9);
//            PetriDish.ChangeFoodLevel(Cell, FoodKind.Mineral, Cell.Hunger * .1);
//        }

//        protected override CellBehaviourBase CloneInternal()
//        {
//            return new Autotroph
//            {
//                HungerRegenPerTick = this.HungerRegenPerTick,
//                MaxMineralConsumption = this.MaxMineralConsumption,
//                MinMineralLevelForMaxEfficency = this.MinMineralLevelForMaxEfficency,
//            };
//        }
//    }
//}
