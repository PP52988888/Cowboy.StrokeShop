// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-05-2019
//
// Last Modified By : pan
// Last Modified On : 03-05-2019
// ***********************************************************************
// <copyright file="BusinessException.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Common
{
    /// <summary>
    /// Class BusinessException.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BusinessException:Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public BusinessException(int code = 500)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        public BusinessException(string message, int code = 500) : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>The code.</value>
        public int Code { get; }
    }
}
