using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

namespace PlayScene.WaveEmitterNode{

public class WEN_area_random : StageWaveEmitterNode {
	[Header("敵を設定する")]
	public GameObject[] enemys;
	[Header("左上、右下のポイント")]
	public Vector2 leftUpPoint;
	public Vector2 rightDwonPoint;
	[Header("確率0~1の間 0.1秒ごとに判定"),Range(0, 1)]
	public float prob;
	[Header("during秒の間召喚しつづける")]
	public float during;


	public override FunctionIEnumerator emit (){
		return Call;
	}

	IEnumerator Call() {
		Vector2 tmp = Vector2.zero;
		for(float f = 0; f< during; f += 0.1f){
			tmp.x = Random.Range(leftUpPoint.x, rightDwonPoint.x);
			tmp.y = Random.Range(rightDwonPoint.y, leftUpPoint.y);
			GameObject obj = enemys [Random.Range (0, enemys.Length)].gameObject;
			ObjectPool.instance.GetGameObject (obj, tmp, obj.transform.rotation);
			yield return new WaitForSeconds (0.1f);
		}
	}

}

}