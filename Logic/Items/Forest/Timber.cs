using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Items.Forest
{
    public class Timber : Item
    {
        public Timber(string name, int count, int cost) : base(Item.itemType.Timber, name, "Отличный древесный материал")
        {
            this.count = count;
            this.cost = cost;
        }
    }
}
