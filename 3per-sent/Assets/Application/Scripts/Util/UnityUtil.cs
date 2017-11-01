using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate IEnumerator FunctionIEnumerator();//コールバック用.
public delegate void FunctionVoid();//コールバック用.

public class UnityUtil{
	public static T GetPrefub<T>(Component prefub, Transform transform)
		where T : Object
	{
		if (prefub.gameObject.scene.name == null) {
			GameObject o = Object.Instantiate (prefub.gameObject, transform.position, transform.rotation);
			o.transform.parent = transform;
			T c = o.GetComponent<T> ();
			return c;
		}  else {
			return prefub.GetComponent<T>();
		}

	}


}
