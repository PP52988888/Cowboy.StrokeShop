// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-15-2019
//
// Last Modified By : pan
// Last Modified On : 03-15-2019
// ***********************************************************************
// <copyright file="GetUserQRCodeResponse.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 获取用户的分享二维码图片
    /// </summary>
    public class GetUserQRCodeResponse
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// 分享二维码图片
        /// </summary>
        /// <value>The share image.</value>
        public string ShareImage { get; set; }
    }
}
