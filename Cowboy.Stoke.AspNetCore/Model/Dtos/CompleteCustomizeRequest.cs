// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="CompleteCustomizeRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// Class CompleteCustomizeRequest.
    /// </summary>
    public class CompleteCustomizeRequest
    {
        /// <summary>
        /// 定制行程编号
        /// </summary>
        /// <value>The customize identifier.</value>
        public long CustomizeId { get;  set; }
    }
}
