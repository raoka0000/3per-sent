using UnityEngine;
using DG.Tweening;
using PlayScene;

namespace PlayScene.Enemys{

public class E_dora : Enemy {

	protected override void Init(){
		if(bulletUnits != null)bulletUnits.Shot (0);
		this.transform.DOLocalMoveX(-300f,150f).timeScale = timeScale;
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,15);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,0);
	}
	protected override void _Sleep (){
		this.transform.DOKill ();
	}
	protected override void _Update () {}


}

}