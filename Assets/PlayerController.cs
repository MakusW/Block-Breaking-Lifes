using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed =15.0f;
	public float padding = 1f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 1.0f;
	public float health = 250.0f;

	float xmin;
	float xmax;



	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
	Vector3 leftmost =	Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost =	Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	void Fire (){
		Vector3 offset = new Vector3(0,1,0);
		GameObject beam = Instantiate(projectile, transform.position+offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
			//print ("down");


		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("Fire");
		//	print ("up");

		}
		if (Input.GetKey ("left")) {
		//	transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed *Time.deltaTime;

		} else if (Input.GetKey ("right")) {
			//transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed *Time.deltaTime;
		}

		//restrict the palyer to game space
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);

		transform.position = new Vector3(newX, transform.position.y, transform.position.z);

	}
	void OnTriggerEnter2D(Collider2D collider){
	Debug.Log("Player collided w missile");
	Projectile missile = collider.gameObject.GetComponent<Projectile>();
	if (missile){
	health -= missile.GetDamage();
	missile.Hit();
	if (health <= 0) {
	Destroy(gameObject);}
		Debug.Log("Hit by a projectile");

	}
}

}

