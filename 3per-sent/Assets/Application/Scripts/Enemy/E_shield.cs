using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

using PlayScene;

namespace PlayScene.Enemys{

public class E_shield : Enemy {
	
	protected override void Init(){
		this.transform.DOLocalMoveX(-75,150f).timeScale = timeScale;
		this.transform.DOPunchScale (new Vector3(0.05f,0.05f,0), 0.5f, 10).SetLoops (-1, LoopType.Yoyo);
	}

	public override void Hit (BulletEffect bullet){
		Vector3 pos = bullet.gameObject.transform.position;
		ParticleManager.instance.reflectionEffect (pos, 1);
		if(bulletUnits != null)bulletUnits.Shot (0);
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