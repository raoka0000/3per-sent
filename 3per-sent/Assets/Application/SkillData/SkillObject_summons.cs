using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;
using PlayScene.Skills;

public class SkillObject_summons : SkillObject {
	public GameObject friend;
	public int count = 1;
	public Vector2 leftUpPoint;
	public Vector2 rightDwonPoint;


	public override void UseSkill (GameObject obj){
		var a = obj.AddComponent<Skill_summons> ();
		a.friend = friend;
		a.count = count;
		a.leftUpPoint = leftUpPoint;
		a.rightDwonPoint = rightDwonPoint;
	}
}
