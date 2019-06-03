using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{

    /// <summary>
    /// 发送验证码
    /// </summary>
    public class SendCodeRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        /// <value>The cphone.</value>
        public string Cphone { get; set; }
    }
}
