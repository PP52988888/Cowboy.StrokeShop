// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 01-16-2019
// ***********************************************************************
// <copyright file="AuthFilterAttribute.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.ApiControllers.Filters
{
    /// <summary>
    /// Class AuthFilterAttribute.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class AuthFilterAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// Called when [action executing].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey("Token"))
            {
                ((ApiController)context.Controller).UserId = long.Parse(context.HttpContext.Request.Headers["Token"]);
            }
            base.OnActionExecuting(context);
        }
    }
}
