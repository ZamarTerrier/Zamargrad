using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Items.Forest
{
    public class Skin : Item
    {
        public Skin(string name, int count, int cost) : base(Item.itemType.Skin, name, "Кожа благородного животного!")
        {
            this.count = count;
            this.cost = cost;
        }
    }
}
