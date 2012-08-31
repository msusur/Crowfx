using System.Linq;
using Crow.Library.Foundation.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crow.Library.Tests.Hosting
{
    [TestClass]
    public class UrlParserTest
    {
        [TestMethod]
        public void Parses_a_secure_url_with_parameters()
        {
            UrlParser parser = new UrlParser("https://localhost/test/method?id=2&name=mert");
            Assert.IsTrue(parser.IsHttps);
            Assert.AreEqual<int>(80, parser.Port);
            Assert.AreEqual<string>("localhost", parser.Hostname);
            Assert.AreEqual<string>("test", parser.BusinessClass);
            Assert.AreEqual<string>("method", parser.BusinessAction);
            Assert.AreEqual<int>(2, parser.Parameters.Count);
            Assert.AreEqual<string>("id", parser.Parameters.Keys.First());
            Assert.AreEqual<string>("2", parser.Parameters.Values.First());
            Assert.AreEqual<string>("name", parser.Parameters.Keys.Last());
            Assert.AreEqual<string>("mert", parser.Parameters.Values.Last());
        }

        [TestMethod]
        public void Parses_a_non_secure_url_with_parameters()
        {
            UrlParser parser = new UrlParser("http://localhost/test/method?id=2&name=mert");
            Assert.IsFalse(parser.IsHttps);
            Assert.AreEqual<int>(80, parser.Port);
            Assert.AreEqual<string>("localhost", parser.Hostname);
            Assert.AreEqual<string>("test", parser.BusinessClass);
            Assert.AreEqual<string>("method", parser.BusinessAction);
            Assert.AreEqual<int>(2, parser.Parameters.Count);
            Assert.AreEqual<string>("id", parser.Parameters.Keys.First());
            Assert.AreEqual<string>("2", parser.Parameters.Values.First());
            Assert.AreEqual<string>("name", parser.Parameters.Keys.Last());
            Assert.AreEqual<string>("mert", parser.Parameters.Values.Last());
        }

        [TestMethod]
        public void Parses_a_non_secure_url_with_port_and_parameters()
        {
            UrlParser parser = new UrlParser("http://localhost:8080/test/method?id=2&name=mert");
            Assert.IsFalse(parser.IsHttps);
            Assert.AreEqual<int>(8080, parser.Port);
            Assert.AreEqual<string>("localhost", parser.Hostname);
            Assert.AreEqual<string>("test", parser.BusinessClass);
            Assert.AreEqual<string>("method", parser.BusinessAction);
            Assert.AreEqual<int>(2, parser.Parameters.Count);
            Assert.AreEqual<string>("id", parser.Parameters.Keys.First());
            Assert.AreEqual<string>("2", parser.Parameters.Values.First());
            Assert.AreEqual<string>("name", parser.Parameters.Keys.Last());
            Assert.AreEqual<string>("mert", parser.Parameters.Values.Last());
        }

        [TestMethod]
        public void Parses_a_non_secure_url_with_port_and_without_parameters()
        {
            UrlParser parser = new UrlParser("http://localhost:8080/test/method");
            Assert.IsFalse(parser.IsHttps);
            Assert.AreEqual<int>(8080, parser.Port);
            Assert.AreEqual<string>("localhost", parser.Hostname);
            Assert.AreEqual<string>("test", parser.BusinessClass);
            Assert.AreEqual<string>("method", parser.BusinessAction);
            Assert.AreEqual<int>(0, parser.Parameters.Count);
        }

    }
}
