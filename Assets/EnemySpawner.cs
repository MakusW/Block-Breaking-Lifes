using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
public GameObject enemyPrefab;
public float width = 10f;
public float height = 5f;
private bool movingRight = true;
public float speed =5f;
private float xmax;
private float xmin;
public float spawnDelay = 0.5f;

	// Use this for initialization
	void Start (){
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xmax = rightEdge.x;
		xmin = leftEdge.x;
		SpawnUntilFull ();
	}
	void SpawnEnemies ()
	{
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	void SpawnUntilFull ()
	{
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
		Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
	}
	// Update is called once per frame
	void Update ()
	{
		if (movingRight) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0);
		} else {
			transform.position += new Vector3 (-speed * Time.deltaTime, 0);
		}
		float rightEdgeofScreen = transform.position.x + (0.5f * width);
		float leftEdgeofScreen = transform.position.x - (0.5f * width);
		if (leftEdgeofScreen < xmin) {
			movingRight = true;

		} else if (rightEdgeofScreen > xmax) {
			movingRight = false;
		}
		if (AllMembersDead ()) {
			Debug.Log ("Empty Formation");
			SpawnUntilFull ();
		}
	}

	Transform NextFreePosition (){
		foreach (Transform childPositionGameObject in transform) {
		if (childPositionGameObject.childCount == 0) {
		return childPositionGameObject;
		}
		}
		return null;
	}

	bool AllMembersDead(){
		foreach (Transform childPositionGameObject in transform) {
		if (childPositionGameObject.childCount >0) {
		return false;
		}
		}
		return true;
	}
	}