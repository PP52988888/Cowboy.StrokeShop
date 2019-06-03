// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-16-2019
//
// Last Modified By : pan
// Last Modified On : 03-05-2019
// ***********************************************************************
// <copyright file="Response.cs" company="Cowboy.Stroke.AspNetCore">
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
    [Serializable]
    public class Response
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public int Code { get; set; } 

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Successes this instance.
        /// </summary>
        /// <returns>Response.</returns>
        public static Response Success()
        {
            return new Response { };
        }
        /// <summary>
        /// Successes the specified data.
        /// </summary>
        /// <typeparam name="TData">The type of the t data.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>Response&lt;TData&gt;.</returns>
        public static Response<TData> Success<TData>(TData data)
        {
            return new Response<TData>
            {
                Code=0,
                Data = data
            };
        }
    }
}
