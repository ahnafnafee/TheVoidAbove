using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReleaseEditorMemory
{
    [MenuItem("Tools/PHL/Release Editor Memory")]
    public static void Release()
    {
        EditorUtility.UnloadUnusedAssetsImmediate(true);
        EditorUtility.UnloadUnusedAssetsImmediate(false);
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
