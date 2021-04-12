using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;

using Zamargrad.Logic.Shop;


namespace Zamargrad.Modules
{
    [Name("Shop")]
    public class Shop : ModuleBase<SocketCommandContext>
    {
        [Command("товар")]
        [Summary("Показать товары на рынке")]
        public Task ShowShop() => ShopService.MakeShop(Context);

        [Command("купить")]
        [Summary("Купить товары на рынке")]
        public Task BuyShop([Remainder] string text) => ShopService.Buyshop(text, Context);

    }
}
