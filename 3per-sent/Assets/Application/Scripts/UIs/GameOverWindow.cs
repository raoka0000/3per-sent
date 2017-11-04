using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PlayScene{

public class GameOverWindow : MonoBehaviour {

	public RectTransform retry;
	public RectTransform giveup;
	private int _cursor = 0;//0がリトライ、1がギブアップ
	public int cursor{
		get{ 
			return _cursor;
		}
		set{
			setcursor (value);
		}
	}
	void Awake() {
		cursor = 0;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			Debug.Log ("右");
			cursor = 0;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			Debug.Log ("左");
			cursor = 1;
		}
		if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
			if (cursor == 0) {
				GotoRetry ();
			} else {
				GotoTitle ();
			}
		}
	}
	
		public void GotoRetry(){
			EnvironmentEvent.instance.GotoRetry ();
		}

		public void GotoTitle(){
			EnvironmentEvent.instance.GotoTitle ();
		}

	void setcursor(int i){
		_cursor = i;
		if (_cursor == 0) {
			giveup.DOKill ();
			giveup.DOScale (Vector3.one, 0.3f);
			retry.DOScale (new Vector2 (0.3f, 0.3f), 0.3f).SetRelative ().SetLoops (-1, LoopType.Yoyo);
		} else {
			retry.DOKill ();
			retry.DOScale (Vector3.one, 0.3f);
			giveup.DOScale (new Vector2 (0.3f, 0.3f), 0.3f).SetRelative ().SetLoops (-1, LoopType.Yoyo);
		}
	}
}

}