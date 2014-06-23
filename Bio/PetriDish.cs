using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// средний уровень пищи : 100,000
// низкий уровень пищи  :  10,000
using System.Windows;

namespace Bio
{
    class PetriDish
    {
        private SortedList<int,Cell> cells = new SortedList<int, Cell>();

        /// <summary>
        /// Множитель вероятности мутации. 1 по умолчанию.
        /// </summary>
        public double RadiationLevel { get; private set; }

        public void Despawn(int cellId)
        {
            if (!cells.ContainsKey(cellId))
                throw new Exception("Клетка не найдена");

            cells.Remove(cellId);
        }

        public void Spawn( Cell cell ) 
        {
            if (cells.ContainsKey(cell.Id))
                throw new Exception("Клетка уже зарегистрирована");

            cells.Add(cell.Id, cell);
        }

        public double FoodLevel(Cell cell, FoodKind kind)
        {
            throw new NotImplementedException();
        }

        public Vector FoodLevelGradient(Cell cell, FoodKind kind)
        {
            throw new NotImplementedException();
        }

        public double ChangeFoodLevel(Cell cell, FoodKind kind, double delta)
        {
            throw new NotImplementedException();
        }
    }
}
