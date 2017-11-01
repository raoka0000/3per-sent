using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

public abstract class SkillObject : ScriptableObject {
	public Sprite image;
	public Skill.SkillType skillType;
	public abstract void UseSkill (GameObject obj);

}
