using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyFighterPrefab;
    public GameObject enemyCapitalPrefab;
    public int numberOfFighters;
    public int numberOfCapitals;

    void Start()
    {
        // Enemy Fighters
        for (int i = 0; i < numberOfFighters; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

            GameObject clone = (GameObject)Instantiate(enemyFighterPrefab, spawnPosition, spawnRotation);
        }

        // Enemy Capitals
        for (int i = 0; i < numberOfCapitals; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(325, 325), Random.Range(-35, 35), Random.Range(325, 325));
            Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

            GameObject clone = (GameObject)Instantiate(enemyCapitalPrefab, spawnPosition, spawnRotation);
        }
    }
}
