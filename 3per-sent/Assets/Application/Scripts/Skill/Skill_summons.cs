using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene.Skills {

public class Skill_summons : Skill {
	public GameObject friend;
	public int count = 1;
	public Vector2 leftUpPoint;
	public Vector2 rightDwonPoint;

	public override void Init(){

	}

	public override void StartEfect(){
		Vector2 tmp = Vector2.zero;
		for(int i = 0;i<count;i++){
			tmp.x = Random.Range(leftUpPoint.x, rightDwonPoint.x);
			tmp.y = Random.Range(rightDwonPoint.y, leftUpPoint.y);
			ParticleManager.instance.kirakiraEffect (tmp);
			ObjectPool.instance.GetGameObject(friend, tmp, this.gameObject.transform.rotation);
		}
	}

}

}