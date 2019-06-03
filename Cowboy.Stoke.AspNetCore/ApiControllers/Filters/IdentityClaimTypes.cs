// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="IdentityClaimTypes.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.ApiControllers.Filters
{
    /// <summary>
    /// Class IdentityClaimTypes. This class cannot be inherited.
    /// </summary>
    public sealed class IdentityClaimTypes
    {

        /// <summary>
        /// 用户编号声明
        /// </summary>
        public const string UserId = "http://schemas.cowboy.com/ws/2008/06/identity/claims/user-identifier";

        /// <summary>
        /// 用户名称声明
        /// <para><see cref="ClaimTypes.Surname" />的快捷方法</para>
        /// </summary>
        public const string UserName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

        /// <summary>
        /// 用户角色声明
        /// <para><see cref="System.Security.Claims.ClaimTypes.Role" />的快捷方式</para>
        /// </summary>
		public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    }
}
