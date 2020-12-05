using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Zamargrad.Logic.Character;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Tools.Exeptions
{
    public static class Exeptioner
    {
        public static bool ValidPlayer(string name, SocketCommandContext Context)
        {
            try
            {
                if (name[0] == ' ')
                    name = name.Substring(1);

                var p = PersonService.persons[name];
                return true;
            }catch
            {
                Task.Run(()=> Loging.Log("Нет такого челика", Context.Channel));
                return false;
            }
        }

        public static bool ValidMe(SocketCommandContext Context, bool talk = true)
        {
            try
            {
                var p = PersonService.persons[Context.User.Username];

                if(p.ghost && Context.Channel.Name != "духи")
                {
                    Task.Run(()=>Loging.Log("Дух " + Context.User.Username + " яростно машет руками и пытается что-то сделать.", Context.Channel));
                    return false;
                }else
                    return true;
            }
            catch
            {
                if(talk)
                    Task.Run(() => Loging.Log("Ты не зарегестрирован в городе", Context.Channel));
                return false;
            }
        }

    }
}
