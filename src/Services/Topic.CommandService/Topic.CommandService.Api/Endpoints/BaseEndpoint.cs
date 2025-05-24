using Carter;
namespace Topic.CommandService.Api.Endpoints;
public abstract class BaseEndpoint<TCommand> : ICarterModule
    where TCommand : BaseCommand
{
    protected abstract string GetSuccessMessage { get; }
    public abstract void AddRoutes(IEndpointRouteBuilder app);

    protected async Task<IResult> ExecuteCommandAsync(
        TCommand cmd,
        Func<TCommand, Task> commandHandler,
        ILogger logger)
    {
        try
        {
            if (cmd is null)
            {
                string errorMessage = "Отсутствует тело запроса";
                logger.LogInformation(errorMessage);
                return Results.BadRequest(new ResponseDto { Message = errorMessage });
            }

            await commandHandler(cmd);

            return Results.Ok(cmd is CreateTopicCommand
                ? new CreateTopicResponseDto { Id = cmd.Id, Message = GetSuccessMessage }
                : new ResponseDto { Message = GetSuccessMessage }
            );
        }
        catch (InvalidOperationException ex)
        {
            string errorMessage = "Проверьте параметры запроса";
            logger.LogWarning(ex, errorMessage);
            return Results.BadRequest(new ResponseDto { Message = errorMessage });
        }
        catch (AggregateNotFoundException ex)
        {
            string errorMessage = "Записи с таким id не существует";
            logger.LogWarning(ex, errorMessage);
            return Results.NotFound(new ResponseDto { Message = errorMessage });
        }
        catch (Exception ex)
        {
            string errorMessage = "Ошибка при обновлении";
            logger.LogError(ex, errorMessage);
            return Results.Json(new ResponseDto { Message = errorMessage },
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}