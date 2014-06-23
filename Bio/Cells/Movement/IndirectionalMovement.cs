//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Media;

//namespace Bio
//{
//    /// <summary>
//    /// Клетка движится одинаково хорошо в любом направлении
//    /// </summary>
//    class IndirectionalMovement : CellBehaviourBase<IndirectionalMovement>, IMovementBehaviour
//    {
//        // TODO: ввести зависимость скорости от уровня голода

//        private const double DefaultMaxVelocity = .033;
//        private const double DefaultSensorStrength = .02;
//        private const double DefaultHungerPerTickIdle = .005;
//        private const double DefaultHungerPerTickMove = .02;
//        private const double DeviationSetp = .2;

//        public double maxVelocity = DefaultMaxVelocity;
//        public double sensorStrength = DefaultSensorStrength;
//        public double hungerPerTickIdle = DefaultHungerPerTickIdle;
//        public double hungerPerTickMove = DefaultHungerPerTickMove;

//        private double deviation;

//        private List<ISensor> sensors;

//        public IndirectionalMovement()
//        {
//            base.HungerPerTick = hungerPerTickIdle;
//            base.MutationChance = .01;
//        }

//        public override void Tick(int timeCompression)
//        {
//            var desiredDirection = new Vector();

//            foreach (var sensor in sensors)
//                desiredDirection = desiredDirection + sensor.DesiredVector;

//            if (desiredDirection.Length < sensorStrength)
//            {
//                HungerPerTick = hungerPerTickIdle;
//                return;
//            }

//            desiredDirection.Normalize();
//            Cell.Direction = desiredDirection;


//            Cell.Coordinate = Cell.Coordinate + Cell.Direction * maxVelocity;
//            HungerPerTick = hungerPerTickMove;
//        }

//        protected override void Mutate()
//        {
//            deviation = deviation * (1 + Math.Sign(Random.Next(2) - 1) * DeviationSetp * Random.Next(1));

//            maxVelocity = DefaultMaxVelocity * deviation;
//            sensorStrength = DefaultSensorStrength / Math.Sqrt(deviation);

//            hungerPerTickIdle = DefaultHungerPerTickIdle * deviation;
//            hungerPerTickMove = DefaultHungerPerTickMove * deviation * deviation;
//        }

//        public override IndirectionalMovement Clone()
//        {
//            return new IndirectionalMovement
//            {
//                maxVelocity = maxVelocity,
//                sensorStrength = sensorStrength,
//                hungerPerTickIdle = hungerPerTickIdle,
//                hungerPerTickMove = hungerPerTickMove,

//                deviation = deviation
//            };
//        }

//        public override void OnInit()
//        {
//            sensors = new List<ISensor>(Cell.GetBehaviours<ISensor>());

//            if (sensors.Count == 0)
//                Cell.ForceKill("DirectionalMovement: у клетки нет сенсоров");

//        }
//    }
//}

