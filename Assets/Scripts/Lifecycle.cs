using UnityEngine;
using System.Collections;

public class Lifecycle : MonoBehaviour
{
    void OnDestroy()
    {
        Debug.Log("Destroyed " + name);
    }
}
