using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// Class GetRecommendStokeRequest.
    /// </summary>
    public class GetRecommendStrokeRequest
    {
        /// <summary>
        /// 当前城市,true 为当前城市的推荐，false 为全部行程中的推荐
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current city]; otherwise, <c>false</c>.</value>
        public bool CurrentCity { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        /// <value>The city identifier.</value>
        public long CityId { get; set; }
    }
}
