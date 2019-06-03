// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="GetCustomizeResponse.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 获取全部的定制信息
    /// </summary>
    public class GetCustomizeResponse
    {
        /// <summary>
        /// 定制行程编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get;  set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        /// <value>The user identifier.</value>
        public long UserId { get;  set; }

        /// <summary>
        /// 开始城市
        /// </summary>
        /// <value>The start city.</value>
        public string StartCity { get;  set; }

        /// <summary>
        /// 类型
        /// </summary>
        /// <value>The type.</value>
        public int Type { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        /// <value>The destination.</value>
        public string Destination { get;  set; }

        /// <summary>
        /// 手机号
        /// </summary>
        /// <value>The cphone.</value>
        public string Cphone { get;  set; }

        /// <summary>
        /// 定制状态
        /// </summary>
        /// <value>The status.</value>
        public CustomizeStatus Status { get;  set; }

        /// <summary>
        /// 定制行程订单号
        /// </summary>
        /// <value>The customize no.</value>
        public string CustomizeNo { get;  set; }

        /// <summary>
        /// 成人人数
        /// </summary>
        /// <value>The adult.</value>
        public int Adult { get;  set; }

        /// <summary>
        /// 儿童人数
        /// </summary>
        /// <value>The children.</value>
        public int Children { get;  set; }

        /// <summary>
        /// 预约人
        /// </summary>
        /// <value>The name.</value>
        public string Name { get;  set; }

        /// <summary>
        /// 人均预算
        /// </summary>
        /// <value>The budget.</value>
        public decimal Budget { get;  set; }

        /// <summary>
        /// 游玩天数
        /// </summary>
        /// <value>The number day.</value>
        public int NumberDay { get;  set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get;  set; }

        /// <summary>
        /// 备注信息 
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get;  set; }
    }
}
