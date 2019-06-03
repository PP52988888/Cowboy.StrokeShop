using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 订单支付
    /// </summary>
    public class OrderPayResponse
    {
        /// <summary>
        /// JsPay支付跳转链接
        /// </summary>
        /// <value>The mweb URL.</value>
        public string MwebUrl { get;  set; }
    }
}
