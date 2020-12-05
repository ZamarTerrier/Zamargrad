using System;
using System.Collections.Generic;
using System.Text;

using Zamargrad.Logic.Character;

namespace Zamargrad.Logic.Items.Flasks
{
    class Health : Item
    {
        int count;

        public Health(string name, int count, int cost) :base(Item.itemType.HealthFlask, name, "Эликсир, выпив который заживаю любые раны.")
        {
            this.count = count;
            this.cost = cost;
        }

        public void HealthRegen(Person ply)
        {
            ply.p_Hp += ply.p_MaxHP;

            if (ply.p_Hp > ply.p_MaxHP)
            {
                ply.p_Hp = ply.p_MaxHP;
            }
        }

    }
}
