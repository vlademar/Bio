using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bio
{
    class Cell
    {
        public const double MaxHungerLevel = 100;

        private static int _nextId = 1;

        protected PetriDish Dish { get; private set; }
        protected ObservableCollection<CellBehaviourBase> behaviours;

        public int Id { get; private set; }

        public double Health { get; set; }
        public double Hunger { get; set; }

        public Vector Coordinate { get; set; }
        public Vector Direction { get; set; }

        public Cell(PetriDish dish, IEnumerable<CellBehaviourBase> _behaviours)
        {
            Id = _nextId++;
            Dish = dish;

            Health = 100;
            Hunger = MaxHungerLevel;

            behaviours = new ObservableCollection<CellBehaviourBase>(_behaviours);

            behaviours.CollectionChanged += (sender, args) =>
                {
                    if (args.Action != NotifyCollectionChangedAction.Add)
                        return;

                    foreach (CellBehaviourBase item in args.NewItems)
                    {
                        item.Cell = this;
                        item.PetriDish = dish;
                    }
                };
        }


        public void Tick(int timeCompression)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Tick(timeCompression);

                Hunger -= behaviour.HungerPerTick * timeCompression;
            }
        }

        private void ReinitBehaviours()
        {
            foreach (var behaviour in behaviours)
                behaviour.OnInit();
        }

        /// <summary>
        /// Убивает нежизнеспособную клетку
        /// </summary>
        /// <param name="reason">Причина нежизнеспособности</param>
        public void ForceKill(string reason)
        {
            throw new NotImplementedException();
        }

        public void Divide()
        { }

        public IEnumerable<T> GetBehaviours<T>() where T : IBehaviourClass
        {
            // TODO: придумать что-нибудь поизящнее
            return behaviours.Where(b => (b is T)).Select(b => (T)Convert.ChangeType(b, typeof(T)));
        }
    }
}
