using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using Novel;

namespace PlayScene {


public class EnvironmentEvent : SingletonMonoBehaviour<EnvironmentEvent> {
	private GameObject _cameraObject;
	public GameObject cameraObject{
		get {
			if(_cameraObject == null){
				_cameraObject = GameObject.FindWithTag ("MainCamera");
			}
			return _cameraObject;
		}
	}
	private Player _player;
	public Player player{
		get {
			if(_player == null){
				GameObject obj = GameObject.FindWithTag ("Player");
				_player = obj.GetComponent (typeof(Player)) as Player;
			}
			return _player;
		}
		set {
			_player = value;
		}
	}


	public void gameovar(){
		//ゲームオーバー処理
		Debug.Log("Game Ovar");
		DOVirtual.DelayedCall (1.0f,()=>{
			NoiseAndScratches nas = cameraObject.GetComponent<NoiseAndScratches> ();
			VignetteAndChromaticAberration vaa = cameraObject.GetComponent<VignetteAndChromaticAberration> ();
			nas.enabled = true;
			nas.grainIntensityMin = 5;
			nas.grainIntensityMax = 5;
			vaa.enabled = true;
			UiManager.instance.ShowGameOverWindow();
		});
	}
	public void gameclear(){
		Debug.Log ("おめです");
		GotoJoker ();
		//SceneManager.LoadScene ("Player");
	}

	public void GotoTitle(){
		//SceneManager.LoadScene(DEFINE.TITLE_SCENE_NAME);
		AudioManager.instance.kill ();
		NovelSingleton.StatusManager.callJoker("wide/title","");
	}
	public void GotoRetry(){
		AudioManager.instance.kill ();
		SceneManager.LoadScene(SceneManager.GetActiveScene ().name);
	}


	public void GotoJoker(){
		AudioManager.instance.kill ();
		string s = JokerUtil.GetNextJokerScene (SceneManager.GetActiveScene ().name);
		//Debug.Log (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
		if (s != null) {
				if(s == "wide/scene2"){
					NovelSingleton.StatusManager.callJoker("wide/scene2","");
				}else if(s == "wide/scene3"){
					NovelSingleton.StatusManager.callJoker("wide/scene3","");
				}else if(s == "wide/scene4"){
					NovelSingleton.StatusManager.callJoker("wide/scene4","");
				}
		}
	}

	//カメラの揺れ
	public void ShakeCamera(float duration = 0.5f,float strength = 1.0f,int vibrato = 50,bool noise = false){
		if (noise) {
			cameraObject.GetComponent<NoiseAndScratches> ().enabled = true;
			cameraObject.transform.DOShakePosition (duration, strength, vibrato).OnComplete (
				() => {
					_cameraObject.GetComponent<NoiseAndScratches> ().enabled = false;
				}
			);
		} else {
			cameraObject.transform.DOShakePosition (duration, strength, vibrato);
		}
	}

	//スローモション演出
	private float slowDuration = 0;
	private bool isSlow = false;

	public void DoSlow(float t, bool blur = false, bool BGM = false){
		if(blur)cameraObject.GetComponent<MotionBlur> ().enabled = true;
		if (BGM) {
			AudioManager.instance.SetPitchBGM (0.8f);
		}
		slowDuration += t;
		if(!isSlow) StartCoroutine ("Slow");
	}

	private IEnumerator Slow() {
		const float scale = 0.2f;
		const float _scale = 1.0f / scale;
		isSlow = true;
		// コルーチンの処理
		Time.timeScale = scale;
		float t = slowDuration;
		for (; slowDuration > 0; slowDuration -= t) {
			t = slowDuration;
			yield return new WaitForSeconds (t  * scale);
		}
		Time.timeScale = 1.0f;
		cameraObject.GetComponent<MotionBlur> ().enabled = false;
		AudioManager.instance.SetPitchBGM (1);
		isSlow = false;
	}

	private bool isAberration = false;
	public void DoAberration(float _t){
		if (isAberration) return;
		float t = _t / 2;
		isAberration = true;
		var vaa = cameraObject.GetComponent<VignetteAndChromaticAberration> ();
		vaa.enabled = true;
		Sequence sequence = DOTween.Sequence ();
		sequence.Append (
			DOTween.To(
				() => vaa.chromaticAberration,          // 初期値
				num => vaa.chromaticAberration = num,   // 値の更新
				30,                  					// 最終的な値
				t                 					    // アニメーション時間
			)
		);
		sequence.Append (
			DOTween.To(
				() => vaa.chromaticAberration,          // 初期値
				num => vaa.chromaticAberration = num,   // 値の更新
				0,                  					// 最終的な値
				t                 					    // アニメーション時間
			)
		);
		sequence.OnComplete (() => {
			vaa.enabled  = false;
			isAberration = false;
		});

	}

}


}