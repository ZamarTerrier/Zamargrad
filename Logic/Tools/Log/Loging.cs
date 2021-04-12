using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Zamargrad.Logic.Tools.Draw;

namespace Zamargrad.Logic.Tools.Log
{
    public static class Loging
    {
        public static async Task Log(string text, IMessageChannel Chanel)
        {
                Console.WriteLine(DateTime.Now.ToString() + " : " + text);
            await Chanel.SendMessageAsync(text);
        }
    }
}
