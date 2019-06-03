// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="StokeService.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.ExtensionMethods;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stoke.AspNetCore.Model.Dtos;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Cowboy.TravelShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cowboy.Stoke.AspNetCore.IService.Implement
{
    /// <summary>
    /// 行程服务
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.IService.IStrokeService" />
    public class StrokeService:IStrokeService
    {
        #region Field
        /// <summary>
        /// 行程数据上下文
        /// </summary>
        private readonly StrokeContext strokeContext;

        /// <summary>
        /// 微信支付参数
        /// </summary>
        private readonly WeChatOptions weChatOptions;

        /// <summary>
        ///  日志
        /// </summary>
        /// <value>The logger.</value>
        private ILogger Logger { get; set; }

        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeService"/> class.
        /// </summary>
        /// <param name="strokeContext">The stroke context.</param>
        /// <param name="weChatOptions">The we chat options.</param>
        /// <param name="logger"></param>
        /// <param name="contextAccessor"></param>
        public StrokeService(StrokeContext strokeContext,IOptions<WeChatOptions> weChatOptions,ILogger<StrokeService> logger, IHttpContextAccessor contextAccessor, IMemoryCache memoryCache)
        {
            this.strokeContext = strokeContext;
            this.weChatOptions = weChatOptions.Value;
            this.Logger = logger;
            this.contextAccessor = contextAccessor;
            this.memoryCache = memoryCache;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取Banner数据
        /// </summary>
        /// <returns>Task&lt;Response&lt;GetBannerResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<GetBannerResponse[]>> GetBanner(GetBannerRequest request)
        {
            if (request.CityId == 0)
            {
                request.CityId = 860571;//默认为杭州市
            }


            var bannerInfo = await (this.strokeContext.Ad.Where(x => x.CityId == request.CityId && x.Type == ADType.Banner)
                .Select(x => new GetBannerResponse
                {
                    Id = x.Id,
                    Type = x.Type,
                    Title = x.Title,
                    CityId = x.CityId,
                    Content = x.Content,
                    StrokeId = x.StrokeId,
                    Link = x.Link,
                    ImageUrl = x.ImageUrl,
                    ImageThumb = x.ImageThumb
                })).ToArrayAsync();

            if (bannerInfo.Count()==0)
            {
                bannerInfo= await (this.strokeContext.Ad.Where(x => x.CityId == 860571 && x.Type == ADType.Banner)
                .Select(x => new GetBannerResponse
                {
                    Id = x.Id,
                    Type = x.Type,
                    Title = x.Title,
                    CityId = x.CityId,
                    StrokeId=x.StrokeId,
                     Link=x.Link,
                    Content = x.Content,
                    ImageUrl = x.ImageUrl,
                    ImageThumb = x.ImageThumb
                })).ToArrayAsync();
            }
            return new Response<GetBannerResponse[]>
            {
                Data = bannerInfo
            };
        }

        /// <summary>
        /// 获取全部城市信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetCityResponse[]&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<GetCityResponse[]>> GetCityInfo(GetCityRequest request)
        {
            var cityInfo = await (this.strokeContext.Cities.WhereIf(request.IsHot.HasValue, x => x.IsHot == request.IsHot)
                .WhereIf(!string.IsNullOrEmpty(request.SearchKey), x => x.Name.Contains(request.SearchKey) || x.FullSpell.Contains(request.SearchKey) || x.SimpleSpell.Contains(request.SearchKey))
                .Where(x => x.IsOpen)
                .Where(x=>x.ParentId!=0)
                .OrderBy(x=>x.SimpleSpell)
                .Select(x => new GetCityResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsOpen = x.IsOpen,
                    IsHot = x.IsHot,
                    SimpleSpell = x.SimpleSpell,
                    FullSpell = x.FullSpell
                })).ToArrayAsync();
            return new Response<GetCityResponse[]>
            {
                Data = cityInfo
            };
        }

        /// <summary>
        /// 获取推荐行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetRecommendStokeResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<GetRecommendStrokeResponse[]>> GetRecommendStroke(GetRecommendStrokeRequest request)
        {
            var cityName = "杭州市";
            if (request.CityId != 0)
            {
                var cityInfo = await this.strokeContext.Cities.FirstOrDefaultAsync(x => x.Id == request.CityId);
                cityName = cityInfo.Name;
            }
            var strokeInfo =await  this.strokeContext.Strokes.WhereIf(request.CurrentCity, x => x.City.Contains(cityName))
            .Where(x => x.IsRecommend)
              .Select(x => new GetRecommendStrokeResponse
              {
                  Id = x.Id,
                  Title = x.Title,
                  City = x.City,
                  Destination = x.Destination,
                  ImageUrl = x.ImageUrl,
                  ImageThumb = x.ImageThumb
              }).ToArrayAsync();
            if (strokeInfo.Count()==0)
            {
                strokeInfo= await this.strokeContext.Strokes.WhereIf(request.CurrentCity, x => x.City.Contains("杭州市"))
            .Where(x => x.IsRecommend)
              .Select(x => new GetRecommendStrokeResponse
              {
                  Id = x.Id,
                  Title = x.Title,
                  City = x.City,
                  Destination = x.Destination,
                  ImageUrl = x.ImageUrl,
                  ImageThumb = x.ImageThumb
              }).ToArrayAsync();
            }
            return new Response<GetRecommendStrokeResponse[]>
            {
                Data = strokeInfo
            };
        }

        /// <summary>
        /// 获取行程详情信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId"></param>
        /// <returns>Response&lt;GetStokeDetailResponse&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<GetStrokeDetailResponse>>  GetStrokeDetail(GetStrokeDetailRequest request)
        {
            var userId = contextAccessor.UserId();
            var strokeInfo =await  this.strokeContext.Strokes.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (strokeInfo==null)
            {
                throw new Exception("获取行程信息失败");
            }
            var tempRows = await (from stroke in this.strokeContext.Strokes
                                  join strokeColl in this.strokeContext.StrokeCollects on stroke.Id equals strokeColl.StrokeId into temp
                                  from t in temp.DefaultIfEmpty()
                                  where stroke.Id == request.Id
                                  select new GetStrokeDetailResponse
                                  {
                                      Id = stroke.Id,
                                      Title = stroke.Title,
                                      Detail = stroke.Detail,
                                      CollectId = t == null ? 0 : t.Id,//行程收藏编号
                                      IsActive =t==null?false:true,
                                      ImageUrl = stroke.ImageUrl,
                                      ImageThumb = stroke.ImageThumb,
                                      NumberDay = stroke.NumberDay,
                                      City = stroke.City,
                                      Destination = stroke.Destination,
                                      Price = stroke.Price,
                                      SpecialPrice = stroke.SpecialPrice
                                  }).FirstOrDefaultAsync();
            return new Response<GetStrokeDetailResponse>
            {
                Data = tempRows
            };
     
        }

        /// <summary>
        /// 收藏行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId"></param>
        /// <returns>Task&lt;Response&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<CollectStrokeResponse>> CollectStroke(CollectStrokeRequest request)
        {
            var userId = contextAccessor.UserId();
            var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (userInfo==null||!userInfo.IsActive)
            {
                throw new Exception("用户信息不存在或已被禁用");
            }
            var strokeInfo = await this.strokeContext.Strokes.FirstOrDefaultAsync(x => x.Id == request.StrokeId);
            if (strokeInfo==null)
            {
                throw new Exception("获取行程失败");
            }

            var collectId = 0l;
            if (request.CollectId == 0)
            {
                var collectStoke = await this.strokeContext.StrokeCollects.FirstOrDefaultAsync(x => x.StrokeId == request.StrokeId && x.UserId == userId);
                if (collectStoke != null)
                {
                    throw new Exception("该行程已经收藏");
                }
                //添加收藏
                var newCollect = this.strokeContext.StrokeCollects.Add(new StrokeCollect
                {
                    StrokeId = request.StrokeId,
                    UserId = userId,
                    IsActive = true
                });
                this.strokeContext.SaveChanges();
                collectId = newCollect.Entity.Id;
            }
            else {
                var collectStoke = await this.strokeContext.StrokeCollects.FirstOrDefaultAsync(x => x.Id == request.CollectId);
                if (collectStoke == null)
                {
                    throw new Exception("获取收藏行程信息失败");
                }
                this.strokeContext.StrokeCollects.Remove(collectStoke);
                this.strokeContext.SaveChanges();

            }

            return new Response<CollectStrokeResponse>
            {
                Data = new CollectStrokeResponse
                {
                    CollectId = collectId
                }
            };
        }

        /// <summary>
        /// 获取所有的收藏行程
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;Response&lt;GetCollectStokeResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<GetCollectStrokeResponse[]>> GetCollectStroke()
        {
            var userId = contextAccessor.UserId();
            var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (userInfo==null||!userInfo.IsActive)
            {
                throw new Exception("用户不存在或已被禁用");
            }
            var collectInfo = this.strokeContext.StrokeCollects;
            var strokeInfo = this.strokeContext.Strokes;

            var tempRows = await (from collect in collectInfo
                                  join stroke in strokeInfo on collect.StrokeId equals stroke.Id
                                  where collect.UserId == userId
                                 && collect.IsActive
                                  select new GetCollectStrokeResponse
                                  {
                                      Id = collect.Id,
                                      UserId = collect.UserId,
                                      StrokeId = stroke.Id,
                                      Title = stroke.Title,
                                      ImageUrl = stroke.ImageUrl,
                                      ImageThumb = stroke.ImageThumb
                                  }).ToArrayAsync();
            return new Response<GetCollectStrokeResponse[]>
            {
                Data = tempRows
            };
           
        }

        /// <summary>
        /// 行程分享
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;Response&lt;StrokeShareResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<StrokeShareResponse>> StrokeShare(StrokeShareRequest request)
        {
            var userId = contextAccessor.UserId();
            var strokeInfo = await this.strokeContext.Strokes.FirstOrDefaultAsync(x => x.Id == request.StrokeId);
            if (strokeInfo == null || strokeInfo.IsActive == false)
            {
                throw new Exception("该行程不存在或已经下架");
            }
            Logger.LogDebug($"dizHi------------{request.ShareUrl}");
          // var resultUrl = "https://bcl.baocailang.com/wechat/oauth2?state=travel";//用此地址
            var result = JSSDKHelper.GetJsSdkUiPackageAsync(this.weChatOptions.AppId, this.weChatOptions.AppSecret, request.ShareUrl).Result;

            #region 另一种方法 
            //var timestamp = JSSDKHelper.GetTimestamp();
            //var nonceStr = JSSDKHelper.GetNoncestr();

            //var ticket = Senparc.Weixin.MP.Containers.JsApiTicketContainer.TryGetJsApiTicket(this.weChatOptions.AppId, this.weChatOptions.AppSecret, true);
            //var signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, request.ShareUrl);

            #endregion
            var shareUrl =$"https://bcl.baocailang.com/wechat/oauth2?state=travel%26shareCode={userId}%26from=1%26resultUrl={request.JumpUrl}";

            //var shareUrl = request.ShareUrl + $"&shareCode={userId}&from=1&resultUrl={request.JumpUrl}";
            Logger.LogDebug($"构造的分享地址为:{shareUrl}");
            return new Response<StrokeShareResponse>
            {
                Data = new StrokeShareResponse
                {
                    AppId =this.weChatOptions.AppId,
                    NonceStr = result.NonceStr,
                    Signature = result.Signature,
                    Timestamp = result.Timestamp,
                    ShareImageUrl = this.weChatOptions.HostUrl + strokeInfo.ImageUrl,
                    ShareUrl = shareUrl
                }
            };
        }


        /// <summary>
        /// 行程搜索
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;StrokeSearchResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<StrokeSearchResponse[]>> StrokeSearch(StrokeSearchRequest request)
        {
            if (string.IsNullOrEmpty(request.SearchKey))
            {
                throw new Exception("请输入要搜索的城市");
            }
            var tempRows = await this.strokeContext.Strokes.WhereIf(!string.IsNullOrEmpty(request.SearchKey),
                x => x.City.Contains(request.SearchKey) || x.Destination.Contains(request.SearchKey))
                .Select(x => new StrokeSearchResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    City = x.City,
                    Destination = x.Destination,
                    ImageUrl = x.ImageUrl,
                    ImageThumb = x.ImageThumb
                }).ToArrayAsync();
            return new Response<StrokeSearchResponse[]>
            {
                Data = tempRows
            };

        }

        /// <summary>
        /// 添加私人定制行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;AddCustomizeResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<AddCustomizeResponse>> AddCustomize(AddCustomizeRequest request)
        {
            try
            {
                var userId = contextAccessor.UserId();
                var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (userInfo == null)
                {
                    throw new Exception("获取用户信息失败");
                }
                //var customInfo = await this.strokeContext.Customizes
                //    .Where(x => x.UserId == userId)
                //    .Where(x => x.StartCity == request.StratCity)
                //    .Where(x => x.Destination == request.Destination)
                //    .Where(x => (int)x.Status != 1)
                //    .Where(x => (int)x.Status != 4)
                //    .FirstOrDefaultAsync();
                //if (customInfo!=null)
                //{ 
                //        throw new Exception("您已经提交定制订单,订单取消或完成后才能再次提交");
                //}

                var customizes = this.strokeContext.Customizes.Add(new Customize
                {
                    UserId = userId,
                    StartCity = request.StratCity,
                    Destination = request.Destination,
                    Cphone = request.Cphone,
                    Status = CustomizeStatus.Await,
                    CustomizeNo = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                    Type=request.Type,
                    Adult = request.Adult,
                    Children = request.Children,
                    Name = request.Name,
                    Budget = request.Budget,
                    NumberDay = request.NumberDay,
                    StartDate = request.StartDate.Value,
                    Remark = request.Remark
                });
                this.strokeContext.SaveChanges();
                return new Response<AddCustomizeResponse>
                {
                    Data = new AddCustomizeResponse
                    {
                        Id = customizes.Entity.Id,
                        CustomizeNo = customizes.Entity.CustomizeNo
                    }
                };
            }
            catch (Exception ex)
            {
                Logger.LogDebug("添加定制旅行信息出错：信息---" + ex.Message);
                throw new Exception("参数有误");
            }
          
        }


        /// <summary>
        /// 获取全部私人定制行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetCustomizeResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<GetCustomizeResponse[]>> GetCustomize(GetCustomizeRequest request)
        {
            var userId = contextAccessor.UserId();
            var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (userInfo == null)
            {
                throw new Exception("获取用户信息失败");
            }
            var tempRows = await this.strokeContext.Customizes.WhereIf(request.Status.HasValue, x => x.Status == request.Status)
                .Where(x=>x.UserId==userId)
                .OrderByDescending(x=>x.CreateTime)
                .Select(x => new GetCustomizeResponse
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    StartCity = x.StartCity,
                    Destination = x.Destination,
                    Cphone = x.Cphone,
                    Status = x.Status,
                    CustomizeNo = x.CustomizeNo,
                    Type=x.Type,
                    Adult = x.Adult,
                    Children = x.Children,
                    Name = x.Name,
                    Budget = x.Budget,
                    NumberDay = x.NumberDay,
                    StartDate = x.StartDate,
                    Remark = x.Remark
                }).ToArrayAsync();
            return new Response<GetCustomizeResponse[]>
            {
                Data = tempRows
            };
        }


        ///// <summary>       行程定制
        ///// 取消定制订单
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        ///// <exception cref="NotImplementedException"></exception>
        //public async Task<Response> CancelCustomize(CancelCustomizeRequest request)
        //{
        //    var userId = contextAccessor.UserId();
        //    var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        //    if (userInfo == null)
        //    {
        //        throw new Exception("获取用户信息失败");
        //    }
        //    var customInfo = await this.strokeContext.Customizes.FirstOrDefaultAsync(x => x.Id == request.CustomizeId);
        //    if (customInfo==null)
        //    {
        //        throw new Exception("获取定制信息");
        //    }
        //    if ((int)customInfo.Status==-1)
        //    {
        //        throw new Exception("此定制订单已经取消");
        //    }
        //    if ((int)customInfo.Status>=(int)CustomizeStatus.Agree)
        //    {
        //        throw new Exception("已经给您定制，不能取消");
        //    }
        //    customInfo.Status = CustomizeStatus.Cancel;
        //    customInfo.UpdateTime = DateTime.Now;
        //    this.strokeContext.Customizes.Update(customInfo);
        //    this.strokeContext.SaveChanges();
        //    return Response.Success();
        //}

        ///// <summary>
        ///// 确认定制行程状态完成
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        ///// <exception cref="NotImplementedException"></exception>
        //public async  Task<Response> CompleteCustomize(CompleteCustomizeRequest request)
        //{
        //    var userId = contextAccessor.UserId();
        //    var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        //    if (userInfo == null)
        //    {
        //        throw new Exception("获取用户信息失败");
        //    }
        //    var customInfo = await this.strokeContext.Customizes.FirstOrDefaultAsync(x => x.Id == request.CustomizeId);
        //    if (customInfo == null)
        //    {
        //        throw new Exception("获取定制信息");
        //    }
        //    if ((int)customInfo.Status==-1)
        //    {
        //        throw new Exception("此定制订单已经取消");
        //    }
        //    if (customInfo.Status != CustomizeStatus.Paid)
        //    {
        //        throw new Exception("此定制还未付款，不能确认完成");
        //    }
        //    customInfo.Status = CustomizeStatus.Complete;
        //    customInfo.UpdateTime = DateTime.Now;
        //    this.strokeContext.Customizes.Update(customInfo);
        //    this.strokeContext.SaveChanges();
        //    return Response.Success();
        //}


        ///// <summary>
        ///// 定制行程支付
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        ///// <exception cref="NotImplementedException"></exception>
        //public async  Task<Response<PlaceAnOrderInfoResponse>> CustiomizePay(CustiomizePayRequest request)
        //{
        //    var userId = contextAccessor.UserId();
        //    var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        //    if (userInfo == null)
        //    {
        //        throw new Exception("获取用户信息失败");
        //    }
        //    var orderInfo = await this.strokeContext.Customizes.FirstOrDefaultAsync(x => x.Id == request.CustomizesId);
        //    if (orderInfo == null)
        //    {
        //        throw new Exception("获取定制行程信息失败");
        //    }
        //    //支付
        //    var weChatPay = new WeChatPayRequest
        //    {
        //        Body = HttpUtility.UrlEncode("订单支付"),
        //        TotalFee = orderInfo.TotalAmount,
        //        SuccessUrl = request.SuccessUrl,
        //        FailedUrl = request.FaildUrl,
        //        NotifyUrl = "https://bcl.baocailang.com:8995/api/Payment/WeChatpayNotify",
        //        SignType = "MD5"
        //    };

        //    weChatPay.OrderIds = orderInfo.Id.ToString();

        //    //获取微信公众号中，微信js   支付信息
        //    var result = GetJsPayRedirect(weChatPay, PaymentType.WeChat);
        //    return new Response<PlaceAnOrderInfoResponse>
        //    {
        //        Data = new PlaceAnOrderInfoResponse
        //        {
        //            MwebUrl = result,
        //            WeChatAmount = orderInfo.TotalAmount,
        //            OutTradeNo = orderInfo.Id.ToString(),
        //            PaymentType = PaymentType.WeChat
        //        }
        //    };
        //}

        /// <summary>
        /// 创建微信JsPay支付请求信息
        /// </summary>
        /// <param name="weChatPay">The we chat pay.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <returns>System.String.</returns>
        public string GetJsPayRedirect(WeChatPayRequest weChatPay, PaymentType paymentType)
        {
            Logger.LogDebug("创建支付订单信息");
            var paymentInfo = CreatePaymentInfo(weChatPay.OrderIds, paymentType);
            Logger.LogDebug($"订单支付PaymentId:{paymentInfo.Id}");
            var returnUrl =string.Format("https://bcl.baocailang.com/WeChat/JsPay");
            var state = string.Format("{0}|{1}|{2}|{3}|{4}", paymentInfo.Id, weChatPay.TotalFee, "123", "456",weChatPay.Body);//申请时携带的状态参数
            //var state = Guid.NewGuid().ToString("N");
            //weChatPay.PaymentId = paymentInfo.Id.ToString();
            //this.memoryCache.Set(state, weChatPay, TimeSpan.FromMinutes(2));
            Logger.LogDebug("创建state成功---{0}",state);
            return OAuthApi.GetAuthorizeUrl(this.weChatOptions.AppId, returnUrl, state, Senparc.Weixin.MP.OAuthScope.snsapi_base);
        }
         
        /// <summary>
        /// 创建支付订单信息
        /// </summary>
        /// <param name="orderIds">The order ids.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <returns>System.Object.</returns>
        public Payment CreatePaymentInfo(string orderIds, PaymentType paymentType)
        {
            var paymentInfo = this.strokeContext.Payments.Add(new Payment
            {
                OrderIds = orderIds,
                PaymentType = paymentType,
                Status = false
            });
            this.strokeContext.SaveChanges();
            return paymentInfo.Entity;
        }

        /// <summary>
        /// 行程支付
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;PlaceAnOrderInfoResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<PlaceAnOrderInfoResponse>> StrokePay(StrokePayRequest request)
        {
            var userId = contextAccessor.UserId();
            var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (userInfo == null)
            {
                throw new Exception("获取用户信息失败");
            }

            if (string.IsNullOrEmpty(userInfo.Cphone))
            {
                return new Response<PlaceAnOrderInfoResponse>
                {
                    Code = 200,
                    Message = "手机号未绑定"
                };
            }
            var strokeInfo = await this.strokeContext.Strokes.FirstOrDefaultAsync(x => x.Id == request.StrokeId);
            if (strokeInfo == null)
            {
                throw new Exception("获取行程信息失败");
            }
            #region 添加订单
            long shareUser = 0;
            long.TryParse(request.ShareCode, out shareUser);

            long fromSource = 0;
            long.TryParse(request.From, out fromSource);
            Logger.LogDebug($"用户来源-from:{fromSource},分享用户：{shareUser}");
            var orderInfo = this.strokeContext.Orders.Add(new Order
            {
                OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                UserId = userId,
                StrokeId = strokeInfo.Id,
                Title = strokeInfo.Title,
                City = strokeInfo.City,
                Destination = strokeInfo.Destination,
                ImageUrl = strokeInfo.ImageUrl,
                NumberDay = strokeInfo.NumberDay,
                From = (UserFrom)fromSource,
                ShareUser = shareUser,
                OrderStatus = OrderStatus.NoPaid,
                Price = strokeInfo.Price,
                TotalAmount = strokeInfo.SpecialPrice,
                Payment = PaymentType.WeChat,
                Remark = "",
                TotalCount = 1
            });
            #endregion
            this.strokeContext.SaveChanges();

            //支付
            var weChatPay = new WeChatPayRequest
            {
                Body = HttpUtility.UrlEncode(orderInfo.Entity.Title),
                TotalFee = orderInfo.Entity.TotalAmount,
                SuccessUrl = request.SuccessUrl,
                FailedUrl = request.FaildUrl,
                NotifyUrl =this.weChatOptions.HostUrl+"/api/Payment/WeChatpayNotify",
                SignType = "MD5",
                 OrderIds=orderInfo.Entity.Id.ToString()
            };


            //获取微信公众号中，微信js   支付信息
            var result = GetJsPayRedirect(weChatPay, PaymentType.WeChat);
            Logger.LogDebug($"支付链接-----{result}");
            return new Response<PlaceAnOrderInfoResponse>
            {
                Data = new PlaceAnOrderInfoResponse
                {
                    OutTradeNo =orderInfo.Entity.Id.ToString(),
                    PaymentType = PaymentType.WeChat,
                    WeChatAmount = orderInfo.Entity.TotalAmount,
                    MwebUrl = result
                }
            };
        }

        /// <summary>
        /// 获取国际，国内，省内热门行程
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetStrokeHotResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<GetStrokeHotResponse[]>> GetStrokeHot(GetStrokeHotRequest request)
        {
            var strokeInfo = await this.strokeContext.Strokes.WhereIf(request.CategoryType>0, x => x.CategoryId==request.CategoryType)
           .Where(x => x.IsRecommend)
             .Select(x => new GetStrokeHotResponse
             {
                 Id = x.Id,
                 Title = x.Title,
                 City = x.City,
                 Destination = x.Destination,
                 ImageUrl = x.ImageUrl,
                 ImageThumb = x.ImageThumb,
                 Price = x.Price,
                 SpecialPrice = x.SpecialPrice
             }).ToArrayAsync();
            return new Response<GetStrokeHotResponse[]>
            {
                Data = strokeInfo
            };
        }

 

        #endregion
    }
}
