using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Items.Decor
{
    public class DecorItem:Item
    {
        public string feel;

        public DecorItem(itemType type, string name,string feel, string description) : base(type, name, description)
        {
            this.feel = feel;
        }
    }
}
