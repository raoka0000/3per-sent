using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace PlayScene {

public class Player : Actor {
	private const float SCAN_STOPTIME = 0.5f;
	private const float HIT_INVINCIBLE_TIME = 0.6f;

	public SkillObject[] mySkill = new SkillObject[3];
	[System.NonSerialized]
	public bool IsScanMode = false;
	[System.NonSerialized]
	public float speed = DEFINE.PLAYER_SPEED_DEFULT;

	//初期化時の処理を実装
	protected override void Init(){
		
	}

	protected override void _Update () {
		this.Controller ();
	}

	//衝突時の判定.弾との衝突判定はActor.csに隠されている.
	protected override void _OnTriggerEnter2D (Collider2D c){
		string hitTagName = c.gameObject.tag;
		if(hitTagName == "Enemy"){
			if (status.ExistStatus (StatusType.muteki)) {
				Enemy enemy = c.gameObject.GetComponent<Enemy> ();
				enemy.Damaged (30);
			}else if (IsScanMode) {
				Enemy enemy = c.gameObject.GetComponent<Enemy> ();
				Scan (enemy);
			} else {
				EnvironmentEvent.instance.ShakeCamera (noise: true);
				this.status.AddStatus (StatusType.invincible, HIT_INVINCIBLE_TIME);
				this.Damaged (5);
			}
		}
		UiManager.instance.SetHp ();
	}
	public override void Hit (BulletEffect bullet){
		if (status.ExistStatus (StatusType.muteki))return;//ステータスが無敵ならリターン.

		EnvironmentEvent.instance.ShakeCamera (duration: 0.3f,strength: 0.4f,noise: true);
		this.status.AddStatus (StatusType.invincible, HIT_INVINCIBLE_TIME);
	}

	public void Scan(Enemy enemy){
		if (this.GetSkill (enemy.hasSkill)) {
			EnvironmentEvent.instance.DoSlow (SCAN_STOPTIME);
			enemy.Damaged (100);
			EnvironmentEvent.instance.DoAberration (SCAN_STOPTIME / 4f);
			AudioManager.instance.PlaySE ("don");
		} else {
			EnvironmentEvent.instance.ShakeCamera (noise: true);
			this.status.AddStatus (StatusType.invincible, HIT_INVINCIBLE_TIME);
			this.Damaged (1);
		}
		this.status.AddStatus (StatusType.invincible, SCAN_STOPTIME/4f);
		ChangeScanMode (SCAN_STOPTIME);
	}

	void Controller(){
		//ショット
		if (Input.GetKeyDown (KeyCode.Space))bulletUnits.Shot (0);
		//モードチェンジ
		if (Input.GetKeyDown(KeyCode.Z))ChangeScanMode ();
		//スキル発動
		if (Input.GetKeyDown (KeyCode.X))UseSkill (0);//ヘッド

		if (Input.GetKeyDown (KeyCode.C)) {
			this.status.AddStatus (StatusType.invincible, 1f);
		}
		if (Input.GetKeyDown (KeyCode.V))UseSkill (2);//テール

		//速度変更
		if(Input.GetKeyDown(KeyCode.LeftShift))this.speed = DEFINE.PLAYER_LOW_SPEED_DEFULT;
		if(Input.GetKeyUp(KeyCode.LeftShift))this.speed = DEFINE.PLAYER_SPEED_DEFULT;

		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");
		Vector2 direction = new Vector2 (x, y).normalized;
		Move (direction);
	}

	void Move(Vector2 direction){
		Vector2 pos = transform.position;
		pos += direction  * speed * myDeltaTime;
		transform.position = pos;
	}

	/*ScanModeに関すること*/
	public void ChangeScanMode(float shotDelay = 0){
		if (status.ExistStatus (StatusType.muteki))return;//ステータスが無敵ならリターン.
		IsScanMode = !IsScanMode;
		if (IsScanMode) {
			this.bulletUnits.Stop (0);
			this.sprite.color = Color.red;
		} else {
			DOVirtual.DelayedCall (shotDelay, ()=>bulletUnits.Shot (0));
			this.sprite.color = Color.white;
		}
	}

	/*BulletManagerに関すること*/
	public BulletManager AddBulletUnit(BulletManager bm, Skill.SkillType type){
		var b = this.bulletUnits.SetBulletManager (bm, (int)type + 1);//+1している理由は0番目は通常弾だから.
		return b;
	}

	/*スキルに関すること*/
	public bool GetSkill(SkillObject s){
		if (s == null) return false;
		UiManager.instance.SetSkillImage (s);
		mySkill [(int)s.skillType] = s;
		return true;
	}
	public void UseSkill(int id){
		if (mySkill [id] == null) return;
		UiManager.instance.LossSkillImage ();
		UseSkill (mySkill [id]);
		LossSkill (id);
	}
	public void UseSkill(SkillObject s){
		s.UseSkill (this.gameObject);
	}
	public void LossSkill(int id){
		mySkill [id] = null;
	}


}

}