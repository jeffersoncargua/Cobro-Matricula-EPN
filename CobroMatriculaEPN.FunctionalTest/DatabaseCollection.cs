using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.FunctionalTest
{
    [CollectionDefinition("My Collection 2")]
    public class DatabaseCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>
    {

    }
}
