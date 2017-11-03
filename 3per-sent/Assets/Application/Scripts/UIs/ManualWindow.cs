using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

//クソ雑
public class ManualWindow : MonoBehaviour {
	public RectTransform[] images;
	private int cou = 0;
	private bool canNext = true;
	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown && canNext){
			canNext = false;
			AudioManager.instance.PlaySE ("kami");
			images [cou].DOLocalMoveX (1300f, 0.8f).OnComplete(()=>{
				canNext = true;
				cou += 1;
				if (cou == images.Length) {
					SceneManager.LoadScene(DEFINE.TITLE_SCENE_NAME);
				}
			});
		}
	}

}
