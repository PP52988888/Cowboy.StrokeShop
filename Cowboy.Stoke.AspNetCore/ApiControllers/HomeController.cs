// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-23-2019
//
// Last Modified By : pan
// Last Modified On : 03-11-2019
// ***********************************************************************
// <copyright file="HomeController.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Options;
using Cowboy.TravelShop.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.ApiControllers
{
    /// <summary>
    /// Class HomeController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("[controller]/[action]")]
    public class HomeController:Controller
    {
        #region Fields
        /// <summary>
        /// The we chat options
        /// </summary>
        private readonly WeChatOptions weChatOptions;
        /// <summary>
        /// The JWT options
        /// </summary>
        private readonly JwtOptions jwtOptions;

        private readonly ConfigOptions configOptions;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// The stroke context
        /// </summary>
        private readonly StrokeContext strokeContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="weChatOptions">The we chat options.</param>
        /// <param name="jwtOptions">The JWT options.</param>
        /// <param name="configOptions"></param>
        /// <param name="logger">The logger.</param>
        /// <param name="strokeContext">The stroke context.</param>
        public HomeController(IOptions<WeChatOptions> weChatOptions, IOptions<JwtOptions> jwtOptions,IOptions<ConfigOptions> configOptions, ILogger<HomeController> logger, StrokeContext strokeContext)
        {
            this.weChatOptions = weChatOptions.Value;
            this.logger = logger;
            this.strokeContext = strokeContext;
            this.jwtOptions = jwtOptions.Value;
            this.configOptions = configOptions.Value;
        }

        #endregion


        #region   通过openId进行微信登陆
        ///// <summary>
        ///// openId微信登陆
        ///// </summary>
        ///// <param name="openid">微信openId</param>
        ///// <param name="shareCode">分享码</param>
        ///// <param name="from">用户来源</param>
        ///// <param name="nickname">昵称 </param>
        ///// <param name="province">省份</param>
        ///// <param name="city">城市</param>
        ///// <param name="headimgurl">头像</param>
        ///// <returns>ActionResult.</returns>
        ///// <exception cref="Exception"></exception>
        //[HttpPost]
        //[HttpGet]
        //public ActionResult Login(string openid, string shareCode, string from, string nickname, string province, string city, string headimgurl)
        //{
        //    #region 
        //    //try
        //    //{
        //    //    logger.LogDebug($"获取微信用户信息:code---:{code},shareCode---:{shareCode}");
        //    //    logger.LogDebug($"appid:{this.weChatOptions.AppId},appSecret:{this.weChatOptions.AppSecret}");
        //    //    var result = OAuthApi.GetAccessToken(this.weChatOptions.AppId, this.weChatOptions.AppSecret, code);
        //    //    logger.LogDebug($"结果:{result.errmsg}-----code:{result.errcode}");
        //    //    if (result.errcode != Senparc.Weixin.ReturnCode.请求成功)
        //    //    {
        //    //        throw new Exception("微信授权登陆失败!");
        //    //    }
        //    //    var weChatUserInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
        //    //    logger.LogDebug("获取微信用户信息成功");
        //    //    var userInfo = this.strokeContext.Users.FirstOrDefault(x => x.WeChatNo == result.openid);
        //    //    if (userInfo == null)//新用户
        //    //    {
        //    //        //获取城市编号
        //    //        var cityInfo = this.strokeContext.Cities.FirstOrDefault(x => x.Name.Contains(weChatUserInfo.city));

        //    //        #region 添加用户基本信息，来源信息
        //    //        var newUserInfo = this.strokeContext.Users.Add(new User
        //    //        {
        //    //            Cphone = "",
        //    //            PassWord = "",
        //    //            UserName = weChatUserInfo.nickname,
        //    //            IsActive = true,
        //    //            Sex = weChatUserInfo.sex == 2 ? false : true,
        //    //            WeChatNo = weChatUserInfo.openid,
        //    //            ImageUrl = weChatUserInfo.headimgurl,
        //    //            ImageThumb = weChatUserInfo.headimgurl,
        //    //            CityId = cityInfo != null ? cityInfo.Id : 860571,
        //    //            UserSource = new UserSource
        //    //            {
        //    //                Adress = weChatUserInfo.province + weChatUserInfo.city,
        //    //                City = weChatUserInfo.city,
        //    //                CityId = (cityInfo != null ? cityInfo.Id : 860571)
        //    //            }
        //    //        });
        //    //        this.strokeContext.SaveChanges();
        //    //        #endregion
        //    //        long fromUser = 0;
        //    //        long.TryParse(shareCode, out fromUser);
        //    //        var invitedUserInfo = this.strokeContext.Users.FirstOrDefault(x => x.Id == fromUser);
        //    //        if (invitedUserInfo != null)
        //    //        {
        //    //            this.strokeContext.UserandUsers.Add(new UserandUser
        //    //            {
        //    //                UserId = newUserInfo.Entity.Id,
        //    //                InvitedUser = fromUser
        //    //            });
        //    //            this.strokeContext.SaveChanges();
        //    //        }
        //    //        var token = CreateToken(newUserInfo.Entity.UserName, newUserInfo.Entity.Id);

        //    //        HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions
        //    //        {
        //    //            Expires = DateTime.Now.AddDays(1)
        //    //        });

        //    //        return new ContentResult()
        //    //        {
        //    //            ContentType = "text/html;",
        //    //            Content = System.IO.File.ReadAllText("wwwroot/index.jtml")
        //    //        };
        //    //    }
        //    //    else
        //    //    {//老用户
        //    //        var token = CreateToken(userInfo.UserName, userInfo.Id);
        //    //        HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions
        //    //        {
        //    //            Expires = DateTime.Now.AddDays(1)
        //    //        });

        //    //        return new ContentResult()
        //    //        {
        //    //            ContentType = "text/html;",
        //    //            Content = System.IO.File.ReadAllText("wwwroot/index.jtml")
        //    //        };

        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    logger.LogDebug(ex.Message);
        //    //    throw new Exception("微信登陆授权失败");
        //    //}
        //    #endregion



        //    #region 

        //    try
        //    {
        //        logger.LogDebug($"获取微信用户信息:openid---:{openid},shareCode---:{shareCode}------from:{from}----nickname:{nickname}------province:{province}--------:city:{city}---head:{headimgurl}");
        //        logger.LogDebug($"appid:{this.weChatOptions.AppId},appSecret:{this.weChatOptions.AppSecret}");
        //        if (from == "singlemessage")
        //        {
        //            var url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx46f7b277082c3985&redirect_uri=https%3a%2f%2fbcl.baocailang.com%2fwechat%2foauth2&response_type=code&scope=snsapi_userinfo&state=travel&shareCode={shareCode}&from=1#wechat_redirect";
        //            logger.LogDebug($"分享而来，跳转到微信：{url}");
        //            HttpContext.Response.Headers.Add("Cache-Control", "no-cache");
        //            return Redirect(url);
        //        }



        //        var userInfo = this.strokeContext.Users.FirstOrDefault(x => x.WeChatNo == openid);

        //        logger.LogDebug("获取微信用户信息成功");
        //        if (userInfo == null)//新用户
        //        {
        //            //获取城市编号
        //            var cityInfo = this.strokeContext.Cities.FirstOrDefault(x => x.Name.Contains(city));

        //            #region 添加用户基本信息，来源信息
        //            var newUserInfo = this.strokeContext.Users.Add(new User
        //            {
        //                Cphone = "",
        //                PassWord = "",
        //                UserName = nickname,
        //                IsActive = true,
        //                Sex = true,
        //                WeChatNo = openid,
        //                ImageUrl = headimgurl,
        //                ImageThumb = headimgurl,
        //                CityId = cityInfo != null ? cityInfo.Id : 860571,
        //            });

        //            this.strokeContext.SaveChanges();

        //            #region 添加用户来源信息
        //            logger.LogDebug("添加用户来源信息");
        //            UserFrom fromType = 0;
        //            UserFrom.TryParse(from, out fromType);
        //            this.strokeContext.UserSources.Add(new UserSource
        //            {
        //                UserId = newUserInfo.Entity.Id,
        //                From = fromType,
        //                City = city,
        //                Adress = province + "省" + city + "市",
        //                CityId = cityInfo != null ? cityInfo.Id : 860571
        //            });
        //            this.strokeContext.SaveChanges();
        //            logger.LogDebug("保存用户来源信息成功");
        //            #endregion
        //            #endregion

        //            long shareUser = 0;
        //            long.TryParse(shareCode, out shareUser);
        //            var invitedUserInfo = this.strokeContext.Users.FirstOrDefault(x => x.Id == shareUser);
        //            if (invitedUserInfo != null)
        //            {
        //                this.strokeContext.UserandUsers.Add(new UserandUser
        //                {
        //                    UserId = newUserInfo.Entity.Id,
        //                    InvitedUser = shareUser
        //                });
        //                this.strokeContext.SaveChanges();
        //            }
        //            var token = CreateToken(newUserInfo.Entity.UserName, newUserInfo.Entity.Id);

        //            HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions
        //            {
        //                Expires = DateTime.Now.AddDays(1)
        //            });

        //            return new ContentResult()
        //            {
        //                ContentType = "text/html;",
        //                Content = System.IO.File.ReadAllText("wwwroot/index.html")
        //            };
        //        }
        //        else
        //        {//老用户
        //            var token = CreateToken(userInfo.UserName, userInfo.Id);

        //            HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions
        //            {
        //                Expires = DateTime.Now.AddDays(1)
        //            });

        //            return new ContentResult()
        //            {
        //                ContentType = "text/html;",
        //                Content = System.IO.File.ReadAllText("wwwroot/index.html")
        //            };

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogDebug("微信授权登陆失败:错误信息------" + ex.Message);
        //        throw new Exception("微信登陆授权失败");
        //    }
        //    #endregion

        //}

        #endregion


        /// <summary>
        /// 通过Code进行微信登陆
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="shareCode">分享码</param>
        /// <param name="from">用户来源 </param>
        /// <param name="resultUrl"></param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="Exception">
        /// 微信授权登陆失败!
        /// or
        /// 微信登陆授权失败
        /// </exception>
        [HttpPost,HttpGet]
        public ActionResult LoginByCode(string code,string shareCode,string from,string resultUrl) {
            try
            {
                logger.LogDebug($"跳转链接----- Code:{code}---shareCode:{shareCode}----from:{from}------resultUrl:{resultUrl}");

                if (from == "singlemessage")
                {
                    var url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx46f7b277082c3985&redirect_uri=https%3a%2f%2fbcl.baocailang.com%2fwechat%2foauth2&response_type=code&scope=snsapi_userinfo&state=travel&shareCode={shareCode}&from=1&resultUrl={resultUrl}#wechat_redirect";
                    logger.LogDebug($"分享而来，跳转到微信：{url}");
                    HttpContext.Response.Headers.Add("Cache-Control", "no-cache");
                    return Redirect(url);
                }


                logger.LogDebug($"获取微信用户信息:code---:{code}");
                logger.LogDebug($"appid:{this.weChatOptions.AppId},appSecret:{this.weChatOptions.AppSecret}");
                var result = OAuthApi.GetAccessToken(this.weChatOptions.AppId, this.weChatOptions.AppSecret, code);
                logger.LogDebug($"结果:{result.errmsg}-----code:{result.errcode}");
                if (result.errcode != Senparc.Weixin.ReturnCode.请求成功)
                {
                    throw new Exception("微信授权登陆失败!");
                }
                var weChatUserInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                logger.LogDebug("获取微信用户信息成功");
              
                var userInfo = this.strokeContext.Users.FirstOrDefault(x => x.WeChatNo == weChatUserInfo.openid);

                logger.LogDebug("获取用户信息成功");


                if (userInfo == null)//新用户
                {
                    //获取城市编号
                    var cityInfo = this.strokeContext.Cities.FirstOrDefault(x => x.Name.Contains(weChatUserInfo.city));

                    #region 添加用户基本信息，来源信息
                    var newUserInfo = this.strokeContext.Users.Add(new User
                    {
                        Cphone = "",
                        PassWord = "",
                        UserName = weChatUserInfo.nickname,
                        IsActive = true,
                        Sex = weChatUserInfo.sex==1?true:false,
                        WeChatNo = weChatUserInfo.openid,
                        ImageUrl = weChatUserInfo.headimgurl,
                        ImageThumb = weChatUserInfo.headimgurl,
                        CityId = cityInfo != null ? cityInfo.Id : 860571,
                    });

                    this.strokeContext.SaveChanges();

                        #region 添加用户来源信息
                    logger.LogDebug("添加用户来源信息");
                    UserFrom fromType = 0;
                    UserFrom.TryParse(from, out fromType);
                    this.strokeContext.UserSources.Add(new UserSource
                    {
                        UserId = newUserInfo.Entity.Id,
                        From = fromType,
                        City = weChatUserInfo.city,
                        Adress = weChatUserInfo.province + "省" + weChatUserInfo.city + "市",
                        CityId = cityInfo != null ? cityInfo.Id : 860571
                    });
                    this.strokeContext.SaveChanges();
                    logger.LogDebug("保存用户来源信息成功");
                    #endregion

                    #region 添加用户积分
                    this.strokeContext.Integrals.Add(new Integral
                    {
                        UserId = newUserInfo.Entity.Id,
                        Number = this.configOptions.Initialize
                    });
                    if (this.configOptions.Initialize>0)
                    {
                        //添加增加积分记录
                        this.strokeContext.IntegralRecords.Add(new IntegralRecord
                        {
                            UserId = newUserInfo.Entity.Id,
                            Number = this.configOptions.Initialize,
                            Type = 1
                        });
                    }
                    this.strokeContext.SaveChanges(); 

                    #endregion

                    #endregion


                    long shareUser = 0;
                    long.TryParse(shareCode, out shareUser);

                    #region 建立用户与友商之间的关系
                    //判断来源是否为友商推荐来的
                    if (fromType == UserFrom.AlliesShare)
                    {
                        var alliesInfo = this.strokeContext.allies.FirstOrDefault(x => x.Id == shareUser);
                        if (alliesInfo != null)
                        {
                            this.strokeContext.UserandAllies.Add(new UserandAllies
                            {
                                UserId = newUserInfo.Entity.Id,
                                AlliesId = alliesInfo.Id,
                                IsActive = true
                            });
                            this.strokeContext.SaveChanges();
                            logger.LogDebug("建立友商关系成功");
                        }
                    }
                    #endregion

                    #region 建立用户与用户之间的关系
                    //判断来源是否为用户分享而来
                    if (fromType == UserFrom.UserShare)
                    {
                        var invitedUserInfo = this.strokeContext.Users.FirstOrDefault(x => x.Id == shareUser);
                        if (invitedUserInfo != null)
                        {
                            this.strokeContext.UserandUsers.Add(new UserandUser
                            {
                                UserId = newUserInfo.Entity.Id,
                                InvitedUser = shareUser
                            });
                            this.strokeContext.SaveChanges();
                            logger.LogDebug("建立新用户关系成功");
                        }
                    } 
                    #endregion


                    //创建token
                    var token = CreateToken(newUserInfo.Entity.UserName, newUserInfo.Entity.Id);
                    //token放入首页cookies中
                    HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddSeconds(30)
                    });
                    if (!string.IsNullOrEmpty(resultUrl))
                    {
                        //如果存在跳转地址--就放在Cookies中让前端调用 
                      var  jumpUrl = resultUrl + $"&shareCode={shareCode}&from={from}";
                        logger.LogDebug($"跳转地址为：resultUrl------{resultUrl},jumpUrl-----{jumpUrl}");

                        HttpContext.Response.Cookies.Append("jumpUrl", jumpUrl, new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = DateTime.Now.AddSeconds(30)
                        });
                    }
                    logger.LogDebug("返回新用户的token");
                    return new ContentResult()
                    {
                        ContentType = "text/html;",
                        Content = System.IO.File.ReadAllText("wwwroot/index.html")
                    };
                }
                else
                {
                    //老用户
                    var token = CreateToken(userInfo.UserName, userInfo.Id);

                    HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddSeconds(30)
                    });
                    if (!string.IsNullOrEmpty(resultUrl))
                    {
                        //如果存在跳转地址--就放在Cookies中让前端调用 
                        var jumpUrl = resultUrl + $"&shareCode={shareCode}&from={from}";
                        logger.LogDebug($"跳转地址为：resultUrl------{resultUrl},jumpUrl-----{jumpUrl}");

                        HttpContext.Response.Cookies.Append("jumpUrl", jumpUrl, new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = DateTime.Now.AddSeconds(30)
                        });
                    }
                    logger.LogDebug("返回老用户的token");
                    return new ContentResult()
                    {
                        ContentType = "text/html;",
                        Content = System.IO.File.ReadAllText("wwwroot/index.html")
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogDebug($"通过Code获取微信信息失败{ex.Message}");
                throw new Exception("微信登陆授权失败");
            }

        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="userId">用户编号</param>
        /// <returns>System.String.</returns>
        [HttpGet]
        string CreateToken(string userName, long userId)
        {
            var claims = new[] {
                new Claim(IdentityClaimTypes.UserName,userName),
                new Claim(IdentityClaimTypes.UserId,userId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenParameter = new JwtSecurityToken(
                issuer: "cowboy.com",
                audience: "Cowboy.Stroke.com",
                claims: claims,
                expires: DateTime.Now.AddDays(3),
                 signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenParameter);
        }

    }
}
