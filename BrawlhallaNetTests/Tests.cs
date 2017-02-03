using System;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrawlhallaNet;
using System.Threading.Tasks;

namespace BrawlhallaNetTests
{
    [TestClass]
    public class Tests
    {
        private static BrawlhallaApiClient client;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            using (var streamReader = new StreamReader("apikey.txt")) // This is .gitignored
            {
                client = new BrawlhallaApiClient(streamReader.ReadToEnd());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullKeyken()
        {
            var client = new BrawlhallaApiClient(null);      
        }

        [TestMethod]
        public async Task TestSearchAsync()
        {
            var dan = await client.GetPlayerFromSteamIdAsync(76561198025185087);

            Assert.AreEqual((ulong)2, dan.BrawlhallaID);
            Assert.AreEqual("[BMG] Dan", dan.Name); // This test could fail frequently
        }

        [TestMethod]
        public async Task TestGetLegendInfoAsync()
        {
            var bodvar = await client.GetLegendInfoAsync(Constants.Legends.Bodvar);

            Assert.AreEqual("bodvar", bodvar.Name);
            Assert.AreEqual("5", bodvar.Speed);
            Assert.AreEqual("Hammer", bodvar.WeaponOne);
        }

        [TestMethod]
        [ExpectedException(typeof(BrawlhallaNetException))]
        public async Task TestGetNonExistantLegendInfoAsync()
        {
            var nonexistantLegend = await client.GetLegendInfoAsync(Constants.Legends.Nonexistant);
        }

        [TestMethod]
        public async Task TestGetClanInfoAsync()
        {
            var clan = await client.GetClanAsync(22);

            Assert.AreEqual("NEKO", clan.Name);
            Assert.AreEqual(1464206400, clan.ClanCreationDateUnix);
        }

        [TestMethod]
        public async Task TestGetRankedPlayerAsync()
        {
            var player = await client.GetRankedPlayerAsync(1003166);

            // Could also fail at any time
            Assert.AreEqual("(b)✨ithrowowowowowowowowowowow", player.Name);
        }

        [TestMethod]
        public async Task TestGetRankedPageAsync()
        {
            var topPlayers = await client.GetRankedPageAsync("1v1", "all", 1);

            Assert.AreNotEqual(0, topPlayers.Count);
            Assert.IsFalse(String.IsNullOrEmpty(topPlayers.FirstOrDefault().Name));
        }

        [TestMethod]
        public async Task TestGetInvalidRankedPlayerAsync() // if the player hasnt played a rank game yet. expect this to break
        {
            var invalidPlayer = await client.GetRankedPlayerAsync(457872);

            Assert.AreEqual((ulong)0, invalidPlayer.BrawlhallaId);
        }
    }
}
