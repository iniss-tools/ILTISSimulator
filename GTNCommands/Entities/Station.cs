using System.Collections.Generic;

namespace GTNCommands.Entities
{
    internal readonly struct Station
    {
        public int ID { get; }
        public string Name { get; }

        public Station(int id, string name = "")
        {
            ID = id;
            Name = name;
        }

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>The fully qualified type name.</returns>
        public override string ToString()
        {
            return $"{ID} {Name}";
        }

        public static bool TryParseName(List<Station> stations, string name, out Station? station)
        {
            foreach (var st in stations)
                if (st.Name == name)
                {
                    station = st;
                    return true;
                }

            station = null;
            return false;
        }
    }
}