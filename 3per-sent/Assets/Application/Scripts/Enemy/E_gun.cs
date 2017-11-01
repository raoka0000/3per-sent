using UnityEngine;
using DG.Tweening;
using PlayScene;

namespace PlayScene.Enemys{

public class E_gun : Enemy {
	[SerializeField]
	private float shotInterval = 5;

	[SerializeField]
	private float length = -20;


	protected override void Init(){
		if(bulletUnits != null)bulletUnits.Shot ();
		this.transform.DOLocalMoveX(length,10f).SetRelative().timeScale = timeScale;
		this.transform.DOLocalMoveY(0.15f,1.5f).SetRelative().SetLoops (-1, LoopType.Yoyo).timeScale = timeScale;
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,15);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,0);
	}
	protected override void _Sleep (){
		this.transform.DOKill ();
	}
	private float time = 0;
	protected override void _Update () {
		time += myDeltaTime;
		if(time > shotInterval){
			bulletUnits.Shot ();
			time = 0;
		}

	}


}

}