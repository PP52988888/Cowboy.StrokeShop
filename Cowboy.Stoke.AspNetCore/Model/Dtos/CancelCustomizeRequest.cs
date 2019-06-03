// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="CancelCustomizeRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 取消私人定制订单请求
    /// </summary>
    public class CancelCustomizeRequest
    {
        /// <summary>
        /// 私人定制编号
        /// </summary>
        /// <value>The customize identifier.</value>
        public long CustomizeId { get; set; }
    }
}
