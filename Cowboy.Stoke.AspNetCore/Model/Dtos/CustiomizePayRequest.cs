// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="CustiomizePayRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// Class CustiomizePayRequest.
    /// </summary>
    public class CustiomizePayRequest
    {
        /// <summary>
        /// 定制行程编号
        /// </summary>
        /// <value>The customizes identifier.</value>
        public long CustomizesId { get; set; }

        /// <summary>
        /// 成功返回地址
        /// </summary>
        /// <value>The success URL.</value>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// 失败返回地址
        /// </summary>
        /// <value>The faild URL.</value>
        public string FaildUrl { get; set; }
    }
}
