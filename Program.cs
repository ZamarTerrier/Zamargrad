using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Zamargrad
{
    class Program
    {
        static KingdomRun kingdom = new KingdomRun();
        public static Task Main(string[] args) => kingdom.RunAsync();
    }
}
