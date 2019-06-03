// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-18-2019
// ***********************************************************************
// <copyright file="ShoppingCartService.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Cowboy.TravelShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.IService.Implement
{
    /// <summary>
    /// 购物车服务
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.IService.IShoppingCartService" />
    public class ShoppingCartService:IShoppingCartService
    {
        #region Field
        /// <summary>
        /// The stoke context
        /// </summary>
        private readonly StrokeContext strokeContext;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The context accessor
        /// </summary>
        private readonly IHttpContextAccessor contextAccessor;
        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartService"/> class.
        /// </summary>
        /// <param name="strokeContext"></param>
        /// <param name="logger"></param>
        /// <param name="contextAccessor"></param>
        public ShoppingCartService(StrokeContext strokeContext, ILogger<IShoppingCartService> logger,IHttpContextAccessor contextAccessor)
        {
            this.strokeContext = strokeContext;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
        }

        #endregion


        #region Methods

        /// <summary>
        /// 行程添加到购物车
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response> AddtoShoppingCart(AddtoShoppingCartRequest request)
        {
            var userId = contextAccessor.UserId();

            //如果存在购物车编号
            if (request.ShoppingCartId.HasValue&&request.ShoppingCartId.Value>0)
            {
                //修改购物车数量
                var shoppingCartInfo = await this.strokeContext.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == request.ShoppingCartId.Value);
                if (shoppingCartInfo == null)
                {
                    throw new Exception("获取购物车信息");
                }
                shoppingCartInfo.Quantity = request.Quantity;
                this.strokeContext.ShoppingCarts.Update(shoppingCartInfo);
                this.strokeContext.SaveChanges();
            }
            else {
                var strokeInfo = await this.strokeContext.Strokes.FirstOrDefaultAsync(x => x.Id == request.StrokeId);
                if (strokeInfo == null)
                {
                    throw new Exception("获取行程信息失败");
                }
                var shoppingCart = await this.strokeContext.ShoppingCarts.FirstOrDefaultAsync(x => x.StrokeId == request.StrokeId && x.UserId == userId);
                if (shoppingCart!=null)
                {
                    throw new Exception("请传递正确的参数");
                }

                this.strokeContext.ShoppingCarts.Add(new ShoppingCart
                {
                    StrokeId = request.StrokeId,
                    UserId = userId,
                    CreateBy = userId,
                    Quantity = request.Quantity,
                    Remark = request.Remark
                });
                this.strokeContext.SaveChanges();
            }
           
            return Response.Success();
        } 


        /// <summary>
        /// 删除购物车数据
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response> DeleteShoppingCart(DeleteShoppingCartRequest request)
        {
            var userId = contextAccessor.UserId();
            var shoppingCart = await this.strokeContext.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == request.ShoppingCartId && x.UserId == userId);
            if (shoppingCart==null)
            {
                throw new Exception("获取购物车信息失败");
            }
            this.strokeContext.Remove(shoppingCart);
            this.strokeContext.SaveChanges();
            return Response.Success();

        }


        /// <summary>
        /// 获取购物车数据信息
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;Response&lt;GetShoppingCartInfoResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<GetShoppingCartInfoResponse[]>> GetShoppingCartInfo()
        {
            var userId = contextAccessor.UserId();
            var tempRows =await  (from shoppingCart in this.strokeContext.ShoppingCarts
                                  join stroke in  this.strokeContext.Strokes on shoppingCart.StrokeId equals stroke.Id
                            where shoppingCart.UserId == userId
                            select new GetShoppingCartInfoResponse
                            {
                                Id = shoppingCart.Id,
                                UserId = shoppingCart.UserId,
                                Quantity = shoppingCart.Quantity,
                                Remark = shoppingCart.Remark,
                                StokeId = stroke.Id,
                                Title = stroke.Title,
                                ImageUrl = stroke.ImageUrl,
                                ImageThumb = stroke.ImageThumb,
                            }).ToArrayAsync();
                
            return new Response<GetShoppingCartInfoResponse[]>
            {
                Data = tempRows
            };
        }

        #endregion
    }
}
