// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-07-2019
//
// Last Modified By : pan
// Last Modified On : 03-07-2019
// ***********************************************************************
// <copyright file="GetStrokeHotResponse.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 获取行程
    /// </summary>
    public class GetStrokeHotResponse
    {
        /// <summary>
        ///  行程编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get;  set; }

        /// <summary>
        /// 行程标题
        /// </summary>
        /// <value>The title.</value>
        public string Title { get;  set; }


        /// <summary>
        /// 城市 
        /// </summary>
        /// <value>The city.</value>
        public string City { get;  set; }


        /// <summary>
        /// 目的地
        /// </summary>
        /// <value>The destination.</value>
        public string Destination { get;  set; }


        /// <summary>
        /// 图片地址
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get;  set; }


        /// <summary>
        /// 封面地址
        /// </summary>
        /// <value>The image thumb.</value>
        public string ImageThumb { get;  set; }


        /// <summary>
        /// 标准价格
        /// </summary>
        /// <value>The price.</value>
        public decimal Price { get;  set; }


        /// <summary>
        /// 优惠价格
        /// </summary>
        /// <value>The special price.</value>
        public decimal SpecialPrice { get;  set; }
    }
}
