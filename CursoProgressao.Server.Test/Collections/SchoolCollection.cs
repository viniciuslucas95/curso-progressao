using CursoProgressao.Server.Test.Fixtures;
using Xunit;

namespace CursoProgressao.Server.Test.Collections;

[CollectionDefinition("School")]
public class SchoolCollection : ICollectionFixture<SchoolDbFixture> { }
