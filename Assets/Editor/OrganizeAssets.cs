using UnityEditor;
using UnityEngine;
using System.IO;

public class OrganizeAssets : EditorWindow
{
    [MenuItem("Tools/Organize Assets")]
    public static void Organize()
    {
        string[] assetGUIDs = AssetDatabase.FindAssets("", new[] { "Assets" });

        foreach (string guid in assetGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string extension = Path.GetExtension(path).ToLower();

            if (path.StartsWith("Assets/Editor") || path.Contains("Plugins") || Directory.Exists(path)) continue;

            string destinationFolder = GetFolderByType(extension);
            if (!string.IsNullOrEmpty(destinationFolder))
            {
                if (!AssetDatabase.IsValidFolder(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                    AssetDatabase.Refresh();
                }

                string destinationPath = Path.Combine(destinationFolder, Path.GetFileName(path));
                string error = AssetDatabase.MoveAsset(path, destinationPath);
                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogError($"Failed to move {path} to {destinationPath}: {error}");
                }
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Assets organized successfully!");
    }

    private static string GetFolderByType(string extension)
    {
        switch (extension)
        {
            // case ".cs": return "Assets/Scripts";
            // case ".png":
            // case ".jpg":
            // case ".jpeg": return "Assets/Sprites";
            // case ".wav":
            case ".mp3": return "Assets/Audio";
            // case ".anim":
            // case ".controller": return "Assets/Animations";
            // case ".prefab": return "Assets/Prefabs";
            default: return string.Empty;
        }
    }
}
