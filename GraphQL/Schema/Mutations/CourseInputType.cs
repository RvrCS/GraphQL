using GraphQL.Models;
using GraphQL.Schema.Queries;

namespace GraphQL.Schema.Mutations
{
    public class CourseInputType
    {
        public string Name { get; set; }

        public Subject Subject { get; set; }

        public Guid InstructorId { get; set; }

    }
}
