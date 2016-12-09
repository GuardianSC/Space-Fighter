using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public MeshCollider mc;

    public float health;
    public float maxHealth;

    public Transform shotSpawn;
    public GameObject shot;
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
        if (health <= 0)
            Destroy(gameObject);
	}
}
