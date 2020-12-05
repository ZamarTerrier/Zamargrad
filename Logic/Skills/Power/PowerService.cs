using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Zamargrad.Modules;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Tools.Log;
using Zamargrad.Logic.Tools.Exeptions;
using Zamargrad.Logic.Buffs;

namespace Zamargrad.Logic.Skills.Power
{
    public class PowerService
    {
        public static Dictionary<string, Power> powers = new Dictionary<string, Power>();

        //Создаем Заклинание
        public static async Task CreatePower(string text, SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            string[] textArray = text.Split(",");

            //Чекаем зареганы ли мы
            //if (!Exeptioner.ValidMe(Context)) return;

            if (Context.Channel.Name != "школа-боевых-исскуств")
            {
                await Loging.Log("Усиления можно создавать только в школе боевых исскуств", Context.Channel);
                return;
            }

            try
            {
                string type = textArray[0];
                string name = textArray[1];
                if (name[0] == ' ')
                    name = name.Substring(1);

                int value = int.Parse(textArray[2]);

                string effect = textArray[3];
                if (effect[0] == ' ')
                    effect = effect.Substring(1);
                switch (type)
                {
                    case "атака":
                        powers.Add(name, new Power(Power.powerType.Урон, name, value, effect));
                                        await Loging.Log("Усиленени " + name + " готово", Context.Channel);
                        break;
                    case "защита":
                        powers.Add(name, new Power(Power.powerType.Защита, name, value, effect));
                                        await Loging.Log("Усиленени " + name + " готово", Context.Channel);
                        break;
                    case "усиление":
                        powers.Add(name, new Power(Power.powerType.Характеристики, name, value, effect));
                                        await Loging.Log("Усиленени " + name + " готово", Context.Channel);
                        break;
                    default:
                        await Loging.Log("Не то ты говоришь", Context.Channel);
                        break;
                }
            }
            catch
            {
                await Loging.Log("Не то ты говоришь", Context.Channel);
            }
        }


        //Используем заклинание
        public static async Task UsePower(SocketCommandContext Context, string text)
        {
            string[] newText = text.Split(",");

            if (!Exeptioner.ValidMe(Context)) return;

            try
            {
                var player = PersonService.persons[Context.User.Username];

                var power = player.skills[newText[0]] as Power;

                if(player.p_Mana - power.cost < 0)
                {
                    await Loging.Log("Не хватает маны!", Context.Channel);
                    return;
                }

                switch (power.type)
                {
                    case Power.powerType.Урон:
                        await ApplyDamage(player, power, Context, newText.Length > 1 ? newText[1] : string.Empty);
                        break;
                    case Power.powerType.Защита:
                        await ApplyDefence(player, power, Context, newText.Length > 1 ? newText[1] : string.Empty);
                        break;
                    case Power.powerType.Характеристики:
                        await ApplyPowerUp(player, power, Context, newText.Length > 1 ? newText[1] : string.Empty);
                        break;
                }

            }
            catch
            {
                await Loging.Log("Не то ты говоришь", Context.Channel);
            }

        }

        private static async Task ApplyDamage(Person player, Power power, SocketCommandContext Context, string target)
        {
            if(target == string.Empty)
            {
                player.p_Mana -= power.cost;

                await player.ApplyStat(0, power.value);

                player.buffs.Add(new Buff(Buff.buffType.Damage, power.value));

                await LogPower(Context, power, player);
            }
            else
            {
                if (!Exeptioner.ValidPlayer(target, Context)) return;

                player.p_Mana -= power.cost;

                var tPlayer = PersonService.persons[target];

                await player.ApplyStat(power.value);

                player.buffs.Add(new Buff(Buff.buffType.Damage, power.value));

                await LogPower(Context, power, player, tPlayer);
            }
        }

        private static async Task ApplyDefence(Person player, Power power, SocketCommandContext Context, string target)
        {
            if (target == string.Empty)
            {
                player.p_Mana -= power.cost;

                await player.ApplyStat(power.value);

                player.buffs.Add(new Buff(Buff.buffType.Damage, power.value));

                await LogPower(Context, power, player);
            }
            else
            {
                if (!Exeptioner.ValidPlayer(target, Context)) return;

                player.p_Mana -= power.cost;

                var tPlayer = PersonService.persons[target];

                await player.ApplyStat(power.value);

                player.buffs.Add(new Buff(Buff.buffType.Damage, power.value));

                await LogPower(Context, power, player, tPlayer);
            }
        }

        private static async Task ApplyPowerUp(Person player, Power power, SocketCommandContext Context, string target)
        {
            if (target == string.Empty)
            {
                player.p_Mana -= power.cost;

                await player.ApplyStat(0, 0, power.value, power.value, power.value);

                player.buffs.Add(new Buff(Buff.buffType.Damage, power.value));

                await LogPower(Context, power, player);
            }
            else
            {

                if (!Exeptioner.ValidPlayer(target, Context)) return;

                player.p_Mana -= power.cost;

                var tPlayer = PersonService.persons[target];

                await tPlayer.ApplyStat(0, 0, power.value, power.value, power.value);

                tPlayer.buffs.Add(new Buff(Buff.buffType.Damage, power.value));

                await LogPower(Context, power, player, tPlayer);

            }
        }

        private static async Task LogPower(SocketCommandContext Context, Power power, Person player, Person target = null)
        {
            if (target != null)
                await Loging.Log(player.name + " использовал усиление " + power.name + " повысив " + power.type + " на " + power.value + " у игрока" + target.name + " и он " + power.effect, Context.Channel);
            else
                await Loging.Log(player.name + " использовал усиление " + power.name + " повысив " + power.type + " на " + power.value + " и он " + power.effect, Context.Channel);
        }

    }
}
