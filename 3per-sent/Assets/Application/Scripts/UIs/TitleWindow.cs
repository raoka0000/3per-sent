using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Novel;

public class TitleWindow : MonoBehaviour {

	public RectTransform[] buttons = new RectTransform[3];//0:開始,1,説明,2物語モード;

	private int _cursor = 0;
	public int cursor{
		get{ 
			return _cursor;
		}
		set{
			int n =  value % buttons.Length;
			setcursor (n);
		}
	}

	// Use this for initialization
	void Start () {
		cursor = 0;
		AudioManager.instance.PlayBGM (DEFINE.STEGE1_BGM);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			cursor -= 1;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			cursor += 1;
		}

		if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
			if (cursor == 0) {
				GoGame ();
			} else if (cursor == 1) {
				GoManual ();
			} else if (cursor == 2) {
				GoStory ();
			}
		}

	}

	void GoGame(){
		DEFINE.isStoryMode = false;
		SceneManager.LoadScene(DEFINE.STAGE1_SCENE_NAME);
	}
	void GoManual(){
		NovelSingleton.StatusManager.callJoker("wide/libs/explain","");
	}
	void GoStory(){
		DEFINE.isStoryMode = true;
		NovelSingleton.StatusManager.callJoker("wide/scene1","");
	}

	void setcursor(int n){
		_cursor = n;
		for(int i = 0; i < buttons.Length; i++){
			if (i == _cursor) {
				buttons[i].DOScale (new Vector2 (0.3f, 0.3f), 0.3f).SetRelative ().SetLoops (-1, LoopType.Yoyo);
			} else {
				buttons[i].DOKill ();
				buttons[i].DOScale (Vector3.one, 0.3f);
			}
		}
	}

}
