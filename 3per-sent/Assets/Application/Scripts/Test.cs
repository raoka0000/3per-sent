using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

public class Test : MonoBehaviour {
	public GameObject enemy;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	// Use this for initialization
	void Start () {
		
	}

	float timer = 3;
	float timer2 = 3;
	float timer3 = 3;
	float timer4 = 30;
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 8.5f) {
			//オブジェクトの座標
			float x = Random.Range(10f, 10f);
			float y = Random.Range(-4.0f, 4.0f);
			ObjectPool.instance.GetGameObject (enemy, new Vector2(x,y), this.gameObject.transform.rotation);
			timer = 0;
		}

		timer2 += Time.deltaTime;
		if (timer2 > 0.8f) {
			//オブジェクトの座標
			float x = Random.Range(10f, 10f);
			float y = Random.Range(-4.0f, 4.0f);
			ObjectPool.instance.GetGameObject (enemy2, new Vector2(x,y), this.gameObject.transform.rotation);
			timer2 = 0;
		}

		timer3 += Time.deltaTime;
		if (timer3 > 0.5f) {
			if (Random.value > 0.5) {
				timer3 = 0;
				return;
			}
			//オブジェクトの座標
			float x = Random.Range(10f, 10f);
			float y = Random.Range(-4.0f, 4.0f);
			ObjectPool.instance.GetGameObject (enemy3, new Vector2(x,y), this.gameObject.transform.rotation);
		}
		timer4 += Time.deltaTime;
		if (timer4 > 10f) {
			//オブジェクトの座標
			ObjectPool.instance.GetGameObject (enemy4, new Vector2(15,0), this.gameObject.transform.rotation);
			timer4 = 0;
		}

	}
}
