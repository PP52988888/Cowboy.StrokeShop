// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="StokeController.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.Controllers;
using Cowboy.Stoke.AspNetCore.IService;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stoke.AspNetCore.Model.Dtos;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.ApiControllers
{
    /// <summary>
    ///  行程管理
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.Controllers.ApiController" />
    public class StrokeController:ApiController
    {
        #region Field
        /// <summary>
        /// The stoke service
        /// </summary>
        private readonly IStrokeService strokeService;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeController"/> class.
        /// </summary>
        /// <param name="strokeService"></param>
        public StrokeController(IStrokeService strokeService)
        {
            this.strokeService = strokeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 获取首页Banner数据
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetBannerResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetBannerResponse[]>> GetBanner([FromBody] GetBannerRequest request)
        {
            return this.strokeService.GetBanner(request);
        }

        /// <summary>
        /// 获取全部城市信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetCityResponse[]&gt;&gt;.</returns>
        [HttpPost]
        public Task<Response<GetCityResponse[]>> GetCityInfo([FromBody] GetCityRequest request)
        {
            return this.strokeService.GetCityInfo(request);
        }

        /// <summary>
        /// 首页获取推荐行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetRecommendStokeResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetRecommendStrokeResponse[]>> GetRecommendStroke([FromBody] GetRecommendStrokeRequest request)
        {
            return this.strokeService.GetRecommendStroke(request);
        }


        /// <summary>
        /// 获取国际，国内，省内热门行程
        /// </summary>
        /// <param name="request">The get stroke hot request.</param>
        /// <returns>Task&lt;Response&lt;GetStrokeHotResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetStrokeHotResponse[]>> GetStrokeHot([FromBody] GetStrokeHotRequest request) {
            return this.strokeService.GetStrokeHot(request);
        }

        /// <summary>
        /// 获取行程详情信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response&lt;GetStokeDetailResponse&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetStrokeDetailResponse>>  GetStrokeDetail([FromBody]GetStrokeDetailRequest request) {
            return this.strokeService.GetStrokeDetail(request);
        }

        /// <summary>
        /// 收藏行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<CollectStrokeResponse>> CollectStroke([FromBody] CollectStrokeRequest request) {
            return this.strokeService.CollectStroke(request);
        }

        /// <summary>
        /// 获取所有的收藏行程
        /// </summary>
        /// <returns>Task&lt;Response&lt;GetCollectStokeResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetCollectStrokeResponse[]>> GetCollectStroke( ) {
            return this.strokeService.GetCollectStroke();
        }



        /// <summary>
        /// 行程分享
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;StrokeShareResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<StrokeShareResponse>> StrokeShare([FromBody] StrokeShareRequest request) {
            return this.strokeService.StrokeShare(request);
        }


        /// <summary>
        /// 行程搜索
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;StrokeSearchResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<StrokeSearchResponse[]>> StrokeSearch([FromBody] StrokeSearchRequest request) {
            return this.strokeService.StrokeSearch(request);
        }

        /// <summary>
        /// 添加私人定制行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;AddCustomizeResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<AddCustomizeResponse>> AddCustomize([FromBody] AddCustomizeRequest request) {
            return this.strokeService.AddCustomize(request);
        }

        /// <summary>
        /// 获取全部私人定制行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetCustomizeResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetCustomizeResponse[]>> GetCustomize([FromBody] GetCustomizeRequest request) {
            return this.strokeService.GetCustomize(request);
        }

        ///// <summary>      行程定制
        ///// 取消定制订单 
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //[HttpPost]
        //[JwtAuthorize]
        //public Task<Response> CancelCustomize([FromBody]  CancelCustomizeRequest request) {
        //    return this.strokeService.CancelCustomize(request);
        //}


        ///// <summary>
        /////  确认定制行程状态完成
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //[HttpPost]
        //[JwtAuthorize]
        //public Task<Response> CompleteCustomize([FromBody] CompleteCustomizeRequest request) {
        //    return this.strokeService.CompleteCustomize(request);
        //}

        ///// <summary>
        ///// 定制行程支付
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns>System.Threading.Tasks.Task&lt;Cowboy.Stoke.AspNetCore.Model.Response&gt;.</returns>
        //[HttpPost]
        //[JwtAuthorize]
        //public Task<Response<PlaceAnOrderInfoResponse>> CustiomizePay([FromBody] CustiomizePayRequest request ){
        //    return this.strokeService.CustiomizePay(request);
        //}


        /// <summary>
        /// 行程支付 
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;PlaceAnOrderInfoResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<PlaceAnOrderInfoResponse>> StrokePay([FromBody] StrokePayRequest request) {
            return this.strokeService.StrokePay(request);
        }
       
        #endregion

    }
}
