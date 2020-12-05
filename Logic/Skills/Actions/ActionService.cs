using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Zamargrad.Logic.Character;
using Zamargrad.Logic.Tools.Log;
using Zamargrad.Logic.Tools.Exeptions;

namespace Zamargrad.Logic.Skills.Actions
{
    class ActionService
    {
        static Dictionary<string, Action> actions = new Dictionary<string, Action>();

        public static async Task CreateAction(string text, SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            string[] textArray = text.Split(",");

            //if (!Exeptioner.ValidMe(Context)) return;

            try
            {
                string name = textArray[0];
                if (name[0] == ' ')
                    name = name.Substring(1);

                string effect = textArray[1];
                if (effect[0] == ' ')
                    effect = effect.Substring(1);

                actions.Add(name, new Action(name, effect));

                await Loging.Log("Теперь ты можешь " + name, Context.Channel);
            }
            catch
            {
                await Loging.Log("Не то ты говоришь", Context.Channel);
            }
        }

        public static async Task UseAction(SocketCommandContext Context, string text)
        {
            string[] newText = text.Split(",");



            try
            {
                var a = actions[newText[0]];
                if (newText.Length > 1)
                {
                    if(newText[1][0] == ' ')
                        newText[1] = newText[1].Substring(1);

                    //var p = Exeptioner.ValidPlayer(newText[1], Context) ? PersonService.persons[newText[1]] : null;

                    //if (p != null)
                    await Task.Run(() => Loging.Log(Context.User.Username + " предложил " + newText[1] + " " + a.name, Context.Channel));
                }
                else
                    await Task.Run(() => Loging.Log(Context.User.Username + " решил " + a.name + " после чего " + a.effect, Context.Channel));

                return;
            }
            catch
            {
                await Loging.Log("Не то ты говоришь", Context.Channel);
            }
        }

    }
}
