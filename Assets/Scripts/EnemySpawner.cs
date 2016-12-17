using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemyFighterPrefab;
    public GameObject enemyCapitalPrefab;
    public int numberOfFighters;
    public int numberOfCapitals;

    public override void OnStartServer()
    {
        // Enemy Fighters
        for (int i = 0; i < numberOfFighters; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

            GameObject clone = (GameObject)Instantiate(enemyFighterPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(clone);
        }

        // Enemy Capitals
        for (int i = 0; i < numberOfCapitals; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(75, 100), 0, Random.Range(75, 100));
            Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

            GameObject clone = (GameObject)Instantiate(enemyCapitalPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(clone);
        }
    }
}
