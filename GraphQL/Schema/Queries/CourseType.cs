using GraphQL.DataLoaders;
using GraphQL.DTOs;
using GraphQL.Models;
using GraphQL.Services.Instructors;

namespace GraphQL.Schema.Queries

{

    

    public class CourseType
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Subject Subject { get; set; }

        [IsProjected(true)]
        public Guid InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader) 
        { 
        
            InstructorDTO instructorDTO = await instructorDataLoader.LoadAsync(InstructorId);

            return new InstructorType() 
            { 
                Id = instructorDTO.Id, 
                FirstName = instructorDTO.FirstName,
                LastName = instructorDTO.LastName,
                Salary = instructorDTO.Salary
            };

        }

        public IEnumerable<StudentType> Students { get; set; }

 
    }
}
