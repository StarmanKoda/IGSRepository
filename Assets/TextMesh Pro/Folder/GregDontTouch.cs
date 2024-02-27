#if (UNITY_EDITOR) 

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class GregDontTouch : MonoBehaviour
{
    private const float frogInterval = 1f;
    private static float frogIntCheck;

    static GregDontTouch()
    {
        EditorApplication.update += THEFROGSHALLLIVE;
        frogIntCheck = Time.realtimeSinceStartup;
    }

    private static bool graveMistake;
    private static bool frogCheck = false;
    private static bool deadFrog = false;
    private static bool overkill = true;

    private static void THEFROGSHALLLIVE()
    {
        if (!frogCheck)
        {
            string[] alive = AssetDatabase.FindAssets("GregsFrog t:Sprite");
            if (alive.Length == 0)
            {

                string[] emergencyFrog = AssetDatabase.FindAssets("Clone t:Sprite");
                if (emergencyFrog.Length > 0)
                {
                    bool revival = AssetDatabase.CopyAsset(AssetDatabase.GUIDToAssetPath(emergencyFrog[0]), "Assets/GregsFrog.png");
                    if (revival)
                    {
                        if (overkill)
                        {
                            //Debug.Log("The Frog Cannot Die. All files saved.");
                        }
                        else
                        {
                            //Debug.Log("The Frog Cannot Die.");
                        }
                    }
                    else
                    {
                        //Debug.Log("The Frog is Dead :(");
                        deadFrog = true;
                    }

                    AssetDatabase.Refresh();
                }
                else
                {
                    //Debug.Log("The Frog is Dead :(");
                    deadFrog = true;
                }
            }
            frogCheck = true;
        }
        if (!deadFrog && Time.realtimeSinceStartup - frogIntCheck >= frogInterval)
        {
            if (graveMistake)
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
                        string[] emergencyFrog = AssetDatabase.FindAssets("Clone t:Sprite");
                        if (emergencyFrog.Length > 0)
                        {
                            bool revival = AssetDatabase.CopyAsset(AssetDatabase.GUIDToAssetPath(emergencyFrog[0]), "Assets/GregsFrog.png");
                            if (revival)
                            {
                                //Debug.Log("Saved failed. Self Destruct Canceled. The Frog Cannot Die.");
                            }
                            else
                            {
                                //Debug.Log("Saved failed. Self Destruct Canceled. The Frog is Dead :(");
                                deadFrog = true;
                            }

                            AssetDatabase.Refresh();
                        }
                        else
                        {
                            //Debug.Log("Saved failed. Self Destruct Canceled. The Frog is Dead You Monster");
                            deadFrog = true;
                        }
                    }
                }
                else
                {
                    string[] emergencyFrog = AssetDatabase.FindAssets("Clone t:Sprite");
                    if (emergencyFrog.Length > 0)
                    {
                        bool revival = AssetDatabase.CopyAsset(AssetDatabase.GUIDToAssetPath(emergencyFrog[0]), "Assets/GregsFrog.png");
                        if (revival)
                        {
                            //Debug.Log("THE FROG CANNOT DIE.");
                            graveMistake = false;
                        }
                        else
                        {
                            //Debug.Log("The Frog is Dead. For now...");
                            deadFrog = true;
                        }

                        AssetDatabase.Refresh();
                    }
                    else
                    {
                        //Debug.Log("The Frog is Dead You Monster.");
                        deadFrog = true;
                    }
                }
            }
            else
            {
                string[] alive = AssetDatabase.FindAssets("GregsFrog t:Sprite");
                if (alive.Length == 0)
                {
                    if (overkill)
                    {
                        //Debug.Log("YOU HAVE MADE A GRAVE MISTAKE. Saving Files.");
                    }
                    graveMistake = true;
                }
            }
            frogIntCheck = Time.realtimeSinceStartup;
        }
    }
}

#endif
