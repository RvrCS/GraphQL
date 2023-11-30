using GraphQL.Schema.Queries;

namespace GraphQL.DTOs
{
    public class InstructorDTO
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Salary { get; set; }

        public IEnumerable<CourseDTO> Courses { get; set; }

        public static implicit operator InstructorDTO(InstructorType v)
        {
            throw new NotImplementedException();
        }
    }
}
