// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 01-16-2019
// ***********************************************************************
// <copyright file="IUserService.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Model.Dtos;

namespace Cowboy.Stoke.AspNetCore.IService
{
    /// <summary>
    /// Interface IUserService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns>Response.</returns>
      Task<Response<UserLoginResponse>>  UserLogin(UserLoginRequest request);

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        Task<Response> Register(RegisterRequest request);

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="request">The phone.</param>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        Task<Response<bool>> SendIdentifyingCodeAsync(SendCodeRequest request);




        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>Response&lt;GetUserInfoResponse&gt;.</returns>
      Task<Response<GetUserInfoResponse>> GetUserInfo();

        /// <summary>
        /// 用户绑定手机号结果
        /// </summary>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        Task<Response<bool>> BindingResults();

        /// <summary>
        ///  获取用户的分享 码
        /// </summary>
        /// <returns>Task&lt;Response&lt;GetUserQRCodeResponse&gt;&gt;.</returns>
        Task<Response<GetUserQRCodeResponse>> GetUserQRCode();
    }
}
