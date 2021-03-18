using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStageTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isOn = false; 
    public FadeInOutLevels fadeScript;
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Areafade");

        if (!isOn)
        {
            fadeScript.LoadStage();
            isOn = true;
            gameObject.SetActive(false);
        }
    }
}
