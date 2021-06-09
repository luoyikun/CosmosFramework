﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cosmos.WebRequest
{
    public interface IWebRequestManager : IModuleManager
    {
        /// <summary>
        /// 当前web请求模式；
        /// </summary>
        WebRequestMode CurrentWebRequestMode { get; }
        /// <summary>
        /// 网络状态是否可用；
        /// </summary>
        bool NetworkReachable { get ; }
        /// <summary>
        ///切换网络请求模式；
        /// </summary>
        /// <see cref="WebRequestMode"></see>
        /// <param name="webRequestMode">Web请求模式</param>
        /// <returns>是否切换成功</returns>
        bool ChangeWebRequestMode(WebRequestMode webRequestMode);
        /// <summary>
        /// 异步下载资源；
        /// 注意，返回值类型可以是Task与Coroutine任意一种表示异步的引用对象；
        /// </summary>
        /// <see cref="Task">Return vaalue</see>
        /// <see cref="UnityEngine. Coroutine">Return vaalue</see>
        /// <param name="uri">Uniform Resource Identifier</param>
        /// <param name="webRequestCallback">传入的回调</param>
        /// <returns>表示异步的引用对象</returns>
        object DownloadAsync(string uri, WebRequestCallback webRequestCallback);
        /// <summary>
        /// 异步上传资源；
        /// 注意，返回值类型可以是Task与Coroutine任意一种表示异步的引用对象；
        /// </summary>
        /// <see cref="Task">Return vaalue</see>
        /// <see cref="UnityEngine. Coroutine">Return vaalue</see>
        /// <param name="uri">Uniform Resource Identifier</param>
        /// <param name="webRequestCallback">传入的回调</param>
        /// <returns>表示异步的引用对象</returns>
        object UploadAsync(string uri, WebRequestCallback webRequestCallback);
    }
}
