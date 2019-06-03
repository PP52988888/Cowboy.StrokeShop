// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-21-2019
// ***********************************************************************
// <copyright file="IStrokeService.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stoke.AspNetCore.Model.Dtos;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Microsoft.AspNetCore.Http;

namespace Cowboy.Stoke.AspNetCore.IService
{
    /// <summary>
    /// Interface IStrokeService
    /// </summary>
    public interface IStrokeService
    {
        /// <summary>
        /// 获取Banner数据
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetBannerResponse&gt;&gt;.</returns>
        Task<Response<GetBannerResponse[]>> GetBanner(GetBannerRequest request);

        /// <summary>
        /// 获取全部城市信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetCityResponse[]&gt;&gt;.</returns>
        Task<Response<GetCityResponse[]>> GetCityInfo(GetCityRequest request);

        /// <summary>
        /// 获取推荐行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetRecommendStokeResponse&gt;&gt;.</returns>
        Task<Response<GetRecommendStrokeResponse[]>> GetRecommendStroke(GetRecommendStrokeRequest request);


        /// <summary>
        /// 获取行程详情信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId"></param>
        /// <returns>Response&lt;GetStokeDetailResponse&gt;.</returns>
        Task<Response<GetStrokeDetailResponse>>  GetStrokeDetail(GetStrokeDetailRequest request);

        /// <summary>
        /// 收藏行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        Task<Response<CollectStrokeResponse>> CollectStroke(CollectStrokeRequest request);

        /// <summary>
        /// 获取所有的收藏行程
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;Response&lt;GetCollectStokeResponse&gt;&gt;.</returns>
        Task<Response<GetCollectStrokeResponse[]>> GetCollectStroke();



        /// <summary>
        /// 行程分享
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;Response&lt;StrokeShareResponse&gt;&gt;.</returns>
        Task<Response<StrokeShareResponse>> StrokeShare(StrokeShareRequest request);


        /// <summary>
        ///  行程搜索
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;StrokeSearchResponse&gt;&gt;.</returns>
        Task<Response<StrokeSearchResponse[]>> StrokeSearch(StrokeSearchRequest request);

        /// <summary>
        ///获取国际，国内，省内热门行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetStrokeHotResponse&gt;&gt;.</returns>
        Task<Response<GetStrokeHotResponse[]>> GetStrokeHot(GetStrokeHotRequest request);


        /// <summary>
        ///添加私人定制行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;AddCustomizeResponse&gt;&gt;.</returns>
        Task<Response<AddCustomizeResponse>> AddCustomize(AddCustomizeRequest request);


        /// <summary>
        /// 获取全部私人定制行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetCustomizeResponse&gt;&gt;.</returns>
        Task<Response<GetCustomizeResponse[]>> GetCustomize(GetCustomizeRequest request);

        ///// <summary>                      行程定制
        ///// 取消定制订单   
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //Task<Response> CancelCustomize(CancelCustomizeRequest request);

        ///// <summary>
        ///// 确认定制行程状态完成
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //Task<Response> CompleteCustomize(CompleteCustomizeRequest request);

        ///// <summary>
        ///// 定制行程支付
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //Task<Response<PlaceAnOrderInfoResponse>> CustiomizePay(CustiomizePayRequest request);

        /// <summary>
        ///  行程支付
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;PlaceAnOrderInfoResponse&gt;&gt;.</returns>
        Task<Response<PlaceAnOrderInfoResponse>> StrokePay(StrokePayRequest request);
    }
}
