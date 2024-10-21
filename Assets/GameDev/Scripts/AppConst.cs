using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// ȫ������
/// </summary>
public class AppConst
{
    /// <summary>
    /// ƽ̨����
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
    /// ��Ϸģ������
    /// </summary>
    public static string ModuleName = "Game";

    /// <summary>
    /// ��ĿAssetsĿ¼
    /// </summary>
    public static string DataPath = Application.dataPath;

    /// <summary> 
    /// ��Ŀ����·��
    /// </summary>
    public static string StreamingAssetsPath = $"{Application.streamingAssetsPath}/{ModuleName}/{Platform}/AssetBundles";

    /// <summary>
    /// �������·��
    /// </summary>
    public static string CacheAssetsPath = $"{Application.persistentDataPath}/{ModuleName}/{Platform}/AssetBundles";


    /// <summary>
    /// Lua�����м�ƴ��·��
    /// </summary>
    public static string LuaMiddlePath = $"LuaProject/{ModuleName}/src";

    /// <summary>
    /// ������ģʽLua����·��
    /// </summary>
    public static string LuaProjrctPath = $"{DataPath}/../{LuaMiddlePath}";

    /// <summary>
    /// �ڲ�Lua����ģʽ
    /// </summary>
    public static string LuaResourcesPath = $"{DataPath}/{LuaMiddlePath}/Resources";

    /// <summary>
    /// Lua������·������
    /// </summary>
    public static string LuaBundleName = "src/lua";

    /// <summary>
    /// ��Դ���·��
    /// </summary>
    public static string BuildAssetBundlesPath = $"{StreamingAssetsPath}/{ModuleName}/{Platform}";



}