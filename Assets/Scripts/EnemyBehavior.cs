using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
public GameObject projectile;
public float projectileSpeed = 10.0f;
public float shotsPerSecond = 0.5f;
public int scoreValue = 175;
public AudioClip enemyShootSfx;
public AudioClip deathSound;

public float health = 150;
private ScoreKeeper scoreKeeper;

void Start ()
	{ scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

void Update (){
float probability = Time.deltaTime * shotsPerSecond;
if (Random.value < probability){
			Fire();
}
	
	}

	void Fire ()
	{Vector3 startPosition = transform.position + new Vector3(0,-1,0);
GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity)as GameObject;
missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
AudioSource.PlayClipAtPoint(enemyShootSfx, transform.position);
	}

void OnTriggerEnter2D(Collider2D collider){
	Projectile missile = collider.gameObject.GetComponent<Projectile>();
	if (missile){
	health -= missile.GetDamage();
	missile.Hit();
	if (health <= 0) {
	AudioSource.PlayClipAtPoint(deathSound, transform.position);
	Destroy(gameObject);}
		Debug.Log("Hit by a projectile");

	scoreKeeper.Score(scoreValue);

	}
}

}
