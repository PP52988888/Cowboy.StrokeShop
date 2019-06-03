// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="StrokeSerachRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    ///  行程搜索请求
    /// </summary>
    public class StrokeSearchRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        /// <value>The search key.</value>
        public string SearchKey { get; set; }
    }
}
