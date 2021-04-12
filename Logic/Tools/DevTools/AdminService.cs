using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Generators;
using Zamargrad.Logic.Tools.Log;
using Zamargrad.Logic.Character; 

namespace Zamargrad.Logic.Tools.DevTools
{
    public class AdminService
    {

        public static async Task ClearAll(SocketCommandContext Context)
        {
            
                for (int i = 0; i < TravelService.lockWay.Length; i++)
                {
                    TravelService.lockWay[i] = false;
                    TravelService.enemys[i] = null;
                }

                foreach(Person p in PersonService.persons.Values){
                    p.way = -1;
                    p.fight = false;
                    p.p_Hp = p.p_MaxHP;
                    p.p_Mana = p.p_MaxMana;
                }

            await Loging.Log("Мир чист", Context.Channel);
        }

    }
}
