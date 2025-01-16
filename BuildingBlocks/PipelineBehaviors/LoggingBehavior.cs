using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest :notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            "[START] handling Request {Request} - Response {Response} - RequestData {RequestData}",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            request
        );

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        var timeElapsed = timer.Elapsed;

        if (timeElapsed.Seconds > 3)
        {
            logger.LogWarning(
                "[PERFORMANCE] The request {Request} took {timeElapsed}",
                typeof(TRequest).Name,
                timeElapsed
            );
        }

        logger.LogInformation(
            "[END] handled Request {Request} with Response {Response}",
            typeof(TRequest).Name,
            typeof(TResponse).Name
        );

        return await next();
    }
}
