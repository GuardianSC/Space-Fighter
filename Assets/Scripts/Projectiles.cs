using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour
{
    EnemyController enemy;
    SpaceController player;
    
    public int damage = 20;
    public float lifetime = 5f;
    	
	// Update is called once per frame
	void Update ()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
	}

    void OnCollisionEnter()
    {
            Destroy(gameObject);
    }
}
