using UnityEngine;
using DG.Tweening;
using PlayScene;

namespace PlayScene.Enemys{

public class E_timeRabbit : Enemy {

	Vector3[] vecs = new Vector3[3];
	Vector3[] myVecs = new Vector3[]{
		new Vector3(0,0,0),
		new Vector3(0,0,0),
		new Vector3(-25,0,0)
	};
	protected override void Init(){
		myVecs [1] = EnvironmentEvent.instance.player.transform.position;
		for(int i = 0; i<vecs.Length; i++){
			if (i == 1) {
				vecs[i] = myVecs[i];
			} else {
				vecs[i] = myVecs[i] + this.transform.position;
			}
		}
		this.transform.DOLocalPath (vecs,4,PathType.CatmullRom).timeScale = timeScale;
	}
	public override void killed(){
		ParticleManager.instance.HitEffect (transform,15);
		AudioManager.instance.PlaySE (DEFINE.SHOT_SE,0);
		//AudioManager.instance.PlaySE ("slap",3);
	}
	protected override void _Sleep (){
		this.transform.DOKill ();
	}
	protected override void _Update () {}


}

}