using PolytechWebThings.Domain.Common;
using PolytechWebThings.Domain.Entities;

namespace PolytechWebThings.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
