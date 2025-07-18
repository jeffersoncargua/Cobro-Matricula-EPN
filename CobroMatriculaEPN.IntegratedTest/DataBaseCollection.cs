using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.IntegratedTest
{
    [CollectionDefinition("My Collection")]
    public class DataBaseCollection : ICollectionFixture<SharedDatabaseFixture>
    {

    }
}
