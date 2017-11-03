// http://kan-kikuchi.hatenablog.com/entry/AudioManager2
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BGMとSEの管理をするマネージャ。シングルトン。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	//オーディオファイルのパス
	private const string BGM_PATH = "Audio/BGM";
	private const string SE_PATH  = "Audio/SE";

	//BGMがフェードするのにかかる時間
	public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
	public const float BGM_FADE_SPEED_RATE_LOW  = 0.3f;
	private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

	//次流すBGM名
	private string _nextBGMName;

	//BGMをフェードアウト中か
	private bool _isFadeOut = false;

	//BGM用、SE用に分けてオーディオソースを持つ
	private AudioSource       _bgmSource;
	private List<AudioSource> _seSourceList;
	private const int SE_SOURCE_NUM = 4;

	//全AudioClipを保持
	private Dictionary<string, AudioClip> _bgmDic, _seDic;

	public void kill(){
		StopBGM ();
		//Destroy (instance);
		//_instance = null;
	}

	//=================================================================================
	//初期化
	//=================================================================================

	private void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);

		//オーディオリスナーおよびオーディオソースをSE+1(BGMの分)作成
		gameObject.AddComponent<AudioListener>();
		for(int i = 0; i < SE_SOURCE_NUM + 1; i++){
			gameObject.AddComponent<AudioSource>();
		}

		//作成したオーディオソースを取得して各変数に設定、ボリュームも設定
		AudioSource[] audioSourceArray = GetComponents<AudioSource> ();
		_seSourceList = new List<AudioSource> ();

		for(int i = 0; i < audioSourceArray.Length; i++){
			audioSourceArray [i].playOnAwake = false;

			if(i == 0){
				audioSourceArray [i].loop = true;
				_bgmSource = audioSourceArray [i];
				_bgmSource.volume = DEFINE.BGM_VOLUME_DEFULT;
			}
			else{
				_seSourceList.Add (audioSourceArray [i]);
				audioSourceArray [i].volume = DEFINE.SE_VOLUME_DEFULT;
			}

		}

		//リソースフォルダから全SE&BGMのファイルを読み込みセット
		_bgmDic = new Dictionary<string, AudioClip> ();
		_seDic  = new Dictionary<string, AudioClip> ();

		object[] bgmList = Resources.LoadAll (BGM_PATH);
		object[] seList  = Resources.LoadAll (SE_PATH);

		foreach (AudioClip bgm in bgmList) {
			_bgmDic [bgm.name] = bgm;
		}
		foreach (AudioClip se in seList) {
			_seDic [se.name] = se;
		}

	}

	//=================================================================================
	//SE
	//=================================================================================

	/// <summary>
	/// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
	/// </summary>
	public void PlaySE (string seName, int ch = -1, float vol = DEFINE.SE_VOLUME_DEFULT)
	{
		if (!_seDic.ContainsKey (seName)) {
			Debug.Log (seName + "という名前のSEがありません");
			return;
		}
		if (ch == -1) {
			GetAudioSource (DEFINE.PLAY_ONE_SHOT_SE_CH).PlayOneShot(_seDic [seName] as AudioClip);
			return;
		}
		var source = GetAudioSource (ch);
		source.clip = _seDic [seName] as AudioClip;
		source.volume = vol;
		if(!source.isPlaying)source.Play ();
	}

	public AudioSource GetAudioSource(int ch = -1){
		if(ch == -1){
			foreach(AudioSource seSource in _seSourceList){
				if(!seSource.isPlaying){
					return seSource;
				}
			}
			return _seSourceList [DEFINE.PLAY_ONE_SHOT_SE_CH];//全て再生中ならワンショット用を返す.
		}
		return _seSourceList [ch];

	}

	//=================================================================================
	//BGM
	//=================================================================================

	/// <summary>
	/// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
	/// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void PlayBGM (string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)
	{
		if (!_bgmDic.ContainsKey (bgmName)) {
			Debug.Log (bgmName + "という名前のBGMがありません");
			return;
		}

		//現在BGMが流れていない時はそのまま流す
		_bgmSource.pitch = 1;
		if (!_bgmSource.isPlaying) {
			_nextBGMName = "";
			_bgmSource.clip = _bgmDic [bgmName] as AudioClip;
			_bgmSource.Play ();
		}
		//違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
		else if (_bgmSource.clip.name != bgmName) {
			_nextBGMName = bgmName;
			FadeOutBGM (fadeSpeedRate);
		}
	}

	public float SetPitchBGM(float pitch){
		_bgmSource.pitch = pitch;
		return _bgmSource.pitch;
	}

	public AudioSource GetBGMAudioSource(){
		return _bgmSource;
	}

	/// <summary>
	/// BGMをすぐに止める
	/// </summary>
	public void StopBGM ()
	{
		_bgmSource.Stop ();
	}

	/// <summary>
	/// 現在流れている曲をフェードアウトさせる
	/// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void FadeOutBGM (float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
	{
		_bgmFadeSpeedRate = fadeSpeedRate;
		_isFadeOut = true;
	}

	private void Update ()
	{
		if (!_isFadeOut) {
			return;
		}

		//徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
		_bgmSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
		if (_bgmSource.volume <= 0) {
			_bgmSource.Stop ();
			_bgmSource.volume = DEFINE.BGM_VOLUME_DEFULT;
			_isFadeOut = false;

			if (!string.IsNullOrEmpty (_nextBGMName)) {
				PlayBGM (_nextBGMName);
			}
		}

	}

	//=================================================================================
	//音量変更
	//=================================================================================

	/// <summary>
	/// BGMとSEのボリュームを別々に変更&保存
	/// </summary>
	public void ChangeVolume (float BGMVolume, float SEVolume)
	{
		_bgmSource.volume = BGMVolume;
		foreach(AudioSource seSource in _seSourceList){
			seSource.volume  = SEVolume;
		}

		//PlayerPrefs.SetFloat (BGM_VOLUME_KEY,  BGMVolume);
		//PlayerPrefs.SetFloat (SE_VOLUME_KEY,   SEVolume);
	}

}