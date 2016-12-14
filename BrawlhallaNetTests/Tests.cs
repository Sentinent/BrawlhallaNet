using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrawlhallaNet;
using System.Threading.Tasks;

namespace BrawlhallaNetTests
{
    [TestClass]
    public class Tests
    {
        private BrawlhallaApiClient client;

        [ClassInitialize]
        public void Initialize()
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

            Assert.AreEqual(2, dan.BrawlhallaID);
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
    }
}
