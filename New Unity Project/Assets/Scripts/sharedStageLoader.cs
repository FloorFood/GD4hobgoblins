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
            if(friendTrigger.GetComponent<sharedStageLoader>().isOn)
            {
                fadeScript.LoadStage();
                gameObject.SetActive(false);
            }
        }
    }
}
