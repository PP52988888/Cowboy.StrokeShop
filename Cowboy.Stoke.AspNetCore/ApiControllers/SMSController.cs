using Cowboy.Stoke.AspNetCore.Controllers;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.ApiControllers
{
    public class SMSController: ApiController
    {
        private readonly ISMSService sMSService;

        public SMSController(ISMSService sMSService)
        {
            this.sMSService = sMSService;
        }


        ///// <summary>
        ///// 发送文本短信
        ///// </summary>
        ///// <param name="phone">The phone.</param>
        ///// <param name="prefix">The message.</param>
        ///// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        //[HttpPost]
        //public Task<Response<bool>> SendIdentifyingCode(string phone, string prefix = null)
        //{
        //    return this.sMSService.SendIdentifyingCodeAsync(phone, prefix);
        //}
        ///// <summary>
        ///// 验证验证码
        ///// </summary>
        ///// <param name="phone">The phone.</param>
        ///// <param name="code">The code.</param>
        ///// <param name="prefix">The message.</param>
        ///// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        //[HttpPost]
        //public Response<bool> ValidateIdentifyingCode(string phone, string code, string prefix = null)
        //{
        //    return this.sMSService.ValidateIdentifyingCodeAsync(phone, code, prefix);
        //}


    }
}
