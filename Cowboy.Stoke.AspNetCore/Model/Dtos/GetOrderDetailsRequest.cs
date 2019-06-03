using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 获取订单相信信息请求
    /// </summary>
    public class GetOrderDetailsRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        /// <value>The order identifier.</value>
        public long OrderId { get;  set; }
    }
}
