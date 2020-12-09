﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Entity
{
    public interface IEntityHelper
    {
        /// <summary>
        /// 实体对象的根节点；
        /// </summary>
        object EntityAssetRoot { get; }
        /// <summary>
        /// 实例化实体。
        /// </summary>
        /// <param name="entityAsset">要实例化的实体资源。</param>
        /// <returns>实例化后的实体。</returns>
        object InstantiateEntity(object entityAsset);
        /// <summary>
        /// 创建实体。
        /// </summary>
        /// <param name="entityAsset">实体实例。</param>
        /// <param name="entityGroup">实体所属的实体组。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>实体。</returns>
        IEntity CreateEntity(object entityAsset, IEntityGroup entityGroup, object userData);
        /// <summary>
        /// 释放实体。
        /// </summary>
        /// <param name="entityAsset">要释放的实体资源。</param>
        void ReleaseEntity(object entityAsset);
    }
}
