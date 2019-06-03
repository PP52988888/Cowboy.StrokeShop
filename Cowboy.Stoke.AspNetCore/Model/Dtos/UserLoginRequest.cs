// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 01-16-2019
// ***********************************************************************
// <copyright file="UserLoginRequest.cs" company="Cowboy.Stoke.AspNetCore">
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
    /// 用户登陆请求数据
    /// </summary>
    [Serializable]
    public class UserLoginRequest
    {
        /// <summary>
        /// 登陆手机号码
        /// </summary>
        /// <value>The c phone.</value>
        public string CPhone { get; set; }


        /// <summary>
        /// 登陆密码
        /// </summary>
        /// <value>The pass word.</value>
        public string PassWord { get; set; }
    }
}
