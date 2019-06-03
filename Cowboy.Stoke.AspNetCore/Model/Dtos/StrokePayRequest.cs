// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="StrokePayRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 行程支付请求
    /// </summary>
    public class StrokePayRequest
    {
        /// <summary>
        /// 行程编号
        /// </summary>
        /// <value>The stroke identifier.</value>
        public long StrokeId { get; set; }


        /// <summary>
        /// 分享用户源  编号
        /// </summary>
        /// <value>The share code.</value>
        public string ShareCode { get; set; }

        /// <summary>
        /// 用户来源
        /// </summary>
        /// <value>From.</value>
        public string From { get; set; }

        /// <summary>
        /// 支付成功后的返回地址
        /// </summary>
        /// <value>The success URL.</value>
        public string SuccessUrl { get;  set; }

        /// <summary>
        /// 支付失败后的返回地址
        /// </summary>
        /// <value>The faild URL.</value>
        public string FaildUrl { get;  set; }
    }
}
