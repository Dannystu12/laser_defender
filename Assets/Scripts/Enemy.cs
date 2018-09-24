using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy")]
    [SerializeField] float health = 100f;
    [SerializeField] GameObject explosion;
    [SerializeField] float durationOfExplosion = 0.5f;
    [SerializeField] int points = 10;

    [Header("Projectile")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxtimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed;

    [Header("Audio")]
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float laserVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float deathVolume = 1.0f;
     

    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxtimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
	}


    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);
            shotCounter = Random.Range(minTimeBetweenShots, maxtimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer dd = collision.gameObject.GetComponent<DamageDealer>();
        if(!dd){return;}
        ProcessHit(dd);
    }

    private void ProcessHit(DamageDealer dd)
    {
        health -= dd.GetDamage();
        dd.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    { 
        Destroy(gameObject);
        FindObjectOfType<GameSession>().UpdateScore(points);
        GameObject vfx = Instantiate(explosion, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        Destroy(vfx, durationOfExplosion);
    }
}
