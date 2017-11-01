using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene.BulletEffects{

public class BE_healBomb : BE_bomb {

	public override void Hit(Actor actor){}

	private void Heal(Actor actor){
		actor.hp += _attack;
	}

	void OnTriggerEnter2D (Collider2D c){
		//弾が当たった時の処理
		if(TagUtil.IsFriendTag(this.gameObject.tag, c.gameObject.tag)){
			Actor actor = c.gameObject.GetComponent<Actor> ();
			if (actor == null) return;
			Heal (actor);
		}

	}

}

}