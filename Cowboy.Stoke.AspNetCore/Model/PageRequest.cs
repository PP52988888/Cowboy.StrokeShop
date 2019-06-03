// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="PageRequest.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.Model
{
    /// <summary>
    /// 分页请求
    /// </summary>
    [Serializable]
    public class PageRequest
    {
        /// <summary>
        /// 分页页码
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页尺度
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// 包含全部项数
        /// </summary>
        /// <value><c>true</c> if [include total size]; otherwise, <c>false</c>.</value>
        public bool IncludeTotalSize { get; set; }
    }
}
