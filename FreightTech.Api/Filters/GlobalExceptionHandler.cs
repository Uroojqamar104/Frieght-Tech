using FreightTech.Api.Extensions;
using System.Net;
using System.Web.Http.ExceptionHandling;

namespace FreightTech.Api.Filters
{
    public class AppExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            AppLogger.Instance.Error(0, context);
        }
    }

    public class GenericTextExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var jsonString = "{\"status\":false,\"message\":\"The server was unable to complete your request.\" }";
            context.Result = new RawJsonActionResult(jsonString, HttpStatusCode.InternalServerError);
            //AppLogger.Instance.Error(0, context.Exception);
            //context.Result = new InternalServerErrorTextPlainResult(
            //  "An unhandled exception occurred; check the log for more information.",
            //  Encoding.UTF8,
            //  context.Request);
        }
    }
}

//public class GlobalExceptionHandler : ExceptionHandler
//{
//    public void Handle(ExceptionHandlerContext context, CancellationToken cancellationToken)
//    {
//        AppLogger.Instance.Error(0, context.Exception);
//        //context.Result = new InternalServerErrorTextPlainResult(
//        //  "An unhandled exception occurred; check the log for more information.",
//        //  Encoding.UTF8,
//        //  context.Request);
//    }
//}
//public class GlobalExceptionHandler : IExceptionFilter
//{
//    //public ExceptionLoggerFilter(Logger logger)
//    //{
//    //    this.logger = logger;
//    //}

//    public bool AllowMultiple { get { return true; } }

//    public Task ExecuteExceptionFilterAsync(
//            HttpActionExecutedContext actionExecutedContext,
//            CancellationToken cancellationToken)
//    {
//        return Task.Factory.StartNew(() =>
//        {
//            AppLogger.Instance.Error(0, actionExecutedContext.Exception);
//        }, cancellationToken);
//    }

//}
