using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 全局配置
/// </summary>
public class AppConst
{
    /// <summary>
    /// 平台属性
    /// </summary>
#if UNITY_ANDROID
        public static string Platform = "Android";
#elif UNITY_IOS
        public static string Platform = "iOS";
#elif UNITY_WEBGL
    public static string Platform = "WebGL";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
    public static string Platform = "Windows";
#endif

    /// <summary>
    /// 游戏模块名称
    /// </summary>
    public static string ModuleName = "Game";

    /// <summary>
    /// 项目Assets目录
    /// </summary>
    public static string DataPath = Application.dataPath;

    /// <summary> 
    /// 项目加载路径
    /// </summary>
    public static string StreamingAssetsPath = $"{Application.streamingAssetsPath}/{ModuleName}/{Platform}/AssetBundles";

    /// <summary>
    /// 缓存加载路径
    /// </summary>
    public static string CacheAssetsPath = $"{Application.persistentDataPath}/{ModuleName}/{Platform}/AssetBundles";


    /// <summary>
    /// Lua代码中间拼接路径
    /// </summary>
    public static string LuaMiddlePath = $"LuaProject/{ModuleName}/src";

    /// <summary>
    /// 开发者模式Lua代码路径
    /// </summary>
    public static string LuaProjrctPath = $"{DataPath}/../{LuaMiddlePath}";

    /// <summary>
    /// 内部Lua代码模式
    /// </summary>
    public static string LuaResourcesPath = $"{DataPath}/{LuaMiddlePath}/Resources";

    /// <summary>
    /// Lua代码打包路径名称
    /// </summary>
    public static string LuaBundleName = "src/lua";

    /// <summary>
    /// 资源打包路径
    /// </summary>
    public static string BuildAssetBundlesPath = $"{StreamingAssetsPath}/{ModuleName}/{Platform}";



}