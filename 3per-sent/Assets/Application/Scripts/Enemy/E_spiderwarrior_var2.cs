using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PlayScene;
namespace PlayScene.Enemys{

public class E_spiderwarrior_var2 : Enemy{
	[SerializeField]
	private float shotInterval = 5;


	[SerializeField]
	private float speed_y = -4.0f;
	[SerializeField]
	private float wait_time =0.0f;
	private float st_speed = 0;

	protected override void Init (){
		st_speed = speed_y;
		/*this.transform.DOLocalMoveY(speed_y,3f).OnComplete(()=>{
			Debug.Log("speedy " + speed_y);
			bulletUnits.Shot (0);
			speed_y*=-1;
		}).timeScale = timeScale;*/
	}

	protected override void _Sleep (){
		this.transform.DOKill ();
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,50);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,0);
	}
	protected override void _Update(){		
		Debug.Log("st_speed:"+st_speed);
		this.transform.Translate(0,speed_y*myDeltaTime,0);
		//Debug.Log(this.transform.position.y);
		Debug.Log("speed_y:"+speed_y);
		if(this.transform.position.y<0){
			speed_y=0.0f;
			wait_time+=myDeltaTime;
			if(wait_time>=1.0f){
				speed_y=st_speed*-1;
				Debug.Log(speed_y);
			}
	}
}
}
}