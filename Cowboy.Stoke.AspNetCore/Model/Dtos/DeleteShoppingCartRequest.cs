// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-18-2019
// ***********************************************************************
// <copyright file="DeleteShoppingCartRequest.cs" company="Cowboy.Stoke.AspNetCore">
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
    /// 删除购物车
    /// </summary>
    public class DeleteShoppingCartRequest
    {
        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <value>The shopping cart identifier.</value>
        public long ShoppingCartId { get; set; }
    }
}
