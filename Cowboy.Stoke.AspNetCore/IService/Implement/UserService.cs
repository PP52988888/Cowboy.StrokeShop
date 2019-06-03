// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 01-16-2019
// ***********************************************************************
// <copyright file="UserService.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cowboy.Stoke.AspNetCore.ExtensionMethods;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Caching;
using Cowboy.Stroke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Cowboy.Stroke.AspNetCore.Options;
using Cowboy.TravelShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Senparc.Weixin.MP.AdvancedAPIs;
using SixLabors.ImageSharp;
using QRCoder;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Hosting;

namespace Cowboy.Stoke.AspNetCore.IService.Implement
{
    /// <summary>
    /// Class UserService.
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.IService.IUserService" />
    public class UserService : IUserService
    {

        #region Field        
        /// <summary>
        /// The stroke context
        /// </summary>
        private readonly StrokeContext strokeContext;

        /// <summary>
        /// The we chat options
        /// </summary>
        private readonly WeChatOptions weChatOptions;

        /// <summary>
        /// The JWT options
        /// </summary>
        private readonly JwtOptions jwtOptions;

        /// <summary>
        /// The memory
        /// </summary>
        private readonly IMemoryCache memory;

        /// <summary>
        /// The options
        /// </summary>
        private readonly SMSOptions smsOptions;
        private ILogger Logger { get; set; }

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;
        /// <summary>
        /// The context accessor
        /// </summary>
        private readonly IHttpContextAccessor contextAccessor;

        #endregion


        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="strokeContext">The stroke context.</param>
        /// <param name="weChatOptions">The we chat options.</param>
        /// <param name="logger"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="smsOptions"></param>
        /// <param name="memory"></param>
        /// <param name="hostingEnvironment"></param>
        public UserService(StrokeContext strokeContext,IOptions<WeChatOptions> weChatOptions,
            ILogger<UserService> logger,IOptions<JwtOptions> jwtOptions,IHttpContextAccessor contextAccessor,
            IOptions<SMSOptions> smsOptions,IMemoryCache memory, IHostingEnvironment hostingEnvironment
            )
        {
            this.strokeContext = strokeContext;
            this.weChatOptions = weChatOptions.Value;
            this.Logger = logger;
            this.jwtOptions = jwtOptions.Value;
            this.contextAccessor = contextAccessor;
            this.smsOptions = smsOptions.Value;
            this.memory = memory;
            this.hostingEnvironment = hostingEnvironment;
        }
        #endregion


        #region Methods

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response> Register(RegisterRequest request)
        {
            var result = ValidateIdentifyingCodeAsync(request.CPhone, request.Code).Data;//验证结果
            if (!result)
            {
                throw new Exception("验证码输入错误");
            }
            var userId = contextAccessor.UserId();

            var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id ==userId);
            var user = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Cphone == request.CPhone);
            if (user != null)
            {
                throw new Exception("该手机号码已经注册");
            }
            userInfo.Cphone = request.CPhone;
            this.strokeContext.Users.Update(userInfo);
            this.strokeContext.SaveChanges();
            return Response.Success();
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns>Response.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<UserLoginResponse>> UserLogin(UserLoginRequest request)
        {
            var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.UserName == request.CPhone && x.PassWord == request.PassWord.EncryptPassword());
            if (userInfo == null)
            {
                throw new Exception("此用户不存在");
            }
            var token = CreateToken(userInfo.UserName, userInfo.Id);
            return new Response<UserLoginResponse>
            {
                Data = new UserLoginResponse
                {
                    UserId=userInfo.Id,
                    Name = userInfo.UserName,
                    Token=token
                }
            };
        }




        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="userId">用户编号</param>
        /// <returns>System.String.</returns>
        public string CreateToken( string userName,long userId) {
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
            return  new JwtSecurityTokenHandler().WriteToken(tokenParameter);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<bool>> SendIdentifyingCodeAsync(SendCodeRequest request)
        {
            var cacheKey = $"sms__{request.Cphone}";
            if (this.memory.TryGetValue<CachedIdentifyingCode>(cacheKey, out CachedIdentifyingCode cacheCode))
            {
                if (cacheCode.LastTime.HasValue && (DateTime.Now - cacheCode.LastTime.Value).TotalSeconds < 30)
                {
                    throw new Exception("规定时间内不能重复获取验证码");
                }
            }
            else
            {
                cacheCode = new CachedIdentifyingCode()
                {
                    Code = new Random().Next(1000, 9999).ToString()
                };
                this.memory.Set(cacheKey, cacheCode, DateTimeOffset.Now.AddSeconds(300));//验证码缓存60秒
            }
            string message = $"您好，您的验证码：{cacheCode.Code}，请及时输入，五分钟之内有效";
            bool isSuccess = await this._SendMessage2Phone(request.Cphone, message, cacheCode.Code);
            if (isSuccess)
            {
                cacheCode.LastTime = DateTime.Now;
            }
            return new Response<bool>
            {
                Data = isSuccess
            };
        }

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Response<bool> ValidateIdentifyingCodeAsync(string phone, string code)
        {
            var cacheKey = $"sms__{phone}";
            if (this.memory.TryGetValue<CachedIdentifyingCode>(cacheKey, out CachedIdentifyingCode cacheCode) && cacheCode.Code == code)
            {
                this.memory.Remove(cacheKey);
                return new Response<bool>
                {
                    Data = true
                };
                // return Task.FromResult(true);
            }
            //return Task.FromResult(false);
            return new Response<bool>
            {
                Data = false
            };
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        async Task<bool> _SendMessage2Phone(string phone, string message, string code = "")
        {
            bool isSuccess = false;
            string resultString = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string body = $"account={smsOptions.ApiKey}&pswd={smsOptions.ApiSecurity}&needstatus=true&msg={message}&mobile={phone}&sendtime={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                    using (HttpContent content = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded"))
                    {
                        string url = "http://send.18sms.com/msg/HttpBatchSendSM";
                        using (HttpResponseMessage responseMessage = await client.PostAsync(url, content))
                        {
                            resultString = await responseMessage.Content.ReadAsStringAsync();
                            isSuccess = !string.IsNullOrEmpty(resultString) && resultString.IndexOf(",0\n") >= 0;
                        }
                    }
                }

                this.strokeContext.SmsSends.Add(new SmsSend
                {
                    Cphone = phone,
                    Content = message,
                    Code = code,
                    IsSuccess = isSuccess,
                });
                this.strokeContext.SaveChanges();
            }
            catch
            {
            }
            return isSuccess;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>Response&lt;GetUserInfoResponse&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task< Response<GetUserInfoResponse>> GetUserInfo()
        {
            var userId = this.contextAccessor.UserId();
            var tempRows = await (from user in this.strokeContext.Users
                                  join city in this.strokeContext.Cities on user.CityId equals city.Id
                                  join integral in this.strokeContext.Integrals on user.Id equals integral.UserId into temp
                                  from t in temp.DefaultIfEmpty()
                                  where user.Id == userId
                                  select new GetUserInfoResponse
                                  {   Cphone=user.Cphone,
                                      City=city.Name,
                                      UserName = user.UserName,
                                      ImageUrl = user.ImageUrl,
                                      Integral = t == null ? 0 : t.Number
                                  }).FirstOrDefaultAsync();

            return new Response<GetUserInfoResponse>
            {
                Data = tempRows
            };

        }


        /// <summary>
        /// 用户绑定手机号结果
        /// </summary>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<bool>> BindingResults()
        {
            var userId = this.contextAccessor.UserId();
            var userInfo =await  this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (userInfo==null)
            {
                throw new Exception("获取用户信息失败");
            }
            return new Response<bool>
            {
                Data = string.IsNullOrEmpty(userInfo.Cphone) ? false : true
            };

        }


        /// <summary>
        /// 获取用户的分享 码
        /// </summary>
        /// <returns>Task&lt;Response&lt;GetUserQRCodeResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<GetUserQRCodeResponse>> GetUserQRCode()
        {
            var userId = this.contextAccessor.UserId();
            var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (userInfo==null)
            {
                throw new Exception("获取用户信息失败 ");
            }
            if (string.IsNullOrEmpty(userInfo.ShareImage))
            {
                //from 0:用户搜索，1：用户分享，2：友商分享，3：广告，4：其他
                var content = $"https://bcl.baocailang.com/wechat/oauth2?state=travel&shareCode={userInfo.Id}&from=1";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                var fileFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploads/ShareCode");
                if (!Directory.Exists(fileFolder))
                {//判断文件夹是否存在
                    Directory.CreateDirectory(fileFolder);//创建文件夹
                }
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                var imageUrl = "/Uploads/ShareCode/" + fileName;
                var rootPath = hostingEnvironment.WebRootPath;
                var filePath = Path.Combine(fileFolder, fileName);
                var memory = new MemoryStream();

                qrCode.GetGraphic(20).Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                using (var image = Image.Load(memory.ToArray()))
                {
                    image.Mutate(x => x
                           .Resize(image.Width / 5, image.Height / 5)
                           .Grayscale());
                    image.Save(filePath);
                }
                userInfo.ShareImage = imageUrl;
                this.strokeContext.Users.Update(userInfo);
                this.strokeContext.SaveChanges();
                return new Response<GetUserQRCodeResponse>
                {
                    Data = new GetUserQRCodeResponse
                    {
                        UserName = userInfo.UserName,
                        ShareImage = imageUrl
                    }
                };
            }
            else {
                return new Response<GetUserQRCodeResponse>
                {
                    Data = new GetUserQRCodeResponse
                    {
                        UserName = userInfo.UserName,
                        ShareImage = userInfo.ShareImage
                    }
                };

            }
        }
        #endregion
    }
}
