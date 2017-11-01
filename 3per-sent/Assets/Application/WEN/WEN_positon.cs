using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

namespace PlayScene.WaveEmitterNode{

public class WEN_positon : StageWaveEmitterNode {
	[System.Serializable]
	public class EnemyAndPositon{
		public GameObject enemy;
		public Vector2 pos;
	}

	public EnemyAndPositon[] eap;
	public override FunctionIEnumerator emit (){
		foreach (EnemyAndPositon e in eap) {
			ObjectPool.instance.GetGameObject (e.enemy.gameObject, e.pos, e.enemy.gameObject.transform.rotation);
		}
		return null;
	}
}

}