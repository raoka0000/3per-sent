using UnityEngine;
using DG.Tweening;
using PlayScene;

namespace PlayScene.Enemys{

public class E_HaneCoil : Enemy{
	[SerializeField]
	private float shotInterval = 5;

	[SerializeField]
	private float speed_x = -3.0f;
	[SerializeField]
	private float speed_y = 1;
	[SerializeField]
	private int attack = 1;

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

		this.transform.Translate(7.0f*speed_x*myDeltaTime,5.0f*speed_y*myDeltaTime,0);
		if(this.transform.position.y<-5.0f||this.transform.position.y>5.0f){
			speed_y*=-1;
		}
	}

	protected override void _OnTriggerEnter2D(Collider2D c){
		Debug.Log(c.tag);
		if(TagUtil.IsOpponentTag(this.tag,c.tag)){
			
			Actor actor = c.GetComponent<Actor>();
			if (actor is Enemy) {
				actor.Damaged(attack);
				this.Damaged(maxHp);
			}
		}
	}

}

}
