using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Cells
{
    class MyClass
    {
        /// <summary>
        /// Категория девиации, к которой принадлежит свойство
        /// </summary>
        public DeviationGroup Group { get; set; }

        /// <summary>
        /// Базовое значение свойства
        /// </summary>
        public double DefaultValue { get; set; }

        /// <summary>
        /// Степень зависимости свойства от девиации
        /// </summary>
        public double DeviationPow { get; set; }        
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class BehaviourPropertyAttribute : Attribute
    {
        public string Name { get; set; }

        public MyClass MC { get; private set; }

        public BehaviourPropertyAttribute( string name)
        {
            Name = name;

            MC = new MyClass();
        }

        public static void UpdateValues(object targer, Deviation deviation)
        {
            var propertiesWithAttributes =
                targer.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Select(prop =>
                        new
                        {
                            Property = prop,
                            Attribute = prop.GetCustomAttributes(typeof(BehaviourPropertyAttribute), true).FirstOrDefault() as BehaviourPropertyAttribute
                        })
                    .Where(pair => pair.Attribute != null);

            foreach (var property in propertiesWithAttributes)
            {
                var newValue = property.Attribute.MC.DefaultValue * Math.Pow(deviation[property.Attribute.MC.Group], property.Attribute.MC.DeviationPow);

                property.Property.SetValue(targer, newValue);
            }
        }
    }
}
