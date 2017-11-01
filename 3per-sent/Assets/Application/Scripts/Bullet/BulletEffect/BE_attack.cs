using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

namespace PlayScene.BulletEffects{

public class BE_attack : BulletEffect {
	public override void Hit(Actor actor){
		actor.Damaged (_attack);
		actor.Hit (this);
		this.Break();
	}
	public override void Break(){
		ParticleManager.instance.HitEffect (transform);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE, DEFINE.SHOT_SE_CH);
		this.gameObject.SetActive (false);
	}

}

}
