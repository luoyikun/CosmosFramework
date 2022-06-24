﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
namespace Cosmos.Editor.Resource
{
    public class ResourceBundleTreeView : TreeView
    {
        readonly List<ResourceBundleInfo> bundleList = new List<ResourceBundleInfo>();

        public Action<IList<int>> onSelectionChanged;
        public Action<IList<int>> onDelete;
        public Action onAllDelete;
        public ResourceBundleTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader)
        {
            Reload();
            showAlternatingRowBackgrounds = true;
            showBorder = true;
        }
        public void Clear()
        {
            bundleList.Clear();
            Reload();
        }
        public bool AddBundle(ResourceBundleInfo bundleInfo)
        {
            if (!bundleList.Contains(bundleInfo))
            {
                bundleList.Add(bundleInfo);
                Reload();
                return true;
            }
            return false;
        }
        public void RemoveBundle(ResourceBundleInfo bundleInfo)
        {
            if (bundleList.Contains(bundleInfo))
            {
                bundleList.Remove(bundleInfo);
                Reload();
            }
        }
        protected override void DoubleClickedItem(int id)
        {
            base.SingleClickedItem(id);
            EditorUtil.PingAndActiveObject(bundleList[id].BundleName);
        }
        protected override void ContextClickedItem(int id)
        {
            var selected = GetSelection();
            GenericMenu menu = new GenericMenu();
            if (selected.Count >= 1)
            {
                menu.AddItem(new GUIContent("Delete bundle"), false, Delete, selected);
                menu.AddItem(new GUIContent("Delete all bundle"), false, DeleteAll);
            }
            menu.ShowAsContext();
        }
        protected override void RowGUI(RowGUIArgs args)
        {
            var length = args.GetNumVisibleColumns();
            for (int i = 0; i < length; i++)
            {
                DrawCellGUI(args.GetCellRect(i), args.item as ResourceBundleTreeViewItem, args.GetColumn(i), ref args);
            }
        }
        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem { id = -1, depth = -1, displayName = "Root" };
            var assetIcon = ResourceEditorUtil.GetFolderIcon();
            var allItems = new List<TreeViewItem>();
            {
                for (int i = 0; i < bundleList.Count; i++)
                {
                    var item = new ResourceBundleTreeViewItem(i, 1, bundleList[i].BundleName, assetIcon) { BundleSize = bundleList[i].BundleSize };
                    allItems.Add(item);
                }
                SetupParentsAndChildrenFromDepths(root, allItems);
                return root;
            }
        }
        protected override void SelectionChanged(IList<int> selectedIds)
        {
            base.SelectionChanged(selectedIds);
            onSelectionChanged?.Invoke(selectedIds);
        }
        void DrawCellGUI(Rect cellRect, ResourceBundleTreeViewItem treeView, int column, ref RowGUIArgs args)
        {
            switch (column)
            {
                case 0:
                    {
                        var lablCellRect = new Rect(cellRect.x + 4, cellRect.y, cellRect.width, cellRect.height);
                        DefaultGUI.Label(lablCellRect, treeView.BundleSize, args.selected, args.focused);
                    }
                    break;
                case 1:
                    {
                        var iconRect = new Rect(cellRect.x + 2, cellRect.y, cellRect.height, cellRect.height);
                        if (treeView.icon != null)
                            GUI.DrawTexture(iconRect, treeView.icon, ScaleMode.ScaleToFit);
                        var lablCellRect = new Rect(cellRect.x + iconRect.width + 4, cellRect.y, cellRect.width - iconRect.width, cellRect.height);
                        DefaultGUI.Label(lablCellRect, treeView.displayName, args.selected, args.focused);
                    }
                    break;
            }
        }
        void DeleteAll()
        {
            bundleList.Clear();
            onAllDelete?.Invoke();
            Reload();
        }
        void Delete(object context)
        {
            try
            {
                var list = context as IList<int>;
                var length = list.Count;
                var rmbundles = new ResourceBundleInfo[length];
                for (int i = 0; i < length; i++)
                {
                    var rmid = list[i];
                    rmbundles[i] = bundleList[rmid];
                }
                var rmlen = rmbundles.Length;
                for (int i = 0; i < rmlen; i++)
                {
                    bundleList.Remove(rmbundles[i]);
                }
                onDelete?.Invoke(list);
                SetSelection(new int[0]);
                Reload();
            }
            catch (Exception e)
            {
                EditorUtil.Debug.LogError(e);
            }
        }

    }
}
