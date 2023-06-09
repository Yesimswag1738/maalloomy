using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LS8 : MonoBehaviour
{
    void OnCollisionEnter(Collision coll)
    {
        SceneManager.LoadScene(8);
    }
}
