using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    public class GetUserInfoResponse
    {
        /// <summary>
        /// 微信昵称
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get;  set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get;  set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        /// <value>The integral.</value>
        public long Integral { get; set; }

        /// <summary>
        /// 城市 
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// 手机号码 
        /// </summary>
        /// <value>The cphone.</value>
        public string Cphone { get; set; }
    }
}
