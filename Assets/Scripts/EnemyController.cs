using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class EnemyController : NetworkBehaviour
{
    public Rigidbody rb;
    public MeshCollider mc;

    public Transform shotSpawn;
    public GameObject shot;

    public int maxHealth;
    public int health;

    public bool isCapitalShip;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
        health = maxHealth;
        // Enemies wait a second, then fire once every second
        if (!isCapitalShip)
            InvokeRepeating("Shoot", 1, 1);
        // Capital ships fire once every 3 seconds
        if (isCapitalShip)
            InvokeRepeating("Shoot", 2.0f, 3f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
            Destroy(gameObject);
	}

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "PlayerProjectile")
        {
            rb.freezeRotation = true;
            health -= 20; // 20 is projectile damage
            rb.freezeRotation = false;
        }
    }

    void Shoot()
    {
        GameObject clone = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Vector3 force = shotSpawn.forward * 500;
        clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        NetworkServer.Spawn(clone);
    }
}
