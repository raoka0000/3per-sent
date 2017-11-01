using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StatusType{
	invincible = 1<<0,
	muteki     = 1<<1,
}



namespace PlayScene {

public class Status : MonoBehaviour {
	[HideInInspector]
	public Actor actor;
	private int _status = 0;
	public int status{ 
		get{return _status;} 
	}

	private Dictionary<StatusType, float> _statusTime;
	private Dictionary<StatusType, float> statusTime{
		get { 
			if(_statusTime == null){
				_statusTime = new Dictionary<StatusType, float> ();
				foreach(StatusType t in Enum.GetValues(typeof(StatusType))){
					_statusTime.Add (t, 0);
				}
			}
			return _statusTime;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public bool ExistStatus(StatusType type){
		return ((status & (int)type) != 0);
	}
	public void AddStatus(StatusType type){
		_status = _status | (int)type;
	}
	public void LossStatus(StatusType type){
		_status = _status & ~(int)type;
	}


	public void AddStatus(StatusType type, float time){
		switch (type){
		case StatusType.invincible:
			StartCoroutine(DoSwitchableStatus(
				time,
				type,
				//Before処理
				() =>{
					var color = actor.sprite.color;
					color.a = 0.5f;
					actor.sprite.color = color;
				},
				//after処理
				() => {
					var color = actor.sprite.color;
					color.a = 1.0f;
					actor.sprite.color = color;
				}
			)
			);
			break;
		case StatusType.muteki:
			//StartCoroutine("Muteki",time);
			StartCoroutine(DoSwitchableStatus(
				time,
				type,
				//Before処理
				() =>{
					if (actor is Player) {
						Player player = actor as Player;
						player.sprite.color = Color.yellow;
						if(player.IsScanMode){
							player.ChangeScanMode();
						}

					}
				},
				//after処理
				() => {
					if (actor is Player) {
						actor.sprite.color = Color.white;
					}
				}
			)
			);
			break;
		default:
			break;
		}
		return;
	}


	IEnumerator DoSwitchableStatus(float duration, StatusType type, UnityAction before, UnityAction after){
		statusTime[type] += duration;
		if (this.ExistStatus (type)) yield break;

		//開始処理
		AddStatus (type);
		before();

		float t = statusTime[type];
		for(;statusTime[type] > 0;statusTime[type] -= t){
			t = statusTime[type];
			yield return new WaitForSeconds (t);
		}

		//終了処理
		after();
		LossStatus (type);

	}


}

}