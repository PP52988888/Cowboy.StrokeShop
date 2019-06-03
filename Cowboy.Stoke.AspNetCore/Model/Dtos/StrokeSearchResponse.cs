// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="StrokeSearchResponse.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// Class StrokeSearchResponse.
    /// </summary>
    public class StrokeSearchResponse
    {
        /// <summary>
        /// 行程编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get;  set; }


        /// <summary>
        /// 标题
        /// </summary>
        /// <value>The title.</value>
        public string Title { get;  set; }


        /// <summary>
        /// 出发城市
        /// </summary>
        /// <value>The city.</value>
        public string City { get;  set; }

        /// <summary>
        /// 目的地
        /// </summary>
        /// <value>The destination.</value>
        public string Destination { get;  set; }


        /// <summary>
        /// 封面地址
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get;  set; }


        /// <summary>
        /// 封面缩略图 
        /// </summary>
        /// <value>The image thumb.</value>
        public string ImageThumb { get;  set; }
    }
}
