using UnityEngine;
using DG.Tweening;
using PlayScene;

namespace PlayScene.Enemys{

public class E_HaneCoil : Enemy{
	[SerializeField]
	private float shotInterval = 5;

	[SerializeField]
	private float speed_x = -3.0f;
	private float speed_y = 1;

	protected override void Init (){
		
	}

	public override void killed (){
		ParticleManager.instance.HitEffect (transform, 15);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE, 0);
	}

	protected override void _Sleep (){
		this.transform.DOKill ();
	}

	private float time = 0;

	protected override void _Update (){
		
		time += myDeltaTime;
		if (time > shotInterval) {
			bulletUnits.Shot ();
			time = 0;
		}

		this.transform.Translate(-1.0f,1.0f*speed_y,0);
		if(this.transform.position.y<-5.0f||this.transform.position.y>5.0f){
			speed_y*=-1;
		}

		Debug.Log(this.transform.position.y);
	}

}

}
