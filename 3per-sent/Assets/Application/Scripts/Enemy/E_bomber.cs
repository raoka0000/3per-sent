using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PlayScene;

namespace PlayScene.Enemys{

public class E_bomber : Enemy {

	private GameObject target;
	private float angle = 0;
	private Vector3 tmp = Vector3.zero;
	[SerializeField]
	private float speed = 1;
	[SerializeField]
	private float explosionInterval = 3f;
	[SerializeField]
	private float explosionRange = 3f;


	private int stete = 0;
	private Vector3 defaultScale = new Vector3(0.5f,0.5f,1);

	protected override void Init(){
		stete = 0;
		this.transform.localScale = defaultScale;
	}
	public override void killed(){
		this.transform.DOKill ();
		ParticleManager.instance.HitEffect (transform,15);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,DEFINE.SHOT_SE_CH);
	}
	private float time = 0;
	private Vector3 tmpvec = Vector3.one;
	protected override void _Update () {
		if (stete == 0) {
			if (target == null) {
				target = SetTarget (TagUtil.GetHomingActorTag(this.tag));
			} else {
				angle = GetAim (target.transform.position);
				tmp.x = Mathf.Cos (angle * Mathf.Deg2Rad);
				tmp.y = Mathf.Sin (angle * Mathf.Deg2Rad);
				tmpvec = transform.position + (tmp * speed * myDeltaTime);
				transform.position = tmpvec;
				if (Vector3.Distance (transform.position, target.transform.position) < explosionRange) {
					stete = 1;
				}
			}
		}
		if(stete == 1){
			tmp.x = 2;
			tmp.y = 2;
			this.transform.DOScale (tmp, explosionInterval).SetEase (Ease.InBounce)
				.OnKill(
					()=>{
						bulletUnits.Shot (0);
						DOVirtual.DelayedCall (0.01f, ()=>this.Damaged(this.maxHp));
					}
				);
			stete = 2;
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