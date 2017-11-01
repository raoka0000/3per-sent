using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace PlayScene {

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (Status))]


/*状態異常など、敵とプレイヤーで共通の処理を行う基底クラス*/
public abstract class Actor : MonoBehaviour {


	[SerializeField]
	protected int _hp;
	public int hp { get{return _hp;} set{setHp(value);} }
	[SerializeField]
	protected int _maxHp = 1;
	public int maxHp { get {return _maxHp;} set{this._maxHp = value;} }

	[System.NonSerialized]
	public float timeScale = 1.0f;
	[System.NonSerialized]
	public float myTimeScale; 
	[System.NonSerialized]
	public float myDeltaTime; 


	[HideInInspector]
	public BulletManagerUnit bulletUnits;

	[HideInInspector]
	public Status status;

	[HideInInspector]
	public SpriteRenderer sprite;

	//初期化時の処理を実装
	void Start() {
		_Init ();
	}
	void OnEnable() {
		_Init ();
	}
	void _Init(){
		if (bulletUnits == null) {
			bulletUnits = this.gameObject.GetComponent<BulletManagerUnit> ();
		}
		if(sprite == null){
			this.sprite = this.gameObject.GetComponent<SpriteRenderer> ();
		}
		if (status == null) {
			this.status = this.gameObject.GetComponent<Status> ();
			this.status.actor = this;
		}
		hp = maxHp;
		gameObject.SetActive(true);
		Init ();
	}
	protected virtual void Init (){}

	//休眠時の処理を実装
	public void Sleep(){
		_Sleep ();
		bulletUnits.Stop ();
		gameObject.SetActive(false);
	}
	public virtual void killed(){}
	protected virtual void _Sleep (){}

	//毎フレームごとの処理
	void Update () {
		SetMytimeScale();
		_Update ();
	}
	protected virtual void _Update (){}

	void SetMytimeScale(){
		this.myTimeScale = Time.timeScale * timeScale;
		this.myDeltaTime = Time.deltaTime * timeScale;
	}

	//当たり判定について.
	protected virtual void _OnTriggerEnter2D (Collider2D c){}
	void OnTriggerEnter2D (Collider2D c){
		//今の状態がインビジブルなら当たり判定は計算しない.
		if(status.ExistStatus(StatusType.invincible)) return;
		string hitTagName = c.gameObject.tag;
		//弾が当たった時の処理
		if(TagUtil.IsHitBulletTag(this.gameObject.tag,hitTagName)){
			HitBullet (c);
		}
		_OnTriggerEnter2D(c);
	}
	public virtual void HitBullet(Collider2D c){
		BulletEffect hitBullet = c.gameObject.GetComponent<BulletEffect> ();
		hitBullet.Hit (this);
	}

	public virtual void Hit (BulletEffect bullet){
		
	}


	//ダメージを受けた時の処理.
	public int Damaged(int attack){
		if (status.ExistStatus(StatusType.muteki))
			return 0;
		int damage = attack;
		this.hp -= damage;
		if(hp <= 0){
			this.killed ();
			this.Sleep();
		}
		return damage;
	}

	protected virtual void setHp(int n){
		if(n > maxHp){
			n = maxHp;
		}else if(n < 0){
			n = 0;
		}
		this._hp = n;
		return;
	}


}

}
