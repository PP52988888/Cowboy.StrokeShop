// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-21-2019
//
// Last Modified By : pan
// Last Modified On : 01-21-2019
// ***********************************************************************
// <copyright file="StrokeShareRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 行程分享请求数据
    /// </summary>
    [Serializable]
    public class StrokeShareRequest
    {
        /// <summary>
        ///行程编号
        /// </summary>
        /// <value>The stroke identifier.</value>
        public long StrokeId { get; set; }

        /// <summary>
        /// 分享连接地址
        /// </summary>
        /// <value>The shareurl.</value>
        public string ShareUrl { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        /// <value>The jump URL.</value>
        public string JumpUrl { get; set; }

    }
}
