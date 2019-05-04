using LuKaSo.Mintos.Api;
using LuKaSo.Mintos.Exceptions;
using LuKaSo.Mintos.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LuKaSo.Mintos.Tests.Production.Api
{
    [TestClass]
    public class InvestorTest
    {
        private MintosApi _mintosClient;
        private User _mintosLogin;

        [TestInitialize]
        public void Init()
        {
            _mintosClient = new MintosApi(new HttpClientBuilder().Build());
            _mintosLogin = new SecretsJsonReader().Read().LoginOk;
        }

        [TestMethod]
        public void GetInvestorOverviewOk()
        {
            var token = _mintosClient.GetCsrfTokenAsync().GetAwaiter().GetResult();
            _mintosClient.LoginAsync(_mintosLogin, token).GetAwaiter().GetResult();
            var data = _mintosClient.GetInvestorOverviewAsync().GetAwaiter().GetResult().ToList();

            Assert.IsTrue(data.Count > 0);
        }

        [TestMethod]
        public void GetInvestorOverviewNotAuthorized()
        {
            Assert.ThrowsExceptionAsync<ServerErrorException>(() => _mintosClient.GetInvestorOverviewAsync());
        }
    }
}
