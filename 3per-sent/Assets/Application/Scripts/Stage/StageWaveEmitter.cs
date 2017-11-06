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
	public int startNumber = 0;
	public SwenAndTime[] sat;

	void Start() {
		StartCoroutine(Emit());
	}

	IEnumerator Emit() {
		for(int i = startNumber; i<sat.Length; i++){
			if (sat[i].swen != null) {
				FunctionIEnumerator func = sat[i].swen.emit ();
				if (func != null) {
					StartCoroutine (func ());
				}
				yield return new WaitForSeconds (sat[i].delayTime);
			}

		}
	}

}

}
