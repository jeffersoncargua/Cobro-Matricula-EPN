using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.FunctionalTest.Controller
{
    [Collection("My Collection2")]
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
