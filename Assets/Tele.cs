using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject ThePlayer;

    void OnTriggerEnter(Collider other)
    {
        ThePlayer.transform.position = teleportTarget.transform.position;
    }
}
