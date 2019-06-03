// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-22-2019
//
// Last Modified By : pan
// Last Modified On : 01-22-2019
// ***********************************************************************
// <copyright file="WeChatPayParameter.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Views.WeChat
{
    /// <summary>
    /// 微信支付调起参数
    /// </summary>
    public class WeChatPayParameter
    {
        /// <summary>
        /// 微信应用AppId 
        /// </summary>
        /// <value>The application identifier.</value>
        public string AppId { get; set; }

        /// <summary>
        /// 微信商户号
        /// </summary>
        /// <value>The MCH identifier.</value>
        public string MchId { get; set; }

        /// <summary>
        /// 微信随机数 
        /// </summary>
        /// <value>The nonce string.</value>
        public string NonceStr { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        /// <value>The time stamp.</value>
        public string TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the package.
        /// </summary>
        /// <value>The package.</value>
        public string Package { get; set; }

        /// <summary>
        /// 支付签名 
        /// </summary>
        /// <value>The pay sign.</value>
        public string PaySign { get; set; }

        /// <summary>
        /// 支付成功后的返回参数 
        /// </summary>
        /// <value>The success URL.</value>
        public string SuccessUrl { get; set; }

        /// <summary>
        ///  支付失败后 
        /// </summary>
        /// <value>The faild URL.</value>
        public string FailedUrl { get; set; }
    }
}
