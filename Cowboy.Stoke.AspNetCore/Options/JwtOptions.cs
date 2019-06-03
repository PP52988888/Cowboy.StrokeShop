// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="JwtOptions.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model
{
    /// <summary>
    /// 授权
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is HTTPS.
        /// </summary>
        /// <value><c>true</c> if this instance is HTTPS; otherwise, <c>false</c>.</value>
        public bool IsHttps { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>The issuer.</value>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>The audience.</value>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the expire seconds.
        /// </summary>
        /// <value>The expire seconds.</value>
        public long ExpireSeconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include error details].
        /// </summary>
        /// <value><c>true</c> if [include error details]; otherwise, <c>false</c>.</value>
        public bool IncludeErrorDetails { get; set; }
    }
}
