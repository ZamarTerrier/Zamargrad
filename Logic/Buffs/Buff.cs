using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Buffs
{
    public class Buff
    {
        public enum buffType
        {
            Damage,
            Defence,
            PowerUp,
        }

        public float time, value;
        public buffType type;

        public Buff(buffType type, float value)
        {
            this.value = value;

            this.type = type;

            time = value * 0.7f < 5 ? 5 : value * 0.7f;
        }

    }
}
