using UnityEngine;
using UnityEditor;
using System.Collections;
using PlayScene;

[CustomEditor(typeof(BulletManagerUnit))]
public class BulletManagerUnitEditor : Editor {


	public override void OnInspectorGUI() {

		DrawDefaultInspector ();

		var obj = target as BulletManagerUnit;
		if(GUILayout.Button("Shot"))
		{
			obj.Shot();
		}
	}
}
