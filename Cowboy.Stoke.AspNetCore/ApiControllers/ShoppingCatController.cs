// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-18-2019
// ***********************************************************************
// <copyright file="ShoppingCatController.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.Controllers;
using Cowboy.Stoke.AspNetCore.IService;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.ApiControllers
{
    /// <summary>
    ///购物车管理
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.Controllers.ApiController" />
    public class ShoppingCatController:ApiController
    {
        #region Field
        /// <summary>
        /// The shopping cart service
        /// </summary>
        private readonly IShoppingCartService shoppingCartService;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCatController"/> class.
        /// </summary>
        /// <param name="shoppingCartService">The shopping cart service.</param>
        public ShoppingCatController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }
        #endregion

        #region Methods

        ///// <summary>
        ///// 行程添加到购物车
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //[HttpPost]
        //[JwtAuthorize]
        //public Task<Response> AddtoShoppingCart([FromBody] AddtoShoppingCartRequest request ) {
        //    return shoppingCartService.AddtoShoppingCart(request);
        //}

        ///// <summary>
        ///// 删除购物车数据
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //[HttpPost]
        //[JwtAuthorize]
        //public Task<Response> DeleteShoppingCart([FromBody] DeleteShoppingCartRequest request) {
        //    return shoppingCartService.DeleteShoppingCart(request);
        //}


        ///// <summary>
        ///// 获取用户购物车数据 
        ///// </summary>
        ///// <returns>Task&lt;Response&lt;GetShoppingCartInfoResponse&gt;&gt;.</returns>
        //[HttpPost]
        //[JwtAuthorize]
        //public Task<Response<GetShoppingCartInfoResponse[]>> GetShoppingCartInfo() {
        //    return shoppingCartService.GetShoppingCartInfo();
        //}

        #endregion
    }
}
