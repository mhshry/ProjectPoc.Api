using MediatR;
using FluentValidation;

namespace ProjectPoc.Api.Features.Todos.Commands;

public static class CreateTodo
{
    public record Command(string Title) : IRequest<TodoDto>;

    public class Handler : IRequestHandler<Command, TodoDto>
    {
        private readonly Data.AppDbContext _db;

        public Handler(Data.AppDbContext db) => _db = db;

        public async Task<TodoDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new Data.TodoItem { Title = request.Title, IsCompleted = false };
            _db.TodoItems.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new TodoDto(entity.Id, entity.Title, entity.IsCompleted);
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        }
    }

    public record TodoDto(int Id, string Title, bool IsCompleted);
}
