// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 03-20-2019
// ***********************************************************************
// <copyright file="GetOrderingResponse.cs" company="Cowboy.Stoke.AspNetCore">
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
    /// 获取订单信息
    /// </summary>
    [Serializable]
    public class GetOrderingResponse
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }


        /// <summary>
        /// 订单号
        /// </summary>
        /// <value>The order no.</value>
        public string OrderNo { get; set; }

        /// <summary>
        /// 行程编号
        /// </summary>
        /// <value>The stoke identifier.</value>
        public long StrokeId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <value>The order status.</value>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        /// <value>The price.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }

        /// <summary>
        /// 封面地址
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 封面缩略图地址
        /// </summary>
        /// <value>The image thumb.</value>
        public string ImageThumb { get; set; }


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
