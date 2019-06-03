// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 01-16-2019
// ***********************************************************************
// <copyright file="ResponseTData.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// Class Response.
    /// </summary>
    /// <typeparam name="TData">The type of the t data.</typeparam>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.Model.Response" />
    [Serializable]
    public class Response<TData>:Response
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public TData Data { get; set; }
    }
}
