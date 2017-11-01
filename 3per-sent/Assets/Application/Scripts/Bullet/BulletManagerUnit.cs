using UnityEngine;
using System.Collections;

namespace PlayScene {


public class BulletManagerUnit : MonoBehaviour {

	[SerializeField]
	private BulletManager[] managers;//初期値を決める用のです.
	private BulletManager[] _managers;

	void Awake () {
		_managers = new BulletManager[managers.Length];
		for (int i = 0; i < _managers.Length; i++) {
			this.SetBulletManager (managers [i], i);
		}
	}
		
	public void Shot(int id = -1){
		if (_managers == null) this.Awake ();

		if(id == -1){
			foreach (var m in _managers) {
				m.Shot();
			}
			return;
		}

		if (_managers.Length > id) {
			_managers [id].Shot ();
		} else {
			Debug.LogWarning("BulletManagerUnitの配列を超えています : " + this.gameObject.name);
		}
	}

	public void Stop(int id = -1){
		if (_managers == null) this.Awake ();

		if(id == -1){
			foreach (var m in _managers) {
				if (m == null)continue;
				m.Stop();
			}
			return;
		}

		if (_managers.Length > id) {
			_managers [id].Stop();
		} else {
			Debug.LogWarning("BulletManagerUnitの配列を超えています : " + this.gameObject.name);
		}

	}

	public BulletManager SetBulletManager(BulletManager bm, int id){
		if (bm == null) return null;
		if (_managers [id] != null) {
			LossBulletManager (id);
		}
		_managers [id] = UnityUtil.GetPrefub<BulletManager> (bm, this.transform);
		return _managers[id];
	}

	public void LossBulletManager(int id){
		Destroy (_managers [id].gameObject);
	}
}

}
