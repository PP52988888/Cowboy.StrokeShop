// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-18-2019
// ***********************************************************************
// <copyright file="IShoppingCartService.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Model.Dtos;

namespace Cowboy.Stoke.AspNetCore.IService
{
    /// <summary>
    ///购物车服务
    /// </summary>
    public interface IShoppingCartService
    {
        #region Methods

        /// <summary>
        /// 行程添加到购物车
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        Task<Response> AddtoShoppingCart(AddtoShoppingCartRequest request);

        /// <summary>
        /// 删除购物车数据
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        Task<Response> DeleteShoppingCart(DeleteShoppingCartRequest request);

        /// <summary>
        /// 获取购物车数据信息
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;Response&lt;GetShoppingCartInfoResponse&gt;&gt;.</returns>
        Task<Response<GetShoppingCartInfoResponse[]>> GetShoppingCartInfo();
        #endregion
    }
}
