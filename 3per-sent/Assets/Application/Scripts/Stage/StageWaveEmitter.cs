using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayScene {

public class StageWaveEmitter : MonoBehaviour {
	[System.Serializable]
	public class SwenAndTime{
		public float delayTime;
		public StageWaveEmitterNode swen;
		public UnityEvent action;
	}
	public SwenAndTime[] sat;

	void Start() {
		StartCoroutine(Emit());
	}

	IEnumerator Emit() {
		foreach(SwenAndTime item in sat){
			FunctionIEnumerator func = item.swen.emit ();
			if(func != null){
				StartCoroutine (func ());
			}
			yield return new WaitForSeconds (item.delayTime);
		}
	}

}

}
