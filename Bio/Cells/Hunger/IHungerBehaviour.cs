using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio
{
    interface IHungerBehaviour : IBehaviourClass
    {
        FoodKind FoodKind { get; }
    }
}
