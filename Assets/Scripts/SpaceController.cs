using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class SpaceController : MonoBehaviour
{
    public Rigidbody rb;
    public MeshCollider mc;

    //public GameObject playerPrefab;

    public Transform shotSpawn;
    public GameObject shot;

    public float turnSpeed   = 5;
    public float speed       = 5;
    public float trueSpeed   = 0;
    public float strafeSpeed = 5;

    public const int maxHealth = 100;
    public int health;
    public float respawnTimer = 5;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
        health = maxHealth;
    }

    // Fixed update does the physics stuff (like movement)
    void FixedUpdate()
    {
        float r = Input.GetAxis("Roll");
        float p = Input.GetAxis("Pitch");
        float y = Input.GetAxis("Yaw");
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        
        Vector3 movement = new Vector3(h * strafeSpeed * Time.deltaTime, 0, v * strafeSpeed * Time.deltaTime);

        if (trueSpeed >= -1 && trueSpeed <= 0)
            speed -= Time.deltaTime;
        if (trueSpeed >= 0 && trueSpeed <= 1)
            speed += Time.deltaTime;
        if (trueSpeed == 0)
            speed = trueSpeed;
        if (Input.GetButton("Stop"))
            speed = 0;

        rb.AddRelativeTorque(p * turnSpeed * Time.deltaTime, y * turnSpeed * Time.deltaTime, r * turnSpeed * Time.deltaTime);
        rb.AddRelativeForce(0, 0, trueSpeed * speed * Time.deltaTime);
        rb.AddRelativeForce(movement);
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting
        if (Input.GetButtonDown("Fire1"))
            Invoke("CmdShoot", 0.1f);

        // Respawning
        if (health <= 0)
        {
            Respawn();
        }
    }
    
    void CmdShoot()
    {
        GameObject clone = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Vector3 force = shotSpawn.forward * 500;
        clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
    }

    void Respawn()
    {
            transform.position = Vector3.zero;
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "EnemyProjectile")
        {
            rb.freezeRotation = true;
            health -= 20; // 20 is projectile damage
            rb.freezeRotation = false;
        }
    }
}
