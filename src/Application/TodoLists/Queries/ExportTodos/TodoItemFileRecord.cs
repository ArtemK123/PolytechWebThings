using PolytechWebThings.Application.Common.Mappings;
using PolytechWebThings.Domain.Entities;

namespace PolytechWebThings.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
