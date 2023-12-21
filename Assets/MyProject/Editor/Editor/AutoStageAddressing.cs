using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AddressableAssets;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : AssetPostprocessor
{

    private const string TARGET_DIRECTORY = "Assets/MyProject/RunTime/AssetData";

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
        string[] movedFromAssetPaths)
    {

        // 追加のみ処理
        if (importedAssets.Length <= 0) return;

        var settings = AddressableAssetSettingsDefaultObject.Settings;

        foreach (var asset in importedAssets)
        {
            // 対象のフォルダ以外のファイルは除外
            if (!asset.Contains(TARGET_DIRECTORY)) continue;
            // フォルダは除外
            if (File.GetAttributes(asset).HasFlag(FileAttributes.Directory)) continue;

            var guid = AssetDatabase.AssetPathToGUID(asset);
            var group = settings.DefaultGroup;
            var assetEntry = settings.CreateOrMoveEntry(guid, group);

            // Simplify addressable name
            assetEntry.SetAddress(Path.GetFileNameWithoutExtension(asset));
        }

        AssetDatabase.SaveAssets();
    }
}
