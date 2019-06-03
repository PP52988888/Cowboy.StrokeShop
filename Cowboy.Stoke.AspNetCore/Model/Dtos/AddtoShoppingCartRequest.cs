// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-18-2019
// ***********************************************************************
// <copyright file="AddtoShoppingCartRequest.cs" company="Cowboy.Stoke.AspNetCore">
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
    /// 行程添加到购物车
    /// </summary>
    public class AddtoShoppingCartRequest
    {
        /// <summary>
        /// 购物车编号
        /// </summary>
        /// <value>The shopping cart identifier.</value>
        public long? ShoppingCartId { get; set; }

        /// <summary>
        /// 行程编号
        /// </summary>
        /// <value>The stoke identifier.</value>
        public long StrokeId { get; set; }

        /// <summary>
        /// 添加数量
        /// </summary>
        /// <value>The quantity.</value>
        public short Quantity { get; set; } = 1;


        /// <summary>
        /// 备注信息
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }
    }
}
