using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Game : MonoBehaviour
{
    private LuaEnv luaEnv;

    private void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'app'");
    }

    private void Update()
    {
        if (luaEnv != null)
        {
            luaEnv.Tick();
        }
    }

    private void OnDestroy()
    {
        luaEnv.Dispose();
    }

    public static async UniTask LoadObject()
    {
        WWW www = new WWW($"{AppConst.StreamingAssetsPath}/model/cube");

        while(!www.isDone)
        {
            await UniTask.Yield();
        }

        AssetBundle bundle = www.assetBundle;

        GameObject go = bundle.LoadAsset<GameObject>("Cube");

        Instantiate(go);
    }
}
