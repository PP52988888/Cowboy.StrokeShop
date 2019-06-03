using Cowboy.Stoke.AspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.ExtensionMethods
{
    public static partial class Extensions
    {
        #region EncryptMd5

        /// <summary>
        /// 将字符串加密为相等的Md5值
        /// </summary>
        /// <param name="sourceString">待转换的字符串</param>
        /// <returns>返回已加密的字符串</returns>
        public static string EncryptMd5(this string sourceString)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(sourceString);
                var hashBytes = md5.ComputeHash(inputBytes);
                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        #endregion

        #region EncryptPassword

        /// <summary>
        /// 将字符串加密为一个Md5密码字符串
        /// </summary>
        /// <param name="inputString">源字符串.</param>
        /// <returns>如果为空，返回Null，否则返回对应的加密字符串.</returns>
        public static string EncryptPassword(this string inputString)
        {
            if (inputString == null)
            {
                return string.Empty;
            }
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(inputString), 0, inputString.Length);
                char[] res2 = new char[res.Length * 2];
                int y = 0;
                for (int i = 0; i < res.Length; i++)
                {
                    int b = ((res[i] + i) & 0xf);
                    b = b > 9 ? b + 0x57 : b + 0x30;
                    res2[y] = (char)b;
                    y++;
                    b = ((res[i] + i) >> 4);
                    b = b > 9 ? b + 0x57 : b + 0x30;
                    res2[y] = (char)b;
                    y++;
                }
                return new string(res2);
            }
        }

        #endregion EncryptPassword

        #region ToPagedQuery

        /// <summary>
        /// 获取分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">query</exception>
        public static IQueryable<T> ToPagedQuery<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 50;
            }
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 获取分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="request">The request.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">request</exception>
        public static IQueryable<T> ToPagedQuery<T>(this IQueryable<T> query, PageRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return ToPagedQuery(query, request.PageIndex, request.PageSize);
        }

        #endregion

        #region ToPagedResult

        /// <summary>
        /// 获取分页查询的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="includeTotalSize">if set to <c>true</c> [include total size].</param>
        /// <returns>PageResult&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">query</exception>
        public static ResponsePaged<T> ToPagedResult<T>(this IQueryable<T> query, int pageIndex, int pageSize, bool includeTotalSize = false)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 50;
            }
            int totalCount = 0, totalPage = 0;
            if (includeTotalSize)
            {
                totalCount = query.Count();
                totalPage = (totalCount / pageSize) + (totalCount % pageSize == 0 ? 0 : 1);
            }
            return new ResponsePaged<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPage = totalPage,
                Data = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable()
            };
        }


        /// <summary>
        /// 获取分页查询的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="request">The request.</param>
        /// <returns>ResponsePaging&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">request</exception>
        public static ResponsePaged<T> ToPagedResult<T>(this IQueryable<T> query, PageRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return ToPagedResult(query, request.PageIndex, request.PageSize, request.IncludeTotalSize);
        }


        #endregion

        public static IEnumerable<T> GetPage<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 50;
            }
            return query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable();
        }


        /// <summary>
        /// 如果值为真，使用输入的条件过滤查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
            {
                return queryable.Where(predicate);
            }
            return queryable;
        }

        /// <summary>
        /// 如果值为真，使用输入的条件过滤查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="elsePredicate">The else predicate.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, bool condition, Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> elsePredicate)
        {
            return condition ? queryable.Where(predicate) : queryable.Where(elsePredicate);
        }
    }
}
