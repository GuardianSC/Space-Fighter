using UnityEngine;
using System.Collections;

public class pProjectiles : MonoBehaviour
{
    EnemyController enemy;

    public int damage = 20;
    public float lifetime = 5f;
    	
	// Update is called once per frame
	void Update ()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy"/* || col.gameObject.tag == "Object"*/)
        {
            enemy.health -= damage;
            Destroy(gameObject);
        }
    }
}
