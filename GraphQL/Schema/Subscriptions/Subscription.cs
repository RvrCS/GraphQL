using GraphQL.Schema.Mutations;
using GraphQL.Schema.Queries;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQL.Schema.Subscriptions
{
    public class Subscription
    {

        [Subscribe]
        public CourseResult CourseCreated([EventMessage] CourseResult course) => course;


        [SubscribeAndResolve]
        public ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_{nameof(Subscription.CourseUpdated)}";


            return topicEventReceiver.SubscribeAsync<CourseResult>(topicName);
        }
    }
}
