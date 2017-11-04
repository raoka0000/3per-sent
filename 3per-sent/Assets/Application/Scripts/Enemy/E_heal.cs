using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

namespace PlayScene.Enemys{

public class E_heal : Enemy {

	private GameObject target;
	private float angle = 0;
	private Vector3 tmp = Vector3.zero;
	[SerializeField]
	private float speed = 1;
	[SerializeField]
	private float shotInterval = 1;


	protected override void Init(){
		
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,15);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,DEFINE.SHOT_SE_CH);
	}
	private float time = 0;
	private Vector3 tmpvec = Vector3.one;
	protected override void _Update () {
		if (target == null || !target.activeSelf) {
			target = SetTarget (TagUtil.GetFriendTag(this.tag));
		} else {
			tmpvec = target.transform.position;
			tmpvec.x -= 1;
			tmpvec.y += 1;
			angle = GetAim (tmpvec);
			tmp.x = Mathf.Cos (angle * Mathf.Deg2Rad);
			tmp.y = Mathf.Sin (angle * Mathf.Deg2Rad);
			tmpvec = transform.position + (tmp * speed * myDeltaTime);
			if (Vector3.Distance (tmpvec, target.transform.position) > 3.0f) {
				transform.position = tmpvec;
			}
		}
		time += myDeltaTime;
		if(time > shotInterval){
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

	private float GetAim(Vector3 vec) {
		float dx = vec.x - this.transform.position.x;
		float dy = vec.y - this.transform.position.y;
		float rad = Mathf.Atan2(dy, dx);
		return rad * Mathf.Rad2Deg;
	}


}

}