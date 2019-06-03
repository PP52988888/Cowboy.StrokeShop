// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-08-2019
//
// Last Modified By : pan
// Last Modified On : 03-08-2019
// ***********************************************************************
// <copyright file="WeChatLoginByRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 通过openId进行登陆请求
    /// </summary>
    public class WeChatLoginByRequest
    {
        /// <summary>
        /// 微信openId
        /// </summary>
        /// <value>The open identifier.</value>
        public string OpenId { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        /// <value>The nickname.</value>
        public string Nickname { get; set; }

        /// <summary>
        ///  性别 
        /// </summary>
        /// <value>The sex.</value>
        public long Sex { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// 微信头像地址
        /// </summary>
        /// <value>The headimgurl.</value>
        public string Headimgurl { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        /// <value>The province.</value>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }
    }
}
