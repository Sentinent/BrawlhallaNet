using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrawlhallaNet;

namespace BrawlhallaNetTests
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        public static async Task MainAsync(string[] args)
        {
            BrawlhallaApiClient client = null;
            using (var streamReader = new StreamReader("apikey.txt"))
            {
                client = new BrawlhallaApiClient(streamReader.ReadToEnd());
            }

            client.Log += (message, thing) =>
            {
                Console.WriteLine(thing.Message);
            };

            var player = await client.GetRankedPlayerAsync(1003166);
            int a = 3;
        }
    }
}

