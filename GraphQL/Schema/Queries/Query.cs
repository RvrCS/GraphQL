using Bogus;
using Bogus.DataSets;
using GraphQL.DTOs;
using GraphQL.Models;
using GraphQL.Schema.Filters;
using GraphQL.Schema.Queries;
using GraphQL.Services;
using GraphQL.Services.Courses;

namespace GraphQL.Schema.Queries
{
    public class Query
    {

        private readonly CoursesRepository _coursesRepository;

        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<IEnumerable<CourseType>> GetCourses()
        { 

            IEnumerable<CourseDTO> courseDTOs = await _coursesRepository.GetAll();

            return courseDTOs.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId
                
            });

        }

        [UseDbContext(typeof(SchoolDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering(typeof(CourseFilterType))]
        [UseSorting]
        public IQueryable<CourseType> GetPaginatedCourses([ScopedService] SchoolDbContext context )
        {


            return context.Courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId

            });

        }

        public async Task<CourseType> GetCourseById(Guid id)
        {
            CourseDTO courseDTO = await _coursesRepository.GetById(id);

            return new CourseType()
            {
                
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };

        }

        [GraphQLDeprecated("This query is deprecated")]
        public string Instructions => "Testing query";

        

    }
}
