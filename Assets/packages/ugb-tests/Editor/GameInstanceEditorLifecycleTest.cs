using UnityGameBase;
using System;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

public class GameInstanceEditorLifecycleTest : GameInstanceTestBase
{
    [Test]
    public void CreateAndDestroy()
    {
        var game = CreateGameInstance();
        
        Assert.True(game != null);

        DestroyGameInstance(game);

        Assert.True(game == null);
    }

}