using Cowboy.Stoke.AspNetCore.Controllers;
using Cowboy.Stoke.AspNetCore.IService;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// The Cowboy.Stoke.AspNetCore.ApiControllers namespace.
/// </summary>
namespace Cowboy.Stoke.AspNetCore.ApiControllers
{
    /// <summary>
    /// Class UserController.
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.Controllers.ApiController" />
    public class UserController : ApiController
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task<Response<UserLoginResponse>> UserLogin([FromBody] UserLoginRequest request) {
            return userService.UserLogin(request);
        }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response> Register([FromBody] RegisterRequest request)
        {
            return userService.Register(request);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="request">The phone.</param>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<bool>> SendIdentifyingCode([FromBody] SendCodeRequest request)
        {
            return this.userService.SendIdentifyingCodeAsync(request);
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>Response&lt;GetUserInfoResponse&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task< Response<GetUserInfoResponse>> GetUserInfo() {
            return this.userService.GetUserInfo();
        }

        /// <summary>
        /// 获取用户的分享 码
        /// </summary>
        /// <returns>Task&lt;Response&lt;GetUserQRCodeResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetUserQRCodeResponse>> GetUserQRCode() {
            return this.userService.GetUserQRCode();
        }
        /// <summary>
        ///  用户绑定手机号结果
        /// </summary>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<bool>> BindingResults() {
            return this.userService.BindingResults();
        }

    }
}
