using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class ScriptFormatTool : UnityEditor.AssetModificationProcessor
{
	public static void OnWillCreateAsset(string path)
	{
		path = path.Replace(".meta", "");
		if (path.ToLower().EndsWith(".cs") || path.ToLower().EndsWith(".lua"))
		{
			string content = File.ReadAllText(path);

			content = content.Replace("#AUTHORNAME#", ScriptFormatSetting.scriptAuthorName.Value);
			content = content.Replace("#CREATETIME#", System.DateTime.Now.ToString());

			File.WriteAllText(path, content);
		}
	}
}


public class ScriptFormatSetting
{
	public static EditorString scriptAuthorName = new EditorString ("ScriptFormatSetting._scriptAuthorName");

	[PreferenceItem("Scripts")]
	static void OnPreferenceGUI()
	{
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Label ("AuthorName");
		scriptAuthorName.Value = EditorGUILayout.TextField (scriptAuthorName.Value);
		EditorGUILayout.EndHorizontal ();

	}
}
