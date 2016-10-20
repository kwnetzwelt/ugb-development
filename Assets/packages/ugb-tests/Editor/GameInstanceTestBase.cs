using UnityGameBase;
using System;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

public class GameInstanceTestBase
{
    protected LifecycleTestGame CreateGameInstance()
    {
        return CreateGameInstance<LifecycleTestGame>();
    }

    protected T CreateGameInstance<T>() where T : Game
    {
        GameObject go = new GameObject();
        var game = go.AddComponent<T>();
        game.runInEditMode = true;
        return game;
    }

    protected void DestroyGameInstance(LifecycleTestGame game)
    {
        GameObject.DestroyImmediate(game.gameObject);
    }

    protected void DestroyGameInstance<T>(T game) where T : Game
    {
        GameObject.DestroyImmediate(game.gameObject);
    }


}