// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="GetCityResponse.cs" company="Cowboy.Stoke.AspNetCore">
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
    /// 获取城市信息数据
    /// </summary>
    public class GetCityResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// 是否开放 
        /// </summary>
        /// <value><c>true</c> if this instance is open; otherwise, <c>false</c>.</value>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        /// <value><c>true</c> if this instance is hot; otherwise, <c>false</c>.</value>
        public bool IsHot { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// 拼音简写
        /// </summary>
        /// <value>The simple spell.</value>
        public string SimpleSpell { get;  set; }

        /// <summary>
        /// 拼音全写
        /// </summary>
        /// <value>The full spell.</value>
        public string FullSpell { get;  set; }
    }
}
