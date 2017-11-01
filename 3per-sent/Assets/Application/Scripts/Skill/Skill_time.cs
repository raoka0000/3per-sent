using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene.Skills {

public class Skill_time : Skill {
	public float _duration;

	public override void Init(){
	}

	public override void StartEfect(){
		EnvironmentEvent.instance.DoSlow (_duration, true, false);
	}

}

}