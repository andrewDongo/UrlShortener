using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using UrlShortening.Domain.Model;

namespace UrlShortening.Web.Controllers
{
    /// <summary>
    /// Performs model state validation for ApiController Action
    /// Returns Bad request and generic modelstate 
    /// </summary>
    public class ValidateModelStateAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (filterContext.ModelState.IsValid == false)
            {
                var errors = filterContext.ModelState.SelectMany(item => item.Value.Errors).ToList();

                var errorCollection = errors.Select(error => error.ErrorMessage).ToList(); //get all errors from modelstate
                filterContext.ModelState.Clear(); // clear modelstate
                foreach (var err in errorCollection) // add generic modelstate errors
                {
                    filterContext.ModelState.AddModelError("", err);
                }

                filterContext.Response = filterContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, filterContext.ModelState);
            }
        }
    }

    /// <summary>
    /// Checks ApiController Action model is null.
    /// Returns Bad request if model is null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CheckNullModelAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private readonly Func<Dictionary<string, object>, bool> _validate;

        public CheckNullModelAttribute()
            : this(arguments =>
                arguments.ContainsValue(null))
        { }

        public CheckNullModelAttribute(Func<Dictionary<string, object>, bool> checkCondition)
        {
            _validate = checkCondition;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (_validate(actionContext.ActionArguments))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Request is invalid : missing parameters");
            }
        }
    }

    /// <summary>
    /// Class to trim white space from string properties passed from client to server
    /// </summary>
    public class TrimModelBinder : DefaultModelBinder
    {
        protected override void SetProperty(ControllerContext controllerContext,
          ModelBindingContext bindingContext,
          System.ComponentModel.PropertyDescriptor propertyDescriptor, object value)
        {
            if (propertyDescriptor.PropertyType == typeof(string))
            {
                var stringValue = (string)value;
                if (!string.IsNullOrEmpty(stringValue))
                    stringValue = stringValue.Trim();

                value = stringValue;
            }

            base.SetProperty(controllerContext, bindingContext,
                                propertyDescriptor, value);
        }
    }

    public class ControllerHelpers
    {
        /// <summary>
        /// Method to add errors in Response object to ModelState of APIController
        /// </summary>
        /// <param name="result"></param>
        /// <param name="baseController"></param>
        public static void AddErrors(Response result, ApiController baseController)
        {
            if (!result.Errors.Any())
            {
                baseController.ModelState.AddModelError("", "An unexpected error occurred");
                return;
            }
            foreach (var error in result.Errors)
            {
                baseController.ModelState.AddModelError("", error);
            }
        }
    }
}