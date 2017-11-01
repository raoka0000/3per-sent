using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

using PlayScene;

namespace PlayScene.Enemys{

public class E_tatupon : Enemy {

	Sequence seq;
	Vector3 step1 = new Vector3 (-5, 0, 0);
	Vector3 step2 = new Vector3 (0, 0, -30);
	Vector3 step3 = new Vector3 (0, 0, 360);
	protected override void Init(){
		seq.Kill ();

		seq = DOTween.Sequence();
		seq.Append (
			this.transform.DOMove(step1, 1f).SetRelative()
		);
		seq.Append (
			this.transform.DORotate(step2, 1f).OnStepComplete(
				() =>{
					bulletUnits.Shot(0);
				}
			)
		);
		seq.Append (
			this.transform.DORotate(step3, 1f).SetRelative()
			.OnStepComplete(
				() =>{
					bulletUnits.Stop();
				}
			)
		);
		seq.SetLoops (-1, LoopType.Incremental);



	}

	protected override void _Sleep (){
		//seq.Kill ();
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,50);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,0);
	}



}

}