using System;
using UnityEngine;

//シングルトン継承元クラス.
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour{
	private static T _instance;
	public static T instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<T> ();
				if (_instance == null) {
					//自身のクラス名を取得する.
					string self = new System.Diagnostics.StackFrame().GetMethod().DeclaringType.GetGenericArguments()[0].Name;
					//クラス名のゲームオブジェクトを生成する
					_instance = new GameObject (self).AddComponent<T> ();
				}
			}

			return _instance;
		}
	}

}