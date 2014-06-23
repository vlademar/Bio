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
//    /// Клетка умеет двигаться в одном направлении и поворачивать
//    /// </summary>
//    class DirectionalMovement : CellBehaviourBase<DirectionalMovement>, IMovementBehaviour
//    {
//        // TODO: ввести зависимость скорости от уровня голода

//        private const double DefaultMaxVelocity = .1;
//        private const double DefaultRotationSpeed = .01;
//        private const double DefaultSensorStrength = .05;
//        private const double DefaultHungerPerTickIdle = .002;
//        private const double DefaultHungerPerTickMove = .05;
//        private const double DeviationSetp = .2;

//        public double maxVelocity = DefaultMaxVelocity;
//        public double rotationSpeed = DefaultRotationSpeed;
//        public double sensorStrength = DefaultSensorStrength;
//        public double hungerPerTickIdle = DefaultHungerPerTickIdle;
//        public double hungerPerTickMove = DefaultHungerPerTickMove;

//        private double deviation;

//        private List<ISensor> sensors;

//        public DirectionalMovement()
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

//            var angleD = Vector.AngleBetween(Cell.Direction, desiredDirection);

//            var turnMatrix = Matrix.Identity;
//            turnMatrix.Rotate(Math.Sign(angleD) * rotationSpeed * timeCompression);

//            Cell.Direction = Cell.Direction * turnMatrix;
//            Cell.Direction.Normalize();

//            Cell.Coordinate = Cell.Coordinate + Cell.Direction * maxVelocity;
//            HungerPerTick = hungerPerTickMove;
//        }

//        protected override void Mutate()
//        {
//            deviation = deviation * (1 + Math.Sign(Random.Next(2) - 1) * DeviationSetp * Random.Next(1));

//            maxVelocity = DefaultMaxVelocity * deviation;
//            rotationSpeed = DefaultRotationSpeed / deviation;
//            sensorStrength = DefaultSensorStrength / Math.Sqrt(deviation);

//            hungerPerTickIdle = DefaultHungerPerTickIdle * deviation;
//            hungerPerTickMove = DefaultHungerPerTickMove * deviation * deviation;
//        }

//        public override DirectionalMovement Clone()
//        {
//            return new DirectionalMovement
//            {
//                maxVelocity = maxVelocity,
//                rotationSpeed = rotationSpeed,
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
