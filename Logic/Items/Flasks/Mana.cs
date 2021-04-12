using System;
using System.Collections.Generic;
using System.Text;

using Zamargrad.Logic.Character;

namespace Zamargrad.Logic.Items.Flasks
{
    class Mana : Item
    {

        int count;

        public Mana(string name, int count, int cost):base(Item.itemType.ManaFlask, name, "Эликсир, выпив который восстанавливается духовная сила.")
        {
            this.count = count;
            this.cost = cost;
        }

        public void ManaRegen(Person ply)
        {
            ply.p_Mana += count;

            if(ply.p_Mana > ply.p_MaxMana)
            {
                ply.p_Mana = ply.p_MaxMana;
            }
        }

    }
}
