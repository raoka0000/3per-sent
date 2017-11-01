using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

using PlayScene;

namespace PlayScene.Enemys{

public class E_sanway : Enemy {

	protected override void Init(){
		this.transform.DOLocalMoveX(8f,3f).OnComplete(() => {
			bulletUnits.Shot (0);
		}).timeScale = timeScale;
		this.transform.DOLocalMoveY(0.5f,0.5f).SetLoops (-1, LoopType.Yoyo).timeScale = timeScale;
		this.transform.DOPunchScale (new Vector3(0.05f,0.05f,0), 0.5f, 10).SetLoops (-1, LoopType.Yoyo);
	}

	protected override void _Sleep (){
		this.transform.DOKill ();
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,50);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,0);
	}



}

}