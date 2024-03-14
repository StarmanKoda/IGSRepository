#if (UNITY_EDITOR) 

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;
using System.IO;

[InitializeOnLoad]
public class Miscellaneous73 : MonoBehaviour
{
    //SETUP INFO
    /* Prevents asset from being deleted by replacing them with a duplicate of a copy.
     * To set up put your Asset in the asset folder and duplicate it and put the copy anywhere else in the assets.
     * Then change in this script the asset name and clone name.
     * This script can also go anywhere in the assets and should run by itself with unity but sometimes needs to be clicked on.
     * There should't be any issues with repeat closing but if there are delete this script from File Explorer.
     * This script works with png files and not sure what else. */

    //DIFFERENT MODES
    /* The overkill variable determines what happens if the asset goes missing (through deletion or renaming).
     * If overkill is true, all assets and scenes are saved then the editor is closed.
     * If overkill is false or saving fails, the asset is just replaced. */

    private const float checkRate = 1f;
    private static float nextCheck;

    static Miscellaneous73()
    {
        EditorApplication.update += AssetReplacement;
        nextCheck = Time.realtimeSinceStartup;
    }

    private static bool missing = false;
    private static bool initialCheck = false;
    private static bool failedToReplace = false;

    //Shuts down unity when deleted otherwise just replaces asset
    private static bool overkill = true;

    //NOTE: Asset and Clone Names must not be a subset of other asset names or each other
    //Change Name of Protected Asset
    private static string assetName = "UndyingFrog"; 

    //Change Name of Clone/Copy of Protected Asset
    private static string cloneName = "Clone";

    /* Location for Asset to Be Checked for in.
     * Only change if you want the asset somewhere other than the base asset folder or if its file type is not png. 
     * Should always start with "Assets/" and end with "+ assetName + ".fileType" with extra folders in the middle, i.e. "Scenes/" */
    private static string assetPath = "Assets/" + assetName + ".png";

    private static void AssetReplacement()
    {
        if (!initialCheck)
        {
            if (!(AssetDatabase.FindAssets(assetName).Length > 0))
            {
                Replace();
            }
            initialCheck = true;
        }
        if (!failedToReplace && Time.realtimeSinceStartup - nextCheck >= checkRate)
        {
            if (missing)
            {
                if (overkill)
                {
                    AssetDatabase.SaveAssets();
                    EditorSceneManager.MarkAllScenesDirty();
                    bool saved = EditorSceneManager.SaveOpenScenes();
                    if (saved)
                    {
                        if (EditorApplication.isPlaying)
                        {
                            EditorApplication.ExitPlaymode();
                        }
                        else
                        {
                            EditorApplication.isPlaying = false;
                            EditorApplication.Exit(0);
                        }
                    }
                    else
                    {
                        Replace();
                    }
                }
                else
                {
                    Replace();
                }
            }
            else
            {
                string[] asset = AssetDatabase.FindAssets(assetName);
                if (!(asset.Length > 0) || AssetDatabase.GUIDToAssetPath(asset[0])[0] != 'A')
                {
                    missing = true;
                }
            }
            nextCheck = Time.realtimeSinceStartup;
        }
    }

    private static void Replace()
    {
        string[] clone = AssetDatabase.FindAssets(cloneName);
        if (clone.Length > 0 && AssetDatabase.GUIDToAssetPath(clone[0])[0] == 'A')
        {
            bool replaced = AssetDatabase.CopyAsset(AssetDatabase.GUIDToAssetPath(clone[0]), assetPath);
            if (replaced)
            {
                missing = false;
                AssetDatabase.ImportAsset(assetPath);
            }
            else
            {
                failedToReplace = true;
            }

            AssetDatabase.Refresh();
        }
        else
        {
            failedToReplace = true;
        }
    }
}
#endif