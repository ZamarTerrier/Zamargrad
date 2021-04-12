using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Buffs;
using Zamargrad.Logic.Character;
using Zamargrad.Logic.Generators;
using Zamargrad.Logic.Tools.Exeptions;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Skills.Magic
{
    public class MagicService : ModuleBase<SocketCommandContext>
    {
        public static Dictionary<string, Magic> magics = new Dictionary<string, Magic>();
        //Создаем магию
        public static async Task CreateMagic(string text, SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            string[] textArray = text.Split(",");

            if (Context.Channel.Name != "школа-магических-исскуств")
            {
                await Loging.Log("Магию можно создавать только в школе магических исскуств", Context.Channel);
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
                    case "лечение":
                        magics.Add(name, new Magic(Magic.magicType.Heal, name, value, effect));
                        break;
                    case "урон":
                        magics.Add(name, new Magic(Magic.magicType.Damage, name, value, effect));
                        break;
                    case "усиление":
                        magics.Add(name, new Magic(Magic.magicType.PowerUp, name, value, effect));
                        break;
                    default:
                        break;
                }
                await Loging.Log("Заклинание " + name + " готово", Context.Channel);
            }
            catch
            {
                await Loging.Log("Не то ты говоришь", Context.Channel);
            }
        }
        //Юзаем магию
        public static async Task UseMagic(SocketCommandContext Context, string text)
        {
            if (!Exeptioner.ValidMe(Context)) return;

            string[] newText = text.Split(",");            

                //находим игрока
                var player = PersonService.persons[Context.User.Username];


                //Находим магию
                var magic = player.skills[newText[0]] as Magic;

                //кастуем магию
                if (player.p_Mana - magic.cost > 0)
                {
                    switch (magic.type)
                    {
                        case Magic.magicType.Heal:
                            await CastHeal(player, magic, Context, newText.Length > 1 ? newText[1] : string.Empty);
                            break;
                        case Magic.magicType.Damage:
                            await CastDamage(player, magic, Context);
                            break;
                        case Magic.magicType.PowerUp:
                            await CastPowerUp(player, magic, Context, newText.Length > 1 ? newText[1] : string.Empty);
                            break;
                    }
                }
                else await Loging.Log("Недостаточно Маны", Context.Channel);
        }
        //Кастуем урон
        private static async Task CastDamage(Person player,Magic magic, SocketCommandContext Context)
        {
            if (player.fight == false)
                await Task.Run(() => Loging.Log(Context.User.Username + " кастанул " + magic.name + " в воздух и вокруг теперь " + magic.effect, Context.Channel));
            else
            {

                player.p_Mana -= magic.cost;

                await TravelService.SetAttack(player, magic);
            }
        }
        //Кастуем хил
        private static async Task CastHeal(Person player, Magic magic, SocketCommandContext Context, string target)
        {
            //Если нет цели
            if(target == string.Empty)
            {
                player.p_Mana -= magic.cost;

                await player.GetHeal(magic.value, true);

                await Task.Run(() => Loging.Log(Context.User.Username + " подлечил себя способностью" + magic.name + " на " + magic.value + " и " + magic.effect, Context.Channel));

            }
            else
            {
                var pTarget = Exeptioner.ValidPlayer(target, Context) ? PersonService.persons[target] : null;

                if (pTarget == null) return;

                player.p_Mana -= magic.cost;

                await pTarget.GetHeal(magic.value, true);

                await Task.Run(() => Loging.Log(Context.User.Username + " подлечил " + pTarget.name + " способностью " + magic.name + " на " + magic.value + " и " + magic.effect, Context.Channel));

            }

        }
        //Кастуем усиление
        private static async Task CastPowerUp(Person player, Magic magic, SocketCommandContext Context, string target)
        {
            //Если нет цели
            if (target == string.Empty)
            {
                player.p_Mana -= magic.cost;

                await player.ApplyStat(0,0,magic.value, magic.value, magic.value);

                player.buffs.Add(new Buff(Buff.buffType.PowerUp, magic.value));

                await Task.Run(() => Loging.Log(Context.User.Username + " кастанул " + magic.name + " на себя и " + magic.effect + " усилив себя на " + magic.value, Context.Channel));

            }
            else
            {
                var pTarget = Exeptioner.ValidPlayer(target, Context) ? PersonService.persons[target] : null;

                if (pTarget == null) return;

                player.p_Mana -= magic.cost;

                await pTarget.ApplyStat(magic.value, magic.value, magic.value);

                pTarget.buffs.Add(new Buff(Buff.buffType.PowerUp, magic.value));

                await Task.Run(() => Loging.Log(Context.User.Username + " кастанул " + magic.name + " на " + pTarget.name + " и тот " + magic.effect + " усилив на " + magic.value, Context.Channel));

            }

        }

    }
}
