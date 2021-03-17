using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutLevels : MonoBehaviour
{

    public List<GameObject> levels = new List<GameObject>();
    public int stage = 0;
    public float fadeSpeed;
    private bool next = false;
    private bool fading = false;
    private bool fadingIn = false;
    public List<Material> mats = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(next)
        {
            if(!fading)
            {
                mats.Clear();
                int numChild = levels[stage].transform.childCount;
                for(int i = 0; i < numChild; i++)
                {
                    GameObject child = levels[stage].transform.GetChild(i).gameObject;
                    for(int j = 0; j < child.GetComponent<Renderer>().materials.Length; j++)
                    {
                        mats.Add(child.GetComponent<Renderer>().materials[j]);
                    }
                }
                fading = true;
            }
            if(fading)
            {
                for (int i = 0; i < mats.Count; i++)
                {
                    Color ObjColor = mats[i].color;
                    float fadeAmount = ObjColor.a + (fadingIn == true ? fadeSpeed : -fadeSpeed);
                    Debug.Log(fadeAmount);

                    ObjColor = new Color(ObjColor.r, ObjColor.g, ObjColor.b, fadeAmount);
                    mats[i].color = ObjColor;
                    if(fadingIn && fadeAmount >= 1)
                    {
                        fadingIn = false;
                        next = false;
                    }
                    if(!fadingIn && fadeAmount <= 0)
                    {
                        fadingIn = true;
                        fading = false;
                        stage++;
                        levels[stage].gameObject.SetActive(false);
                    }
                }
            }

        }
    }

    public void LoadStage()
    {
        next = true;
        fadingIn = stage == 0 ? true : false;
        levels[stage].gameObject.SetActive(true);
    }
}
