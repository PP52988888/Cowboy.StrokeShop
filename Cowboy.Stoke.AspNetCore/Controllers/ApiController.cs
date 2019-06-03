// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 01-16-2019
// ***********************************************************************
// <copyright file="ApiController.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.Controllers
{
    /// <summary>
    /// Class ApiController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]/[action]")]
    [EnableCors(Defines.Default_Cors_Policy)]
    [AuthFilterAttribute]
    [ApiExceptionFilterAttribute]
    public class ApiController:ControllerBase
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        /// <value>The user identifier.</value>
        public long UserId { get; set; }
    }
}
