﻿using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Tools.Exeptions;

namespace Zamargrad.Logic.Help
{
    public static class Helper
    {
        public static async Task Help(string text, SocketCommandContext Context)
        {
            switch (text)
            {
                case "магия":
                    await Context.Channel.SendMessageAsync("Для создания магии используйте:\n" +
             "!создать магию [урон/лечение/усиление],[название],[число],[эффект]\n" + 
             "Перед использованием магии убедитесь, что у вас изучена эта магия:\n" +
             "!изучить магию [название]\n" +
             "Для использования магии :\n" +
             "!кастануть [название] или !кастануть [название],[имя цели]\n");
                    break;
                case "усиления":
                    await Context.Channel.SendMessageAsync("Для создания усиление : \n" +
                "!создать усиление [защита,атака,усиление],[название],[число],[эффект]\n" +
                "Перед использованием усиления убедитесь, что у вас изучено это усиление:\n" +
                "!изучить усиление [название]\n" +
                "Для использования усиления :\n" +
                "!наложить [название] или !наложить [название],[имя цели]\n");
                    break;
                case "действия":
                    await Context.Channel.SendMessageAsync("Для создания действия : \n" +
                "!создать действие [название],[что сделал]\n" +
                "Для использования действия :\n" +
                "!решил [название] или !решил [название],[имя цели]\n");
                    break;
                case "предметы":
                    await Context.Channel.SendMessageAsync("Для создания пердмета : \n" +
                "!создать предмет [декор],[название],[что ощущает],[описание]\n" +
                "!создать предмет [актив],[название],[что с этим делать?],[описание]\n"+
                "Для получения предмета :\n" +
                "!получить [название]\n" +
                "Для использования предмета :\n" +
                "!использовать [название]\n" +
                "Для осмотра предмета :\n" +
                "!посмотреть на [название]\n" +
                "Для передачи предмета :\n" +
                "!передать [название],[ник игрока]\n");
                    break;
                case "торговля":
                    await Context.Channel.SendMessageAsync("Для продажи идите на рынок и используйте\n !продать [имя предмета]\n"+
                        "Для просмотра товаров на рынке используйте !товар\n" + 
                        "Для покупки товара введите !купить [номер товара]\n");
                    break;
                case "классы":
                    await Context.Channel.SendMessageAsync("Для выбора класса идите в регистрационную палату и используте\n !зарегать класс [маг/ассасин/воин/лучник]\n");
                    break;
                case "путешествие":
                    await Context.Channel.SendMessageAsync("Для начала путешествия, надо выбрать одну из троп, которые находятся за стенами (приключение)\n" +
                        "Для начала приключения используйте\n !путешествие\n" +
                        "Если вам попадется враг, не вздумайте убегать - это гарантированная смерть\n" +
                        "Для атаки используте\n !атаковать\n" +
                        "Также для атаки отлично подходят заклинания :\n" +
                        "!кастануть [название]"+
                        "Для того что бы атаковать врага товарища используйте\n !помочь [кому помогаем?]\n" +
                        "Удачного путешествия!");
                    break;
                case "потусторонний мир":
                    await Context.Channel.SendMessageAsync("Если вам посчастливилось погибнуть, вы превратитесь в духа и вам будет доступен только потусторонний мир!");
                    break;
                case "мир":
                    await Context.Channel.SendMessageAsync("Время в этом мире идет также как и в реальном мире. Все бафы и усиления спадают со временем, так что не прохлопайте момент использования");
                    break;
            }

        }

        public static async Task Help( SocketCommandContext Context)
        {
            if (!Exeptioner.ValidMe(Context, true))
            {
                await Context.Channel.SendMessageAsync("Давай сперва зарегестрируемся !регистрация");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Введите после !помощь что вас интересует \n" +
                  " мир/ классы/ магия/ усиления/ действия/ предметы/ торговля/ путешествие/ потусторонний мир\n");
            }

            
        }

    }
}