using HotChocolate.Types;
using HotChocolateSample.Core;

namespace HotChocolateSample.GraphQL
{
    public class CompanyType : ObjectType<Company>
    {
        protected override void Configure(IObjectTypeDescriptor<Company> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Name).Type<StringType>();
            descriptor.Field(a => a.Revenue).Type<DecimalType>();
        }
    }
}