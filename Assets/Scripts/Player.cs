using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] int health = 100;
    [SerializeField] GameObject explosion;
    [SerializeField] float durationOfExplosion = 0.5f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserFiringPeriod = 0.1f;

    [Header("Audio")]
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float laserVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float deathVolume = 1.0f;


    Coroutine firingCoroutine; 

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float width;
    float height;

	// Use this for initialization
	void Start () {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        SetUpMoveBoundaries();
	}

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(Input.GetButtonUp("Fire1") && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var nextXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var nextYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(nextXPos, nextYPos); 
    }

    private void SetUpMoveBoundaries()
    {

        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        xMin += width / 2;
        xMax -= width / 2;
        yMin += height / 2;
        yMax -= height / 2;
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laserLeft = Instantiate(laserPrefab,
                transform.position + new Vector3(-width / 2, 0, 0),
                Quaternion.identity) as GameObject;
            GameObject laserRight = Instantiate(laserPrefab,
                transform.position + new Vector3(width / 2, 0, 0),
                Quaternion.identity) as GameObject;
            laserLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            laserRight.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);
            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var dd = collision.GetComponent<DamageDealer>();
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
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
        FindObjectOfType<Level>().LoadGameOver();
    }

    public int GetHealth()
    {
        return health;
    }
}
