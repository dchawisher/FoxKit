﻿using FoxKit.Modules.DataSet.FoxCore;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FoxKit.Core
{
    public class AssetPostprocessor : UnityEditor.AssetPostprocessor
    {
        public delegate bool TryGetAssetDelegate(string filename, out Object asset);

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // TODO: Handle existing assets
            var assets = new Dictionary<string, Object>();
            var tryGetAsset = MakeTryGetAssetDelegate(assets);
            foreach (var asset in importedAssets)
            {
                var loadedAsset = AssetDatabase.LoadAssetAtPath<Object>(asset);
                assets.Add(asset, loadedAsset);
            }

            foreach (var asset in assets.Values)
            {
                var entity = asset as Entity;
                if (entity == null)
                {
                    continue;
                }                
                entity.OnAssetsImported(tryGetAsset);
            }
        }

        private static TryGetAssetDelegate MakeTryGetAssetDelegate(Dictionary<string, Object> assets)
        {
            return (string path, out Object asset) => TryGetAsset(assets, path, out asset);
        }

        private static bool TryGetAsset(Dictionary<string, Object> newlyImportedAssets, string path, out Object asset)
        {
            if (string.IsNullOrEmpty(path))
            {
                asset = null;
                return true;
            }

            // First see if the asset was just imported.
            if (newlyImportedAssets.TryGetValue(path, out asset))
            {
                return true;
            }

            // Next see if the asset already exists in the project.
            asset = AssetDatabase.LoadAssetAtPath<Object>(path);
            if (asset != null)
            {
                return true;
            }

            Debug.LogError($"Referenced asset {path} not found.");
            return false;
        }
    }
}