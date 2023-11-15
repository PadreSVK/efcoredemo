internal class MyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MyMiddleware> logger;

    public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger)
    {
        _next = next;
        this.logger = logger;
    }

    public Task Invoke(HttpContext httpContext)
    {
        logger.LogInformation("called HTTP method" + httpContext.Request.Method);
        
        return _next(httpContext);
        //if(httpContext.Request.Method == "GET")
        //{
        //    logger.LogInformation("is get.... circuit break");
        //    return Task.CompletedTask;
        //}
    }

}