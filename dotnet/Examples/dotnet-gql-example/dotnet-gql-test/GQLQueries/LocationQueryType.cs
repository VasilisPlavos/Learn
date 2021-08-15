using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace dotnet_gql_test.GQLQueries
{
    public class LocationQueryType : ObjectType<LocationQueries>
    {
        protected override void Configure(IObjectTypeDescriptor<LocationQueries> descriptor)
        {
            base.Configure(descriptor);

            descriptor
                .Field(f => f.GetLocations(default));

            descriptor
                .Field(f => f.GetLocation(default, default))
                .Argument("code", a => a.Type<StringType>());
        }
    }
}
