using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharedStageLoader : MonoBehaviour
{
    private bool isOn = false; 
    public GameObject friendTrigger;
    public FadeInOutLevels fadeScript;
    private void OnTriggerEnter(Collider other)
    {
        if(!isOn)
        {
            isOn = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Checkpoint");

            if (friendTrigger.GetComponent<sharedStageLoader>().isOn)
            {
                fadeScript.LoadStage();
                gameObject.SetActive(false);
            }
        }
    }
}
