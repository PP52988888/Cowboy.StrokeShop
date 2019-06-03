// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-05-2019
//
// Last Modified By : pan
// Last Modified On : 03-05-2019
// ***********************************************************************
// <copyright file="ApiExceptionFilterAttribute.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;//重点
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Common;

namespace Cowboy.Stroke.AspNetCore.ApiControllers.Filters
{
    /// <summary>
    /// Class ApiExceptionFilterAttribute.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute" />
    public class ApiExceptionFilterAttribute: ExceptionFilterAttribute
    {
        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<ExceptionFilterAttribute>>();
        logger.LogError(context.Exception, "http::url={0}", context.HttpContext.Request.Path);
            if (!context.ExceptionHandled)
            {
                if (context.Exception is BusinessException exception)
                {
                    context.Result = new ObjectResult(new Response
                    {
                        Code = exception.Code,
                        Message = exception.Message

    });
                }
                else
                {
                    context.Result = new ObjectResult(new Response
                    {
                        Code = 500,
                        //Message = context.HttpContext.RequestServices.GetService < IHostingEnvironment>().IsDevelopment()
                        //? context.Exception.ToString()
                        //: "服务内部错误"
                        Message = context.Exception.Message
                    });
                }
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}
