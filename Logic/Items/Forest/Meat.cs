using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Items.Forest
{
    public class Meat : Item
    {
        public Meat(string name, int count, int cost) : base(Item.itemType.Meat, name, "Вкуснейшее мясо дикого животного")
        {
            this.count = count;
            this.cost = cost;
        }
    }
}
