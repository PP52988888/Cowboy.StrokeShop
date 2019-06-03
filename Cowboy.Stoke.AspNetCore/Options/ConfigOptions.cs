// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-15-2019
//
// Last Modified By : pan
// Last Modified On : 03-15-2019
// ***********************************************************************
// <copyright file="ConfigOptions.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Options
{
    /// <summary>
    /// 配置参数 
    /// </summary>
    public class ConfigOptions
    {
        /// <summary>
        /// 积分初始化数量
        /// </summary>
        /// <value>The initialize.</value>
        public long Initialize { get; set; }

        /// <summary>
        /// 每次下单后的增加数量
        /// </summary>
        /// <value>The increment.</value>
        public long Increment { get; set; }
    }
}
