using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Items
{
    public class Item
    {
        public enum itemType {
            Decor,
            Use,
            ManaFlask,
            HealthFlask
        }

        public itemType type;
        public string name;
        public string description;
        public int cost;

        public Item(itemType type, string name, string description)
        {
            this.type = type;
            this.name = name;

            switch (type)
            {
                case itemType.Decor:
                    this.description = "Декоративный предмет\n" + "Описание : " + description;
                    break;
                case itemType.Use:
                    this.description = "Используемый предмет\n" + "Описание : " + description;
                    break;
            }
        }
    }
}
