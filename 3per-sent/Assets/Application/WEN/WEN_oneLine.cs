using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

namespace PlayScene.WaveEmitterNode{

public class WEN_oneLine : StageWaveEmitterNode {
	[Header("敵を設定する")]
	public GameObject enemy;
	[Header("2点の間に敵を出す")]
	public Vector2 stratPoint;
	public Vector2 endPoint;
	[Header("countの数だけ召喚")]
	public int count;
	[Header("始点の敵は0秒に発生,終点の敵はduring秒に召喚")]
	public float during;


	public override FunctionIEnumerator emit (){
		return Call;
	}
		
	IEnumerator Call() {
		if (count <= 2) {
			ObjectPool.instance.GetGameObject (enemy.gameObject, stratPoint, enemy.gameObject.transform.rotation);
			if (count == 2) ObjectPool.instance.GetGameObject (enemy.gameObject, endPoint  , enemy.gameObject.transform.rotation);
			yield break;
		}

		ObjectPool.instance.GetGameObject (enemy.gameObject, stratPoint, enemy.gameObject.transform.rotation);
		ObjectPool.instance.GetGameObject (enemy.gameObject, endPoint  , enemy.gameObject.transform.rotation);

		Vector2 tmp = Vector2.zero;
		float x = (endPoint.x - stratPoint.x) / ((float)count - 1f);
		float y = (endPoint.y - stratPoint.y) / ((float)count - 1f);
		float time = during / (float)count;

		for(int i = 1; i<count - 1; i++){
			tmp.x = stratPoint.x + (x * i);
			tmp.y = stratPoint.y + (y * i);
			ObjectPool.instance.GetGameObject (enemy.gameObject, tmp, enemy.gameObject.transform.rotation);
			yield return new WaitForSeconds (time);
		}
	}

}

}