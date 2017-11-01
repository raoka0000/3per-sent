using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene.Skills {

public class Skill_bullet : Skill {
	public BulletManager bullet;

	public override void Init(){
	}

	public override void StartEfect(){
		var b = player.AddBulletUnit (this.bullet, skillType);
		b.Shot ();
	}

}

}