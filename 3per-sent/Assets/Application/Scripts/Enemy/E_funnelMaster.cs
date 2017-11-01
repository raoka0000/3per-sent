using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

using PlayScene;

namespace PlayScene.Enemys{

public class E_funnelMaster : Enemy {

	[SerializeField]
	GameObject[] funnnel;

	private int state = 0;
	private Vector3 defPos = new Vector3(8f,0f,0f);
	protected override void Init(){
		firstAction ();
	}

	private void firstAction(){
		this.transform.DOLocalMove(defPos,3f).OnComplete(() => {
			bulletUnits.Shot (0);
			this.transform.DOLocalMoveY(0.5f,0.5f).SetLoops (-1, LoopType.Yoyo).timeScale = timeScale;
		}).timeScale = timeScale;
		this.transform.DOPunchScale (new Vector3(0.05f,0.05f,0), 0.5f, 10).SetLoops (-1, LoopType.Yoyo);
	}
	private float mp = 0;
	private GameObject target;
	protected override void _Update () {
		if (state == 0) {
			this.transform.localScale = Vector3.one * 2;
			mp += myDeltaTime;
			if (mp > 30) {
				mp -= 30;
				state = Random.Range (1, 4);
			}
		} else if (state == 1) {
			state = 0; 
			this.bulletUnits.Stop();
			this.transform.DOKill ();
			this.transform.DOPunchScale (new Vector3 (1.5f, 2.5f, 0), 0.5f, 2).OnComplete (() => {
				summon (Random.Range (1, 10));
				this.transform.DOKill ();
				firstAction ();
			});
		} else if (state == 2) {
			state = 0; 
			this.bulletUnits.Stop();
			bulletUnits.Shot (1);
			DOVirtual.DelayedCall (12f, () => bulletUnits.Shot (0));
		} else if (state == 3) {
			state = 0;
			this.transform.DOKill ();
			target = SetTarget (TagUtil.GetHomingActorTag(this.tag));
			this.transform.DOLocalMove(target.transform.position,2.5f).SetEase(Ease.InElastic).OnComplete(() => {
				firstAction();
			});
		}
	}

	private void summon(int n){
		for (int i = 0; i < n; i++) {
			Vector3 vec = this.gameObject.transform.position;
			float t = Random.Range (0, Mathf.PI * 2);
			vec += new Vector3 (Mathf.Cos (t) * 3, Mathf.Sin (t) * 3, 0);
			ParticleManager.instance.kirakiraEffect (vec);
			int id = Random.Range(0, funnnel.Length);
			ObjectPool.instance.GetGameObject (funnnel[id], vec, funnnel[id].transform.rotation);
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


	public override void Hit (BulletEffect bullet){
		mp += 1;
	}

	protected override void _Sleep (){
		this.transform.DOKill ();
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,500);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,0);
	}

	void OnBecameInvisible(){
	}


}

}