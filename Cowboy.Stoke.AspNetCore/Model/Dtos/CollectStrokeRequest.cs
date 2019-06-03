// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-19-2019
//
// Last Modified By : pan
// Last Modified On : 01-19-2019
// ***********************************************************************
// <copyright file="CollectStokeRequest.cs" company="Cowboy.Stoke.AspNetCore">
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
    ///  收藏行程请求数据
    /// </summary>
    [Serializable]
    public class CollectStrokeRequest
    {
        /// <summary>
        /// 行程编号
        /// </summary>
        /// <value>The stoke identifier.</value>
        public long StrokeId { get; set; }

        /// <summary>
        /// 收藏编号
        /// </summary>
        /// <value>The collect identifier.</value>
        public long CollectId { get; set; }
    }
}
