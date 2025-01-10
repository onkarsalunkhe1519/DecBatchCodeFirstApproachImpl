using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace DecBatchCodeFirstApproachImpl.Filter
{
    public class CustomActionFilter : Attribute, IActionFilter
    {
       

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var controller = context.Controller;

            var controllerName = context.Controller.GetType().Name;

            var actionName = context.ActionDescriptor.RouteValues["action"];

            if (string.IsNullOrEmpty(actionName))
            {
                throw new Exception($"Action name could not be retrieved. RouteValues: {string.Join(", ", context.ActionDescriptor.RouteValues)}");
            }


            var actionExists = controller.GetType()
                                         .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                         .Any(m => string.Equals(m.Name, actionName, StringComparison.OrdinalIgnoreCase));
            if (!actionExists)
            {
                throw new Exception($"Action '{actionName}' does not exist in the controller '{controllerName}'.");
            }

            var requestMethod = context.HttpContext.Request.Method;
            var requestPath = context.HttpContext.Request.Path;
            var parameters = context.ActionArguments;

            var logMessage = $"Request: {requestMethod} {requestPath} - Controller: {controllerName}, Action: {actionName}";
            LogToFile(logMessage);
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Log response details
            var statusCode = context.HttpContext.Response.StatusCode;
            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;

            var logMessage = $"Response: {controllerName} - {actionName} StatusCode: {statusCode}";
            LogToFile(logMessage);
        }

        private void LogToFile(string messsege)
        {
            var filepath = "action_log.txt";


            using (var writer = new StreamWriter(filepath, append: true))
            {

                writer.WriteLine($"{DateTime.Now}:{messsege}");
                writer.WriteLine();

            }

        }
    }
}
