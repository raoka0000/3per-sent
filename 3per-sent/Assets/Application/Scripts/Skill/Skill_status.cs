using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene.Skills {

public class Skill_status : Skill {
	public StatusType status;
	public float _duration;

	public override void Init(){
	}

	public override void StartEfect(){
		player.status.AddStatus (status, _duration);
	}

}

}