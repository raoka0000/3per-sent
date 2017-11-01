using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;
using DG.Tweening;

namespace PlayScene.BulletEffects{

public class BE_bomb : BulletEffect {
	[SerializeField]
	private float startScale = 0.1f;
	[SerializeField]
	private float endScale = 1.0f;
	[SerializeField]
	private float time = 1.0f;

	private Color startColor;

	private Sequence sequence;
	private SpriteRenderer sprite;
	public override void Init(){
		if (sprite == null) {
			sprite = this.gameObject.GetComponent<SpriteRenderer> ();
			startColor = sprite.color;
		} 
		if(sequence != null) {
			sequence.Kill ();
		}
		sprite.color = startColor;
		this.transform.localScale = new Vector3 (startScale,startScale);
		sequence = DOTween.Sequence ();
		sequence.Append (this.transform.DOScale (endScale, time).SetEase(Ease.OutQuart));
		sequence.Join (sprite.DOFade(0,time).SetEase(Ease.InQuart));
		sequence.OnComplete (() => this.Break ());


	}

	public override void Hit(Actor actor){
		actor.Damaged (_attack);
		actor.Hit (this);
	}

	public override void Break(){
		this.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D (Collider2D c){
		string hitTagName = c.gameObject.tag;
		//弾が当たった時の処理
		if(TagUtil.IsOpponentTag(this.gameObject.tag, hitTagName)){
			BulletEffect hitBullet = c.gameObject.GetComponent<BulletEffect> ();
			hitBullet.Break();
		}
	}

}

}
