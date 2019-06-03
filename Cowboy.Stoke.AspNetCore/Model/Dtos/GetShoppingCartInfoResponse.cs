// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-18-2019
// ***********************************************************************
// <copyright file="GetShoppingCartInfoResponse.cs" company="Cowboy.Stoke.AspNetCore">
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
    /// 获取购物车数据信息
    /// </summary>
    [Serializable]
    public class GetShoppingCartInfoResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get;  set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        /// <value>The user identifier.</value>
        public long UserId { get;  set; }


        /// <summary>
        /// 行程编号 
        /// </summary>
        /// <value>The stoke identifier.</value>
        public long StokeId { get;  set; }


        /// <summary>
        /// 数量
        /// </summary>
        /// <value>The quantity.</value>
        public short Quantity { get;  set; }


        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get;  set; }

        /// <summary>
        ///  标题
        /// </summary>
        /// <value>The title.</value>
        public string Title { get;  set; }

        /// <summary>
        /// 封面图片地址
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get;  set; }

        /// <summary>
        /// 封面图片缩略图
        /// </summary>
        /// <value>The image thumb.</value>
        public string ImageThumb { get;  set; }
    }
}
