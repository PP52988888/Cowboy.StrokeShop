// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-06-2019
//
// Last Modified By : pan
// Last Modified On : 03-06-2019
// ***********************************************************************
// <copyright file="AddCustomizeRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 添加行程定制请求
    /// </summary>
    public class AddCustomizeRequest
    {
        /// <summary>
        /// 出发城市
        /// </summary>
        /// <value>The strat city.</value>
        public string StratCity { get; set; }

        /// <summary>
        /// 目的地 
        /// </summary>
        /// <value>The destination.</value>
        public string Destination { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        /// <value>The cphone.</value>
        public string Cphone { get; set; }

        /// <summary>
        /// 预定人
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// 成人人数
        /// </summary>
        /// <value>The adult.</value>
        public int Adult { get; set; }

        /// <summary>
        /// 定制类型
        /// </summary>
        /// <value>The type.</value>
        public int Type { get; set; }
        /// <summary>
        /// 儿童人数
        /// </summary>
        /// <value>The children.</value>
        public int Children { get; set; }

        /// <summary>
        ///  人均预算
        /// </summary>
        /// <value>The budget.</value>
        public decimal Budget { get; set; }

        /// <summary>
        /// 游玩天数
        /// </summary>
        /// <value>The number day.</value>
        public int NumberDay { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        /// <value>The start date.</value>
        public DateTime?  StartDate { get; set; }
    }
}
