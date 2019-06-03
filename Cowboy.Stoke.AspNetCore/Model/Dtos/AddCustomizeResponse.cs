// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="AddCustomizeResponse.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 添加私人定制行程
    /// </summary>
    public class AddCustomizeResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get;  set; }


        /// <summary>
        /// 定制行程订单号
        /// </summary>
        /// <value>The customize no.</value>
        public string CustomizeNo { get;  set; }
    }
}
