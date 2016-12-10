using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class SpaceController : NetworkBehaviour
{
    Projectiles projectiles;

    public Rigidbody rb;
    public MeshCollider mc;

    public Transform shotSpawn;
    public GameObject shot;

    public float turnSpeed   = 5;
    public float speed       = 5;
    public float trueSpeed   = 0;
    public float strafeSpeed = 5;

    public const int maxHealth = 100;
    int health = maxHealth;
    public float fireRate;
    public float nextFire;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /// Movement
        float r = Input.GetAxis("Roll");
        float p = Input.GetAxis("Pitch");
        float y = Input.GetAxis("Yaw");
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        float s = Input.GetAxis("Stop");
        
        Vector3 movement = new Vector3(h * strafeSpeed * Time.deltaTime, 0, v * strafeSpeed * Time.deltaTime);

        if (trueSpeed >= -1 && trueSpeed <= 0)
            speed -= Time.deltaTime;
        if (trueSpeed >= 0 && trueSpeed <= 1)
            speed += Time.deltaTime;
        if (trueSpeed == 0)
            speed = trueSpeed;
        if (Input.GetKey(KeyCode.Space))
            speed = 0;

        rb.AddRelativeTorque(p * turnSpeed * Time.deltaTime, y * turnSpeed * Time.deltaTime, r * turnSpeed * Time.deltaTime);
        rb.AddRelativeForce(0, 0, trueSpeed * speed * Time.deltaTime);
        rb.AddRelativeForce(movement);

        //Vector3 targetVelocity = (transform.forward * speed) * t;
        //transform.Translate(targetVelocity);
        //transform.position = Vector3.Lerp(transform.position, targetVelocity, currentSpeed * Time.deltaTime);
    }
    
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        /// Shooting
        CmdShoot();
    }

    [Command]
    void CmdShoot()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject clone = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Vector3 force = shotSpawn.forward * 500;
            clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
            NetworkServer.Spawn(clone);
        }
    }

     public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "EnemyProjectile")
        {
            health -= 20; // 20 is projectile damage
        }
    }
}
