// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="GetBannerResponse.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.TravelShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 获取首页banner数据
    /// </summary>
    [Serializable]
    public class GetBannerResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// 标题 
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }


        /// <summary>
        /// 行程编号 
        /// </summary>
        /// <value>The stroke identifier.</value>
        public long StrokeId { get; set; }


        /// <summary>
        /// 广告类型
        /// </summary>
        /// <value>The type.</value>
        public ADType Type { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        /// <value>The link.</value>
        public string Link { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        /// <value>The city identifier.</value>
        public long CityId { get; set; }

        /// <summary>
        /// Banner内容简介
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 图片缩略图
        /// </summary>
        /// <value>The image thumb.</value>
        public string ImageThumb { get; set; }


    }
}
