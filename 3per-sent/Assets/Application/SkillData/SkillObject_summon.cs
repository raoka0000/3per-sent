using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene.Skills;


public class SkillObject_summon : SkillObject {
	public GameObject friend;
	public float duration;
	public override void UseSkill (GameObject obj){
		var a = obj.AddComponent<Skill_summon> ();
		a.friend = friend;
		a.duration = duration;
	}
}
