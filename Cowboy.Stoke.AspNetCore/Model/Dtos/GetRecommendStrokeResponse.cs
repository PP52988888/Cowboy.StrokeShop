// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="GetRecommendStokeResponse.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 获取推荐行程信息
    /// </summary>
    [Serializable]
    public class GetRecommendStrokeResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// 行程编号
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// 封面图片地址
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get; set; }


        /// <summary>
        /// 封面图片缩略图
        /// </summary>
        /// <value>The image thumb.</value>
        public string ImageThumb { get; set; }

        /// <summary>
        /// 出发城市
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        /// <value>The destination.</value>
        public string Destination { get; set; }


    }
}
