// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-22-2019
//
// Last Modified By : pan
// Last Modified On : 01-22-2019
// ***********************************************************************
// <copyright file="WeChatOptions.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model
{
    /// <summary>
    /// 微信支付参数
    /// </summary>
    public class WeChatOptions
    {
        /// <summary>
        /// 公众账号appId
        /// </summary>
        /// <value>The application identifier.</value>
        public string AppId { get; set; }

        /// <summary>
        /// 登陆回调地址
        /// </summary>
        /// <value>The redirect URL.</value>
        public string Redirect_Url { get; set; }

        /// <summary>
        /// 微信支付商户号
        /// </summary>
        /// <value>The MCH identifier.</value>
        public string MchId { get; set; }

        /// <summary>
        /// 微信公众号应用密钥
        /// </summary>
        /// <value>The application secret.</value>
        public string AppSecret { get; set; }

        /// <summary>
        /// 商户支付密钥key
        /// </summary>
        /// <value>The API key.</value>
        public string ApiKey { get; set; }

        /// <summary>
        /// 证书地址
        /// </summary>
        /// <value>The certificate address.</value>
        public string CertificateAddress { get; set; }


        /// <summary>
        /// 服务器主机地址 
        /// </summary>
        /// <value>The host URL.</value>
        public string HostUrl { get; set; }
    }
}
