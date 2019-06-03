// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-23-2019
//
// Last Modified By : pan
// Last Modified On : 01-23-2019
// ***********************************************************************
// <copyright file="IPaymentService.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// Interface IPaymentService
    /// </summary>
    public interface IPaymentService
    {

        /// <summary>
        /// 微信支付回调
        /// </summary>
        /// <returns>Task&lt;Response&gt;.</returns>
        Task<Response> WeChatPayNotify();
    }
}
