﻿using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Cosmos.Editor.Resource
{
    public class ResourceBuilderWindowUtility
    {
        /// <summary>
        /// 检测资源与场景是否同处于一个AB包中；
        /// </summary>
        /// <param name="bundlePath">包地址</param>
        /// <returns>是否处于同一个包</returns>
        public static bool CheckAssetsAndScenesInOneAssetBundle(string bundlePath)
        {
            if (File.Exists(bundlePath))//若是文件
                return false;
            var exts = Directory.GetFiles(bundlePath, ".", SearchOption.AllDirectories).Select(path => Path.GetExtension(path)).ToHashSet();
            exts.Remove(".meta");
            if (exts.Contains(".unity"))
            {
                exts.Remove(".unity");
                return exts.Count != 0;
            }
            return false;
        }
        public static Texture2D GetHorizontalLayoutGroupIcon()
        {
            return EditorGUIUtility.FindTexture("HorizontalLayoutGroup Icon");
        }
        /// <summary>
        /// 获取原生场景资源icon
        /// </summary>
        /// <returns>icon</returns>
        public static Texture2D GetSceneIcon()
        {
            return EditorGUIUtility.FindTexture("SceneAsset Icon");
        }
        /// <summary>
        /// 获取原生Folder资源icon
        /// </summary>
        /// <returns>icon</returns>
        public static Texture2D GetFolderIcon()
        {
            return EditorGUIUtility.FindTexture("Folder Icon");
        }
        public static Texture2D GetFolderEmptyIcon()
        {
            return EditorGUIUtility.FindTexture("FolderEmpty Icon");
        }
        public static Texture2D GetFindDependenciesIcon()
        {
            return EditorGUIUtility.FindTexture("UnityEditor.FindDependencies");
        }
        public static Texture2D GetAssetRefreshIcon()
        {
            return EditorGUIUtility.FindTexture("Refresh");
        }
        public static Texture2D GetInvalidIcon()
        {
            return EditorGUIUtility.FindTexture("TestFailed");
        }
        public static Texture2D GetValidIcon()
        {
            return EditorGUIUtility.FindTexture("TestPassed");
        }
        public static Texture2D GetIgnoredcon()
        {
            return EditorGUIUtility.FindTexture("TestIgnored");
        }
        public static Texture2D GetFilterByTypeIcon()
        {
            return EditorGUIUtility.FindTexture("FilterByType");
        }
        public static MultiColumnHeaderState CreateResourceBundleMultiColumnHeader()
        {
            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Size"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=64,
                    width=72,
                    maxWidth=128,
                    autoResize = true,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Count"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=36,
                    width=56,
                    maxWidth=80,
                    autoResize = true,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Splittable"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=48,
                    width=64,
                    maxWidth=80,
                    autoResize = false,
                    canSort=true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Bundle"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=192,
                    width = 384,
                    maxWidth=1024,
                    autoResize = false,
                    canSort=true
                }
            };
            var state = new MultiColumnHeaderState(columns);
            return state;
        }
        public static MultiColumnHeaderState CreateResourceObjectMultiColumnHeader()
        {
            var columns = new[]
            {     
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(GetFilterByTypeIcon()),
                    headerTextAlignment = TextAlignment.Center,
                    sortingArrowAlignment = TextAlignment.Center,
                    sortedAscending = false,
                    minWidth=28,
                    width=28,
                    maxWidth=28,
                    autoResize = true,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Name"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=128,
                    width = 160,
                    maxWidth=512,
                    autoResize = true,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Extension"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=64,
                    width=72,
                    maxWidth=128,
                    autoResize = true,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Valid"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=40,
                    width=40,
                    maxWidth=40,
                    autoResize = true,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Size"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=64,
                    width=72,
                    maxWidth=128,
                    autoResize = true,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("AssetBundle"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=128,
                    width=256,
                    maxWidth=512,
                    autoResize = true,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("AssetPath"),
                    headerTextAlignment = TextAlignment.Left,
                    sortingArrowAlignment = TextAlignment.Left,
                    sortedAscending = false,
                    minWidth=256,
                    width=1024,
                    maxWidth=1536,
                    autoResize = true,
                }
            };

            var state = new MultiColumnHeaderState(columns);
            return state;
        }
    }
}
