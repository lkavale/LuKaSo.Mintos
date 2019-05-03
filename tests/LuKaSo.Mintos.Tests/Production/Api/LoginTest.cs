using LuKaSo.Mintos.Api;
using LuKaSo.Mintos.Exceptions;
using LuKaSo.Mintos.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LuKaSo.Mintos.Tests.Production.Api
{
    [TestClass]
    public class LoginTest
    {
        private MintosApi _mintosClient;
        private User _mintosLoginOk;
        private User _mintosLoginWrong;

        [TestInitialize]
        public void Init()
        {
            _mintosClient = new MintosApi(new HttpClientBuilder().Build());
            _mintosLoginOk = new SecretsJsonReader().Read().LoginOk;
            _mintosLoginWrong = new SecretsJsonReader().Read().LoginWrong;
        }

        [TestMethod]
        public void GetCsrfTokenOk()
        {
            var token = _mintosClient.GetCsrfTokenAsync().GetAwaiter().GetResult();

            Assert.AreEqual(43, token.Length);
        }

        [TestMethod]
        public void LoginTokenOk()
        {
            var token = _mintosClient.GetCsrfTokenAsync().GetAwaiter().GetResult();
            _mintosClient.LoginAsync(_mintosLoginOk, token).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void LoginTokenBadLogin()
        {
            var token = _mintosClient.GetCsrfTokenAsync().GetAwaiter().GetResult();
            Assert.ThrowsExceptionAsync<BadLoginException>(() => _mintosClient.LoginAsync(_mintosLoginOk, token));
        }
    }
}
