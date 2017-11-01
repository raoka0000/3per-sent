using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene {

public abstract class Skill : MonoBehaviour {
	[HideInInspector]
	public enum SkillType{
		head,
		body,
		tail
	}
	public SkillType skillType;
	[HideInInspector]
	public Player player;

	public float duration = 0;//効果時間
	[HideInInspector]
	public float restDuration = 0;//残りの効果時間

	// Use this for initialization
	void Start () {
		player = this.gameObject.GetComponent<Player> ();
		Init ();
		restDuration = duration;
		StartEfect ();

	}
	
	// Update is called once per frame
	void Update () {
		UpdateEfect ();
		restDuration -= Time.deltaTime;
		if (restDuration < 0) {
			EndEfect ();
			Loss ();
		}
	}
	public virtual void SetDuration(float f){
		duration = f;
		restDuration = duration;
	}
	//初期化処理
	public virtual void Init(){}
	//スキル開始時
	public virtual void StartEfect(){}
	//スキル継続時
	public virtual void UpdateEfect(){}
	//スキル消滅時
	public virtual void EndEfect(){}
	//スキル喪失処理
	public virtual void Loss(){
		Destroy (this);
	}


}

}
