

namespace CobroMatriculaEPN.FunctionalTest.Controller
{
    [Collection("My Collection 2")]
    public class BaseControllerTest
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public BaseControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public HttpClient GetNewClient()
        {
            var newClient = _factory.WithWebHostBuilder(builder =>
            {
                _factory.CustomConfigureServices(builder);
            }).CreateClient();

            return newClient;
        }
    }
}
