using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

namespace PlayScene.Enemys{

public class E_heal : Enemy {

	private GameObject target;
	private float angle = 0;
	private Vector3 tmp = Vector3.zero;
	private float speed = 1;

	protected override void Init(){
		
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,15);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,DEFINE.SHOT_SE_CH);
	}
	private float time = 0;
	protected override void _Update () {
		if (target == null) {
			target = SetTarget (this.tag);
		} else {
			angle = GetAim (target);
			tmp.x = Mathf.Cos (angle * Mathf.Deg2Rad);
			tmp.y = Mathf.Sin (angle * Mathf.Deg2Rad);
			transform.position += (tmp * speed * myDeltaTime);
		}
		time += myDeltaTime;
		if(time > 1f){
			bulletUnits.Shot (0);
			time = 0;
		}
	}

	public GameObject SetTarget(string targetTagName) {
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag(targetTagName)){
			if(obj != this.gameObject && obj.GetComponent<E_heal>() == null){
				return obj;
			}
		}
		return null;
	}

	private float GetAim(GameObject tar) {
		float dx = tar.transform.position.x - this.transform.position.x;
		float dy = tar.transform.position.y - this.transform.position.y;
		float rad = Mathf.Atan2(dy, dx);
		return rad * Mathf.Rad2Deg;
	}


}

}