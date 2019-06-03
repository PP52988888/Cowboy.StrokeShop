// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-21-2019
//
// Last Modified By : pan
// Last Modified On : 01-23-2019
// ***********************************************************************
// <copyright file="StrokeShareResponse.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 行程分享数据
    /// </summary>
    [Serializable]
    public class StrokeShareResponse
    {
        /// <summary>
        /// 微信公众号appId
        /// </summary>
        /// <value>The application identifier.</value>
        public string AppId { get;  set; }


        /// <summary>
        /// 随机数
        /// </summary>
        /// <value>The nonce string.</value>
        public string NonceStr { get;  set; }


        /// <summary>
        /// 签名
        /// </summary>
        /// <value>The signature.</value>
        public string Signature { get;  set; }


        /// <summary>
        /// 时间戳
        /// </summary>
        /// <value>The timestamp.</value>
        public string Timestamp { get;  set; }


        /// <summary>
        /// 分享封面地址
        /// </summary>
        /// <value>The share image URL.</value>
        public string ShareImageUrl { get;  set; }

        /// <summary>
        ///  分享地址
        /// </summary>
        /// <value>The share URL.</value>
        public string ShareUrl { get;  set; }
    }
}
