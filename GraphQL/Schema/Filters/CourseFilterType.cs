using GraphQL.Schema.Queries;
using HotChocolate.Data.Filters;

namespace GraphQL.Schema.Filters
{
    public class CourseFilterType : FilterInputType<CourseType>
    {

        protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(c => c.Students);
            base.Configure(descriptor);
        }

    }
}
