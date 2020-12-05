using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Skills.Magic;
using Zamargrad.Logic.Skills.Power;
using Zamargrad.Logic.Tools.Exeptions;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Skills
{
    public class SkillService
    {
        public static async Task GetMagic(string text, SocketCommandContext Context)
        {

            try
            {
                string[] skillText = text.Split(",");

                if (skillText.Length > 1)
                {

                }
                else
                {
                    var player = Exeptioner.ValidMe(Context)? PersonService.persons[Context.User.Username] : null;

                    if (player == null) return;

                    var magic = MagicService.magics[skillText[0]];

                    player.skills.Add(magic.name,magic);

                    await Loging.Log(player.name + " изучил " + magic.name, Context.Channel);

                }
            }
            catch
            {
                await Loging.Log("Нет такой магии!", Context.Channel);
            }

        }

        public static async Task GetPower(string text, SocketCommandContext Context)
        {

            try
            {
                string[] skillText = text.Split(",");

                if (skillText.Length > 1)
                {

                }
                else
                {
                    var player = Exeptioner.ValidMe(Context) ? PersonService.persons[Context.User.Username] : null;

                    if (player == null) return;

                    var power = PowerService.powers[skillText[0]];

                    player.skills.Add(power.name, power);
                    
                    await Loging.Log(player.name + " изучил " + power.name, Context.Channel);
                }
            }
            catch
            {
                await Loging.Log("Нет такого усиления!", Context.Channel);
            }

        }

    }
}
