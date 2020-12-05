using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Items.Use
{
    public class UseItem:Item
    {
        public string action;

        public UseItem(itemType type, string name,string action, string description):base(type, name, description)
        {
            this.action = action;
        }

    }
}
