using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;
using PlayScene.Skills;


public class SkillObject_status : SkillObject {
	public StatusType status;
	public float duration;
	public override void UseSkill (GameObject obj){
		var a = obj.AddComponent<Skill_status> ();
		a.status = status;
		a._duration = duration;
	}
}
