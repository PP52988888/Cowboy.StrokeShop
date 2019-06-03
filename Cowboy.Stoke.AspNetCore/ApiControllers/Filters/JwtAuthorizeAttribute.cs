// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="JwtAuthorizeAttribute.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.ApiControllers.Filters
{
    /// <summary>
    /// Jwt授权过滤器
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute" />
    public class JwtAuthorizeAttribute : AuthorizeAttribute 
    {
        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        public JwtAuthorizeAttribute()
        {
            this.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }

        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        /// <param name="role">The role.</param>
        public JwtAuthorizeAttribute(object role) : this()
        {
            this.Roles = role.ToString();
        }

        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        /// <param name="role1">The role1.</param>
        /// <param name="role2">The role2.</param>
        public JwtAuthorizeAttribute(object role1, object role2) : this()
        {
            this.Roles = string.Join(",", role1, role2);
        }

        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        /// <param name="role1">The role1.</param>
        /// <param name="role2">The role2.</param>
        /// <param name="role3">The role3.</param>
        public JwtAuthorizeAttribute(object role1, object role2, object role3) : this()
        {
            this.Roles = string.Join(",", role1, role2, role3);
        }
        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        /// <param name="roles">The roles.</param>
        public JwtAuthorizeAttribute(params object[] roles) : this()
        {
            this.Roles = string.Join(",", roles);
        }

        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        /// <param name="role">The role.</param>
        public JwtAuthorizeAttribute(string role) : this()
        {
            this.Roles = role;
        }

        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        /// <param name="role1">The role1.</param>
        /// <param name="role2">The role2.</param>
        public JwtAuthorizeAttribute(string role1, string role2) : this()
        {
            this.Roles = string.Join(",", role1, role2);
        }

        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        /// <param name="role1">The role1.</param>
        /// <param name="role2">The role2.</param>
        /// <param name="role3">The role3.</param>
        public JwtAuthorizeAttribute(string role1, string role2, string role3) : this()
        {
            this.Roles = string.Join(",", role1, role2, role3);
        }

        /// <summary>
        /// 初始化一个新的<see cref="JwtAuthorizeAttribute" />实例
        /// </summary>
        /// <param name="roles">The roles.</param>
        public JwtAuthorizeAttribute(params string[] roles) : this()
        {
            this.Roles = string.Join(",", roles);
        }

    }
}
