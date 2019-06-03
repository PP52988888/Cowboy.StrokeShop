// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-20-2019
//
// Last Modified By : pan
// Last Modified On : 03-20-2019
// ***********************************************************************
// <copyright file="GetOrderDetailsResponse.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.TravelShop.Model;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 获取订单的详细信息
    /// </summary>
    public class GetOrderDetailsResponse
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get;  set; }


        /// <summary>
        /// 订单号
        /// </summary>
        /// <value>The order no.</value>
        public string OrderNo { get;  set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <value>The order status.</value>
        public OrderStatus OrderStatus { get;  set; }


        /// <summary>
        /// 价格
        /// </summary>
        /// <value>The price.</value>
        public decimal Price { get;  set; }


        /// <summary>
        /// 行程图片
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get;  set; }


        /// <summary>
        /// 行程图片缩略图
        /// </summary>
        /// <value>The image thumb.</value>
        public string ImageThumb { get;  set; }


        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get;  set; }


        /// <summary>
        /// 标题
        /// </summary>
        /// <value>The title.</value>
        public string Title { get;  set; }


        /// <summary>
        ///行程编号
        /// </summary>
        /// <value>The stroke identifier.</value>
        public long StrokeId { get;  set; }


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
        /// 游玩天数
        /// </summary>
        /// <value>The number day.</value>
        public sbyte NumberDay { get;  set; }
    }
}
