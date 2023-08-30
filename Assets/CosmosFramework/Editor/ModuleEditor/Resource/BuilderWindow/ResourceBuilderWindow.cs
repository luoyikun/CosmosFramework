﻿using UnityEngine;
using UnityEditor;
using Cosmos.Resource;

namespace Cosmos.Editor.Resource
{
    public class ResourceBuilderWindow : ModuleWindowBase
    {
        ResourceBuilderWindowData windowData;
        public ResourceBuilderWindowData ResourceWindowData { get { return windowData; } }
        readonly string windowDataName = "ResourceBuilderWindowData.json";
        ResourceBuilderWindowTabBase assetDatabaseTab;
        ResourceBuilderWindowTabBase assetBundleTab;
        ResourceBuilderWindowTabBase assetDatasetTab;
        string[] tabArray = new string[] { "AssetDatabase", "AssetBundle", "AssetDataset" };
        ResourceDataset latestResourceDataset;
        /// <summary>
        /// dataset是否为空处理标记；
        /// </summary>
        bool isDatasetEmpty = false;
        Texture2D refreshIcon;
        [MenuItem("Window/Cosmos/Module/Resource/ResourceBuilder")]
        public static void OpenWindow()
        {
            var window = GetWindow<ResourceBuilderWindow>("ResourceBuilder", true);
            window.minSize = EditorUtil.DevWinSize;
        }
        protected override void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            if (assetBundleTab == null)
                assetBundleTab = new AssetBundleTab(this);
            if (assetDatabaseTab == null)
                assetDatabaseTab = new AssetDatabaseTab(this);
            if (assetDatasetTab == null)
                assetDatasetTab = new AssetDatasetTab(this);
            GetWindowData();
            if (!string.IsNullOrEmpty(windowData.ResourceDatasetPath))
            {
                latestResourceDataset = AssetDatabase.LoadAssetAtPath<ResourceDataset>(windowData.ResourceDatasetPath);
            }
            assetDatabaseTab.OnEnable();
            assetBundleTab.OnEnable();
            assetDatasetTab.OnEnable();
            ((AssetBundleTab)assetBundleTab).BuildDataset = ((AssetDatabaseTab)assetDatabaseTab).BuildDataset;
            refreshIcon = ResourceBuilderWindowUtility.GetAssetRefreshIcon();
        }
        protected override void OnGUI()
        {
            DrawLabels();
        }
        protected override void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            if (ResourceBuilderWindowDataProxy.ResourceDataset != null)
            {
                windowData.ResourceDatasetPath = AssetDatabase.GetAssetPath(ResourceBuilderWindowDataProxy.ResourceDataset);
                EditorUtil.SaveScriptableObject(ResourceBuilderWindowDataProxy.ResourceDataset);
            }
            SaveWindowData();
            assetDatabaseTab.OnDisable();
            assetBundleTab.OnDisable();
            assetDatasetTab.OnDisable();
        }
        protected override void GetWindowData()
        {
            try
            {
                windowData = EditorUtil.GetData<ResourceBuilderWindowData>(ResourceEditorConstants.CACHE_RELATIVE_PATH, windowDataName);
            }
            catch
            {
                windowData = new ResourceBuilderWindowData();
                EditorUtil.SaveData(ResourceEditorConstants.CACHE_RELATIVE_PATH, windowDataName, windowData);
            }
        }
        protected override void SaveWindowData()
        {
            EditorUtil.SaveData(ResourceEditorConstants.CACHE_RELATIVE_PATH, windowDataName, windowData);
        }
        void DrawLabels()
        {
            EditorGUILayout.BeginVertical();
            windowData.SelectedTabIndex = GUILayout.Toolbar(windowData.SelectedTabIndex, tabArray);
            EditorGUILayout.BeginHorizontal();
            {
                latestResourceDataset = (ResourceDataset)EditorGUILayout.ObjectField("ResourceDataset", latestResourceDataset, typeof(ResourceDataset), false);
                if (GUILayout.Button(refreshIcon, GUILayout.MaxWidth(32)))
                {
                    if (latestResourceDataset == null)
                        return;
                    switch (windowData.SelectedTabIndex)
                    {
                        case 0:
                            assetDatabaseTab.OnDatasetRefresh();
                            break;
                        case 1:
                            assetBundleTab.OnDatasetRefresh();
                            break;
                        case 2:
                            assetDatasetTab.OnDatasetRefresh();
                            break;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            if (ResourceBuilderWindowDataProxy.ResourceDataset != latestResourceDataset)
            {
                ResourceBuilderWindowDataProxy.ResourceDataset = latestResourceDataset;
                if (ResourceBuilderWindowDataProxy.ResourceDataset != null)
                    AssignDataset();
                else
                    UnassignDataset();
            }
            else
            {
                if (ResourceBuilderWindowDataProxy.ResourceDataset == null)
                {
                    if (!isDatasetEmpty)
                        UnassignDataset();
                }
            }
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Dataset", GUILayout.MinWidth(128f)))
            {
                latestResourceDataset = CreateResourceDataset();
            }
            if (GUILayout.Button("Clear Dataset", GUILayout.MinWidth(128f)))
            {
                latestResourceDataset = null;
                windowData.ResourceDatasetPath = string.Empty;
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(8);
            switch (windowData.SelectedTabIndex)
            {
                case 0:
                    assetDatabaseTab.OnGUI(this.position);
                    break;
                case 1:
                    assetBundleTab.OnGUI(this.position);
                    break;
                case 2:
                    assetDatasetTab.OnGUI(this.position);
                    break;
            }

            EditorGUILayout.EndVertical();
        }
        void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            ResourceBuilderWindowDataProxy.ResourceDataset = null;
        }
        ResourceDataset CreateResourceDataset()
        {
            var so = EditorUtil.CreateScriptableObject<ResourceDataset>("Assets/New ResourceDataset.asset", HideFlags.NotEditable);
            so.ResourceAvailableExtenisonList.AddRange(ResourceBuilderWindowConstant.Extensions);
            EditorUtil.Debug.LogInfo("ResourceDataset created successfully");
            return so;
        }
        void AssignDataset()
        {
            assetDatabaseTab.OnDatasetAssign();
            assetBundleTab.OnDatasetAssign();
            assetDatasetTab.OnDatasetAssign();
            isDatasetEmpty = false;
            windowData.ResourceDatasetPath = AssetDatabase.GetAssetPath(latestResourceDataset);
            EditorUtil.SaveData(windowDataName, windowData);
        }
        void UnassignDataset()
        {
            assetDatabaseTab.OnDatasetUnassign();
            assetBundleTab.OnDatasetUnassign();
            assetDatasetTab.OnDatasetUnassign();
            isDatasetEmpty = true;
            windowData.ResourceDatasetPath = string.Empty;
            EditorUtil.SaveData(windowDataName, windowData);
        }
        /// <summary>
        /// 预留
        /// </summary>
        void DrawHorizontalScope()
        {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Bundle Data Source:");
                GUILayout.FlexibleSpace();
                var c = new GUIContent("cosmos");
                if (GUILayout.Button(c, EditorStyles.toolbarPopup))
                {
                    GenericMenu menu = new GenericMenu();
                    for (int index = 0; index < 16; index++)
                    {

                    }
                    menu.ShowAsContext();
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}