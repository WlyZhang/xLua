using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

public class BuildAssets
{
    /// <summary>
    /// 打包Bundle平台
    /// </summary>
    private static BuildTarget buildTarget;

    /// <summary>
    /// <summary>
    /// 当前运行平台
    /// </summary>
    private static string platform = string.Empty;


    /// <summary>
    /// 脚本列表
    /// </summary>
    private static Dictionary<string, string> scriptList = new Dictionary<string, string>();



    //================================= 以上是属性 ================================================




    [MenuItem("HotUpdate/Builder/Windows Platform")]
    public static void BuildWindows()
    {
        platform = "Windows";
        buildTarget = BuildTarget.StandaloneWindows64;
        BuildToAssetBundle();
    }

    [MenuItem("HotUpdate/Builder/Mac Platform")]
    public static void BuildMac()
    {
        platform = "Mac";
        buildTarget = BuildTarget.StandaloneOSX;
        BuildToAssetBundle();
    }

    [MenuItem("HotUpdate/Builder/iOS Platform")]
    public static void BuildIOS()
    {
        platform = "iOS";
        buildTarget = BuildTarget.iOS;
        BuildToAssetBundle();
    }

    [MenuItem("HotUpdate/Builder/Android Platform")]
    public static void BuildAndroid()
    {
        platform = "Android";
        buildTarget = BuildTarget.Android;
        BuildToAssetBundle();
    }


    [MenuItem("HotUpdate/Builder/WebGL Platform")]
    public static void BuildWebGL()
    {
        platform = "WebGL";
        buildTarget = BuildTarget.WebGL;
        BuildToAssetBundle();
    }

    private static void BuildToAssetBundle()
    {
        string scriptPath = $"{AppConst.LuaProjrctPath}";

        //递归获取代码文件
        GetDirFiles(scriptPath);

        //创建内部模式代码脚本
        CreateResourcesScripts();

        //重新标记代码脚本Bundles名称
        RenameScriptToBundles();
    }

    /// <summary>
    /// 重新标记代码脚本Bundles名称
    /// </summary>
    private static void RenameScriptToBundles()
    {
        if (!Directory.Exists(AppConst.LuaResourcesPath))
            return;

        List<Object> list = new List<Object>();
        foreach (string key in scriptList.Keys)
        {
            if (key.Contains("meta"))
                continue;

            TextAsset asset  = Resources.Load<TextAsset>($"{key}.lua");
            list.Add(asset);
        }

        ReNameBundle($"{AppConst.LuaBundleName}", list);
    }

    /// <summary>
    /// 创建内部模式代码脚本
    /// </summary>
    private static void CreateResourcesScripts()
    {
        foreach (string key in scriptList.Keys)
        {
            string path = $"{AppConst.LuaMiddlePath}{key}";
            string name = key.Replace("\\", string.Empty);
            string fileName = $"{name}.lua.txt";
            string value = scriptList[name];

            Debug.Log(fileName);

            CreateTextFile(fileName, value);
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

        Debug.Log("<color=green>Resources 内部模式代码已生成</color>");
    }


    /// <summary>
    /// 递归循环遍历目录
    /// </summary>
    /// <param name="path"></param>
    private static void GetDirFiles(string path)
    {
        if(string.IsNullOrEmpty(path)) return;

        string[] files = Directory.GetFiles(path);

        for(int i = 0;i<files.Length;i++)
        {
            if (files[i].Contains("meta"))
                continue;
            int count = files[i].LastIndexOf("\\");
            string str = files[i].Substring(0, count);
            string name = files[i].Replace(str, string.Empty);
            string key = name.Split('.')[0].Replace("\\", string.Empty);

            //Debug.Log(key);

            string value = File.ReadAllText(files[i]);

            if (scriptList.ContainsKey(key))
                continue;
            scriptList.Add(key, value);
        }


        string[] dirs = Directory.GetDirectories(path);

        for (int i = 0; i < dirs.Length; i++)
        {
            GetDirFiles(dirs[i]);
        }
    }

    [MenuItem("HotUpdate/Clean Bundle")]
    public static void DeleteCache()
    {
        platform = string.Empty;
        buildTarget = BuildTarget.NoTarget;

        ClearCache($"{AppConst.LuaResourcesPath}");
        ClearCache($"{AppConst.BuildAssetBundlesPath}");

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 清理缓存
    /// </summary>
    private static void ClearCache(string cachePath)
    {
        if (Directory.Exists(cachePath))
        {
            Directory.Delete(cachePath, true);
            Debug.Log($"<color=yellow>{cachePath} 缓存已清理</color>");
        }
        else
        {
            Debug.Log($"{cachePath} 无缓存目录");
        }
    }

    /// <summary>
    /// 复制文件夹及文件
    /// </summary>
    /// <param name="sourceFolder">原文件路径</param>
    /// <param name="destFolder">目标文件路径</param>
    /// <returns></returns>
    public static int CopyFolder(string sourceFolder, string destFolder)
    {
        try
        {
            //如果目标路径不存在,则创建目标路径
            if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                //得到原文件根目录下的所有文件
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(destFolder, name);
                System.IO.File.Copy(file, dest);//复制文件
            }
            //得到原文件根目录下的所有文件夹
            string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = System.IO.Path.GetFileName(folder);
                string dest = System.IO.Path.Combine(destFolder, name);
                CopyFolder(folder, dest);//构建目标路径,递归复制文件
            }
            return 1;
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 计算md5
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    private static string CreateMD5(byte[] buffer)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] md5Bytes = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                sb.Append(md5Bytes[i].ToString("x2"));//X2时，生成字母大写MD5
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// 创建版本信息
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="info"></param>
    private static void CreateTextFile(string fileName, string info)
    {
        if (!Directory.Exists(AppConst.LuaResourcesPath))
        {
            Directory.CreateDirectory(AppConst.LuaResourcesPath);
        }

        if (File.Exists($"{AppConst.LuaResourcesPath}/{fileName}"))
        {
            File.Delete($"{AppConst.LuaResourcesPath}/{fileName}");
        }

        File.WriteAllText($"{AppConst.LuaResourcesPath}/{fileName}", info);
    }

    /// <summary>
    /// 【Bundle】多个资源打一个包
    /// </summary>
    /// <param name="dir"></param>
    private static void ReNameBundle(string dir, List<Object> list)
    {
        Object[] objs = list.ToArray();

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