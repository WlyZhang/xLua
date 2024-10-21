using UnityEditor;
using UnityEngine;

/*
* 将现有的AB资源重新命名，保持原有Bundle名字 ，前面增加了分类路径
*/
#region Bundle 资源重新命名

public class AssetBundleRename
{
    const string Scene = "scene";
    const string UIPanel = "uipanel";
    const string Model = "model";
    const string Texture = "texture";
    const string Sprite = "sprite";
    const string Audio = "audio";
    const string Video = "video";
    const string Config = "config";
    const string Material = "material";
    const string Effect = "effect";


    #region 资源单个打包

    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Scene)]
    static void RenameScene()
    {
        Object[] objs = Selection.objects;

        ReNmaeAsset(Scene);
    }

    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + UIPanel)]
    static void RenameUIPanel()
    {
        Object[] objs = Selection.objects;

        ReNmaeAsset(UIPanel);
    }

    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Model)]
    static void RenameModle()
    {
        Object[] objs = Selection.objects;

        ReNmaeAsset(Model);
    }

    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Texture)]
    static void RenameTexture()
    {
        Object[] objs = Selection.objects;

        ReNmaeAsset(Texture);
    }


    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Sprite)]
    static void RenameSprite()
    {
        Object[] objs = Selection.objects;

        ReNmaeAsset(Sprite);
    }


    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Audio)]
    static void RenameAudio()
    {
        ReNmaeAsset(Audio);
    }


    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Video)]
    static void RenameVideo()
    {
        ReNmaeAsset(Video);
    }

    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Config)]
    static void RenameConfigData()
    {
        Object[] objs = Selection.objects;

        ReNmaeAsset(Config);
    }

    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Effect)]
    static void RenameEffect()
    {
        Object[] objs = Selection.objects;

        ReNmaeAsset(Effect);
    }
    
    [MenuItem("Assets/AssetBundleRename_SingleAsset/" + Material)]
    static void RenameMaterial()
    {
        Object[] objs = Selection.objects;

        ReNmaeAsset(Material);
    }

    #endregion

    #region 资源打进整包

    [MenuItem("Assets/AssetBundleRename_BundlePackage/" + Texture)]
    static void RenameBundleTexture()
    {
        Object[] objs = Selection.objects;

        ReNameBundle(Texture);
    }

    [MenuItem("Assets/AssetBundleRename_BundlePackage/" + Sprite)]
    static void RenameBundleSprite()
    {
        Object[] objs = Selection.objects;

        ReNameBundle(Sprite);
    }

    [MenuItem("Assets/AssetBundleRename_BundlePackage/" + Config)]
    static void RenameBundleConfig()
    {
        Object[] objs = Selection.objects;

        ReNameBundle(Config);
    }

    [MenuItem("Assets/AssetBundleRename_BundlePackage/" + Effect)]
    static void RenameBundleEffect()
    {
        Object[] objs = Selection.objects;

        ReNameBundle(Effect);
    }

    [MenuItem("Assets/AssetBundleRename_BundlePackage/" + Material)]
    static void RenameBundleMaterial()
    {
        Object[] objs = Selection.objects;

        ReNameBundle(Material);
    }

    #endregion


    /// <summary>
    /// 【Asset】单个资源独立打包
    /// </summary>
    /// <param name="dir"></param>
    public static void ReNmaeAsset(string dir)
    {
        Object[] objs = Selection.objects;

        int totalLength = objs.Length;
        int index = 0;
        float progress = 0f;

        foreach (var v in objs)
        {
            index++;
            progress = (float)index / (float)totalLength;
            EditorUtility.DisplayProgressBar("资源命名", "请稍等..." + index.ToString() + "/" + totalLength.ToString(), progress);

            string fileName;
            string path = AssetDatabase.GetAssetPath(v);
            AssetImporter imp = AssetImporter.GetAtPath(path);

            imp.assetBundleName = $"{dir}/{v.name.Trim()}";

            imp.SaveAndReimport();
        }

        UnityEditor.EditorUtility.ClearProgressBar();
    }

    /// <summary>
    /// 【Bundle】多个资源打一个包
    /// </summary>
    /// <param name="dir"></param>
    public static void ReNameBundle(string dir)
    {
        Object[] objs = Selection.objects;

        int totalLength = objs.Length;
        int index = 0;
        float progress = 0f;

        foreach (var v in objs)
        {
            index++;
            progress = (float)index / (float)totalLength;
            EditorUtility.DisplayProgressBar("资源命名", "请稍等..." + index.ToString() + "/" + totalLength.ToString(), progress);

            string path = AssetDatabase.GetAssetPath(v);
            AssetImporter imp = AssetImporter.GetAtPath(path);



            imp.assetBundleName = $"{dir.Trim()}";

            imp.SaveAndReimport();
        }

        AssetDatabase.Refresh();

        UnityEditor.EditorUtility.ClearProgressBar();
    }
}

#endregion

