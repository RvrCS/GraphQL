using GraphQL.DTOs;
using GraphQL.Schema.Mutations;
using GraphQL.Schema.Queries;
using GraphQL.Schema.Subscriptions;
using GraphQL.Services.Courses;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;

namespace GraphQL.Schema
{
    public class Mutation
    {
        private readonly CoursesRepository _coursesRepository;

        public Mutation(CoursesRepository courses)
        {
            _coursesRepository = courses;
        }

        [Authorize]
        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {

            CourseDTO courseDTO = CourseMapper(courseInput);


            courseDTO = await _coursesRepository.Create(courseDTO);

            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

            return course;

        }

        [Authorize]
        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseDTO courseDTO = CourseMapper(courseInput);
            courseDTO.Id = id;

            courseDTO = await _coursesRepository.Update(courseDTO);

            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        [Authorize]
        public async Task<bool> DeleteCourse(Guid id)
        {
            try
            {
                return await _coursesRepository.Delete(id);
            }catch (Exception ex)
            {
                return false;
            }
        }

        public CourseDTO CourseMapper(CourseInputType courseInput)
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            return courseDTO;
        }

    }
}
