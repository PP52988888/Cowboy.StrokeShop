// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="GetCityRequest.cs" company="Cowboy.Stoke.AspNetCore">
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
    /// 获取城市信息请求
    /// </summary>
    [Serializable]
    public class GetCityRequest
    {
        /// <summary>
        /// 是否为热门城市
        /// </summary>
        /// <value><c>null</c> if [is hot] contains no value, <c>true</c> if [is hot]; otherwise, <c>false</c>.</value>
        public bool? IsHot { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        /// <value>The search key.</value>
        public string SearchKey { get; set; }
    }
}
