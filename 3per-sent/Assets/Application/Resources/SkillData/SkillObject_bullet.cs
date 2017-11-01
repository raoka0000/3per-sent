using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;
using PlayScene.Skills;


public class SkillObject_bullet : SkillObject {
	public BulletManager bullet;
	public override void UseSkill (GameObject obj){
		var a = obj.AddComponent<Skill_bullet> ();
		a.bullet = bullet;
	}
}
