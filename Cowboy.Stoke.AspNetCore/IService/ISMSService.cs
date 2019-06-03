// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="ISMSService.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.Stoke.AspNetCore.Model;

namespace Cowboy.Stroke.AspNetCore.IService
{
    /// <summary>
    /// Interface ISMSService
    /// </summary>
    public interface ISMSService
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        Task<Response<bool>> SendIdentifyingCodeAsync(string phone, string prefix);

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <param name="code">The code.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        Response<bool> ValidateIdentifyingCodeAsync(string phone, string code, string prefix);
    }
}
