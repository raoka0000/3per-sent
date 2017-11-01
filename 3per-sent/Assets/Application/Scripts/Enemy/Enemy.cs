using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PlayScene {

public class Enemy : Actor {
	public SkillObject hasSkill;


	//初期化時の処理を実装
	protected override void Init(){}

	protected override void _Update () {}

	void OnBecameInvisible(){
		this.Sleep ();
	}


}

}
