using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityGameBase.Core.Globalization;
using UnityGameBase.Core.Data;

public class LocaImporterTest {

	[Test]
	public void LocaLabelTestFileExists()
	{
		var assets = AssetDatabase.FindAssets( GameLocalization.UGBLocaSourceFilter );

		Assert.Greater(assets.Length, 0, "no asset found with label: " + GameLocalization.UGBLocaSourceFilter);

		foreach(var asset in assets)
		{
			var path = AssetDatabase.GUIDToAssetPath(asset);
			var textasset = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
			Assert.IsNotNull(textasset);
			Resources.UnloadAsset(textasset);
		}	
	}

	[Test]
	public void EditorTest() {

		// remove all generated loca files 
		
		if(System.IO.Directory.Exists(LocaData.targetFolder))
		{
			System.IO.Directory.Delete(LocaData.targetFolder, true);
		}
		
		AssetDatabase.Refresh();
		
		var assets = AssetDatabase.FindAssets( GameLocalization.UGBLocaSourceFilter );
		foreach(var asset in assets)
		{	
			var path = AssetDatabase.GUIDToAssetPath(asset);
			AssetDatabase.ImportAsset(path,ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
		}

		Assert.IsTrue(System.IO.Directory.Exists(LocaData.targetFolder));		

		var dir = new System.IO.DirectoryInfo(LocaData.targetFolder);
		Assert.Greater( dir.GetFiles("*.xml").Length, 0, "not xml files imported through loca");
	}

	
}
