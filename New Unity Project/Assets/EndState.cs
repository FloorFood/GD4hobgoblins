using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndState : MonoBehaviour
{
    public GameObject fin;
    public GameObject finalArea;

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("OldGhostHappy");
        FindObjectOfType<AudioManager>().Play("LonelyGhostHappy");
        FindObjectOfType<AudioManager>().Play("MellowGhostHappy");
        fin.SetActive(true);
        finalArea.SetActive(false);
    }
}
