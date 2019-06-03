// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-23-2019
//
// Last Modified By : pan
// Last Modified On : 01-23-2019
// ***********************************************************************
// <copyright file="PaymentController.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.Controllers;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.ApiControllers
{
    /// <summary>
    /// 支付回调
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.Controllers.ApiController" />
    public class PaymentController:ApiController
    {
        #region Filed
        /// <summary>
        /// The payment service
        /// </summary>
        private readonly IPaymentService paymentService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        /// <param name="paymentService">The payment service.</param>
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        #endregion

        /// <summary>
        /// 微信支付回调
        /// </summary>
        /// <returns>Task&lt;Response&gt;.</returns>
        [HttpPost]
        [HttpGet]
        public Task<Response> WeChatPayNotify() {
            return this.paymentService.WeChatPayNotify();
        }
    }
}
