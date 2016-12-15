using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class EnemyController : NetworkBehaviour
{
    Projectiles projectiles;

    public Rigidbody rb;
    public MeshCollider mc;

    public Transform shotSpawn;
    public GameObject shot;

    public int maxHealth;
    public int health;
    
    public float fireRate = .5f;
    public float nextFire = .5f;
    public float fireTime = 0;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
        health = maxHealth;
        // Enemies wait 2 seconds, then fire once every 1.5 seconds
        InvokeRepeating("Shoot", 2.0f, 1.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Shoot();

        if (health <= 0)
            Destroy(gameObject);
	}

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "PlayerProjectile")
        {
            health -= 20; // 20 is projectile damage
        }
    }

    void Shoot()
    {
        //fireTime = fireTime + Time.deltaTime;
        
        GameObject clone = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        //nextFire = fireTime + fireRate;
        Vector3 force = shotSpawn.forward * 500;
        clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        NetworkServer.Spawn(clone);
        //nextFire = nextFire - fireTime;
        //fireTime = 0;
    }
}
