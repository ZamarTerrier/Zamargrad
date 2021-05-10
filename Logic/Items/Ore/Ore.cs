using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Items.Ore
{
    public class Ore : Item
    {
        public enum Type
        {
            Железа,
            Меди,
            Золота,
            Платины
        }
        
        Type oreType;
        

        public Ore(Type type, int count, int cost) : base(Item.itemType.Ore, type + " слиток", "Руда " + type)
        {
            this.oreType = type;
            this.count = count;
            this.cost = cost;
        }
    }
}
