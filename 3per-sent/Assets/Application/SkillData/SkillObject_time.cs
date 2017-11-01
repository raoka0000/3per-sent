using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;
using PlayScene.Skills;


public class SkillObject_time : SkillObject {
	public float duration;
	public override void UseSkill (GameObject obj){
		var a = obj.AddComponent<Skill_time> ();
		a._duration = duration;
	}
}