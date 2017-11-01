using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene.Skills {

public class Skill_summon : Skill {
	public GameObject friend;
	private Actor friendActor;

	public override void Init(){
		
	}

	public override void StartEfect(){
		ParticleManager.instance.kirakiraEffect (this.gameObject.transform.position);
		friendActor = ObjectPool.instance.GetGameObject(friend, this.gameObject.transform.position, this.gameObject.transform.rotation).GetComponent<Actor>();
	}
	//スキル継続時
	public override void UpdateEfect(){
		if(!friendActor.gameObject.activeSelf){
			restDuration = -1;
		}
	}

	public override void EndEfect(){
		friendActor.Damaged (friendActor.maxHp);
	}

}

}