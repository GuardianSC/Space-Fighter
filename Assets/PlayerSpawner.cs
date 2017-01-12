using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    SpaceController player;
    public GameObject playerPrefab;

	// Use this for initialization
	void Start ()
    {
        Vector3    spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);
        GameObject clone = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);
	}
}