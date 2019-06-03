// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-17-2019
//
// Last Modified By : pan
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="ResponsePaged.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.Model
{
    /// <summary>
    /// Class ResponsePaged.
    /// </summary>
    /// <typeparam name="TData">The type of the t data.</typeparam>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.Model.Response{System.Collections.Generic.IEnumerable{TData}}" />
    [Serializable]
    public class ResponsePaged<TData> : Response<IEnumerable<TData>> {
        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get;set;}

        /// <summary>
        /// Gets or sets the total page.
        /// </summary>
        /// <value>The total page.</value>
        public int TotalPage { get; set; }
    }
   
}
