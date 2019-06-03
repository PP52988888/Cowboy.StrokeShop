// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="GetStokeDetailResponse.cs" company="Cowboy.Stoke.AspNetCore">
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
    /// 获取行程详情信息
    /// </summary>
    public class GetStrokeDetailResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get;  set; }

        /// <summary>
        ///  标题
        /// </summary>
        /// <value>The title.</value>
        public string Title { get;  set; }


        /// <summary>
        /// 详情
        ///</summary>
        /// <value>The detail.</value>
        public string Detail { get;  set; }


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


        /// <summary>
        /// 游玩天数
        /// </summary>
        /// <value>The \.</value>
        public sbyte  NumberDay { get;  set; }

        /// <summary>
        /// 行程收藏编号
        /// </summary>
        /// <value>The stroke identifier.</value>
        public long CollectId { get; set; }

        /// <summary>
        /// 行程是否收藏
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get; set; }

        /// <summary>
        ///  出发城市
        /// </summary>
        /// <value>The city.</value>
        public string City { get;  set; }


        /// <summary>
        /// 目的地
        /// </summary>
        /// <value>The destination.</value>
        public string Destination { get;  set; }


        /// <summary>
        /// 价格 
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
