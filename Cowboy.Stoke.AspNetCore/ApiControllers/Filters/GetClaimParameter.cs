// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="GetClaimParameter.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.ApiControllers.Filters
{
    /// <summary>
    /// Class GetClaimParameter.
    /// </summary>
    public static  class GetClaimParameter
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <returns>System.Int64.</returns>
        /// <exception cref="Exception">
        /// 转换失败
        /// or
        /// 获取Token失败
        /// </exception>
        public static long UserId(this IHttpContextAccessor contextAccessor) {
            var claim = contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == IdentityClaimTypes.UserId);
            var userId=claim!=null?(long.TryParse(claim.Value,out long val)?val:throw new Exception("转换失败")):throw new Exception("获取Token失败");
            return userId;
        }
    }
}
