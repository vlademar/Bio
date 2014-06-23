using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using Bio.Cells;

namespace Bio
{
    class CellFactory
    {
        private static CultureInfo CI = CultureInfo.InvariantCulture;
        private static Deviation zeroDeviation = new Deviation();

        private class CellProt
        {
            public string Name { get; set; }
            public List<BehaviourProt> Behaviours { get; set; }
        }

        private class BehaviourPropertyProt
        {
            public string Name { get; set; }
            public DeviationGroup Group { get; set; }
            public double DefaultValue { get; set; }
            public double DeviationPow { get; set; }
        }

        private class BehaviourProt
        {
            public string Name { get; set; }
            public SortedList<string, BehaviourPropertyProt> Properties { get; set; }
        }

        private PetriDish _petri;
        private SortedList<string, CellProt> cellProts;
        private SortedList<string, Type> behavoiurTypes;

        public CellFactory(XDocument doc, PetriDish petri)
        {
            _petri = petri;
            cellProts = new SortedList<string, CellProt>(
                doc.Elements("Cell").Select(cell => new CellProt
                {
                    Name = cell.Attribute("Name").Value,
                    Behaviours = new List<BehaviourProt>(
                        cell.Elements()
                            .Select(b => new BehaviourProt
                            {
                                Name = b.Name.LocalName,
                                Properties = new SortedList<string, BehaviourPropertyProt>(
                                    b.Elements().Select(prop =>
                                        new BehaviourPropertyProt
                                        {
                                            Name = prop.Name.LocalName,
                                            Group = prop.Attribute("Group") == null ? DeviationGroup.None : (DeviationGroup)Convert.ToInt32(prop.Attribute("Group").Value, CI),
                                            DefaultValue = prop.Attribute("DefaultValue") == null ? -1 : Convert.ToDouble(prop.Attribute("DefaultValue").Value, CI),
                                            DeviationPow = prop.Attribute("DeviationPow") == null ? -1 : Convert.ToDouble(prop.Attribute("DeviationPow").Value, CI)
                                        }).ToDictionary(p => p.Name))
                            }))
                }).ToDictionary(i => i.Name));

            behavoiurTypes = new SortedList<string, Type>(
                typeof(CellFactory).Assembly
                    .GetTypes()
                    .Where(type => type.IsSubclassOf(typeof(CellBehaviourBase)))
                    .ToDictionary(cl => cl.Name));
        }

        public static XDocument CreatePattern()
        {
            var behaviours = typeof(CellFactory).Assembly
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(CellBehaviourBase)));

            XElement xCell = new XElement("Cell",
                new XAttribute("Name", "SAMPLE"));

            foreach (var behaviour in behaviours)
            {
                var properties = behaviour.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Select(prop =>
                        new
                        {
                            Property = prop,
                            Attribute = prop.GetCustomAttributes(typeof(BehaviourPropertyAttribute), true).FirstOrDefault() as BehaviourPropertyAttribute
                        })
                    .Where(pair => pair.Attribute != null);

                var xBehaviour = new XElement(behaviour.Name);
                xCell.Add(xBehaviour);

                foreach (var property in properties)
                {
                    xBehaviour.Add(
                        new XElement(property.Property.Name,
                            new XAttribute("Group", 1),
                            new XAttribute("DefaultValue", 0.5.ToString(CI)),
                            new XAttribute("DeviationPow", 3.0.ToString(CI))));
                }

            }

            return new XDocument(xCell);
        }

        public Cell BuildCell(string cellKind)
        {
            if (!cellProts.ContainsKey(cellKind))
                throw new Exception("Не найден шаблон клетки \"" + cellKind + "\"");

            List<CellBehaviourBase> behaviours = new List<CellBehaviourBase>();

            foreach (var behaviour in cellProts[cellKind].Behaviours)
            {
                var b = (CellBehaviourBase)behavoiurTypes[behaviour.Name].GetConstructor(new Type[0]).Invoke(new object[0]);

                var propertiesWithAttributes =
                    behavoiurTypes[behaviour.Name]
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .Select(prop =>
                            new
                            {
                                Property = prop,
                                Attribute =
                                    prop.GetCustomAttributes(typeof(BehaviourPropertyAttribute), true).FirstOrDefault()
                                        as BehaviourPropertyAttribute
                            })
                        .Where(pair => pair.Attribute != null);

                foreach (var property in propertiesWithAttributes)
                {
                    var pr = behaviour.Properties[property.Property.Name];

                    property.Attribute.MC.Group = pr.Group;
                    property.Attribute.MC.DefaultValue = pr.DefaultValue;
                    property.Attribute.MC.DeviationPow = pr.DeviationPow;
                }

                BehaviourPropertyAttribute.UpdateValues(b, zeroDeviation);

                behaviours.Add(b);
            }

            return new Cell(_petri, behaviours);
        }
    }
}
