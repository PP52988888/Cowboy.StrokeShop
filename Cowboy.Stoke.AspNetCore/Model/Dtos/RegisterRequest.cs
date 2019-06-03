// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 01-16-2019
// ***********************************************************************
// <copyright file="RegisterRequest.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 注册请求数据
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        /// <value>The c phone.</value>
        public string CPhone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }
    }
}
