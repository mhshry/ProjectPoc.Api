using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ProjectPoc.Api.Features.Todos.Queries;

public static class GetTodos
{
    public record Query : IRequest<IEnumerable<TodoDto>>;

    public class Handler : IRequestHandler<Query, IEnumerable<TodoDto>>
    {
        private readonly Data.AppDbContext _db;

        public Handler(Data.AppDbContext db) => _db = db;

        public async Task<IEnumerable<TodoDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var items = await _db.TodoItems.AsNoTracking().ToListAsync(cancellationToken);
            return items.Select(i => new TodoDto(i.Id, i.Title, i.IsCompleted));
        }
    }
    public record TodoDto(int Id, string Title, bool IsCompleted);
}
