using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;

namespace Web.IntegrationTest.Controllers.CommonTestBases
{
    internal abstract class MockedHttpClientApiTestBase : StoredUserApiTestBase
    {
        protected Mock<HttpMessageHandler> HttpMessageHandlerMock { get; private set; }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
            HttpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            MockHttpClientFactory(services: services);
        }

        protected void SetupHttpMessageHandlerMock(Func<HttpRequestMessage, bool> matchRequestFunc, HttpResponseMessage response)
        {
            HttpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(message => matchRequestFunc(message)), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }

        protected void SetupHttpMessageHandlerMockWithException<TException>(Func<HttpRequestMessage, bool> matchRequestFunc, TException exception)
            where TException : Exception
        {
            HttpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(message => matchRequestFunc(message)), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(exception);
        }

        protected async Task<string> ReadContentFromDiskAsync(string pathFromProjRoot)
        {
            string resourcesFolder = Path.GetFullPath(pathFromProjRoot);
            return await File.ReadAllTextAsync(resourcesFolder);
        }

        private void MockHttpClientFactory(IServiceCollection services)
        {
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            HttpClient httpClient = new HttpClient(HttpMessageHandlerMock.Object);
            httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            services.AddTransient(_ => httpClientFactoryMock.Object);
        }
    }
}