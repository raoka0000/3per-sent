using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene {

public abstract class BulletEffect : MonoBehaviour {
	[SerializeField]
	protected int  attack = 1;
	protected int _attack = 1;


	void OnEnable(){
		_attack = attack;
		this.Init ();
	}
	public virtual void Init (){}

	public abstract void Hit (Actor actor);

	public virtual void Break(){
		this.gameObject.SetActive (false);
	}

}
}