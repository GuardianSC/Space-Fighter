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
    
    public float fireRate;
    public float nextFire;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CmdShoot();


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

    
    void CmdShoot()
    {
            nextFire = Time.time + fireRate;
            GameObject clone = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Vector3 force = shotSpawn.forward * 500;
            clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
            NetworkServer.Spawn(clone);
    }
}
