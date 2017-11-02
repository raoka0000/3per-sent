using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;


namespace PlayScene.WaveEmitterNode{

public class WEN_boss : StageWaveEmitterNode {

	[Header("BOSSを設定する.倒したらステージクリアとなる")]
	public  GameObject  enemy;
	private GameObject _enemy;
	public Vector2 pos;

	public override FunctionIEnumerator emit (){
		_enemy = ObjectPool.instance.GetGameObject (enemy.gameObject, pos, enemy.transform.rotation);
		return Call;
	}

	IEnumerator Call() {
		while(_enemy.activeSelf){
			yield return null;
		}
		EnvironmentEvent.instance.gameclear ();
	}
}

}
