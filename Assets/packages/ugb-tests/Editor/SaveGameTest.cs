using UnityGameBase;
using System;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

public class SaveGameTest : GameInstanceTestBase
{
    Game game;

    [SetUp]
    protected virtual void SetUp()
    {
        game = CreateGameInstance();
    }
    [TearDown]
    protected virtual void TearDown()
    {
        DestroyGameInstance(game);
    }


    [Test]
    public void SaveSaveGameTest()
    {
        
        var data = new SaveGameTestData();
        var ase = new AutoResetEvent(false);

        UGB.SaveGame.Save(0, data, (result) => {
            ase.Set();
        });
        
        bool saved = ase.WaitOne(5000);

        Assert.True(saved, "save game never written or callback not called");


    }


    [Test]
    public void LoadSaveGameTest()
    {
        var data = new SaveGameTestData();
        var ase = new AutoResetEvent(false);

        UGB.SaveGame.Save(0, data, (result) => {
            ase.Set();
        });
        
        bool saved = ase.WaitOne(5000);

        Assert.True(saved, "save game never written or callback not called");
        
        ase = new AutoResetEvent(false);

        SaveGameTestData loadedData = null;
        UGB.SaveGame.Load<SaveGameTestData>(0, (result, resultData ) => {
            loadedData = resultData;
            ase.Set();
        });

        bool loaded = ase.WaitOne(5000);

        Assert.True(loaded);
        Assert.True(loadedData != null);
    }

    [Serializable]
    public class SaveGameTestData
    {
        public int value1 = 1;
        public float value2 = 23.54f;
        public string value3 = "some string";
    }

}