using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    public class GetStrokeHotRequest
    {

        /// <summary>
        /// 分类类别 :1 为国际，2：为国内，3：省内
        /// </summary>
        /// <value>The type of the category.</value>
        public long CategoryType { get; set; }
    }
}
