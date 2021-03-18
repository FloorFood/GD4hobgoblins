using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FadeInOutLevels : MonoBehaviour
{

    public List<GameObject> levels = new List<GameObject>();
    public int stage = 0;
    public float fadeSpeed;
    private bool next = false;
    private bool fading = false;
    private bool fadingIn = false;
    List<Material> mats = new List<Material>();

    public List<Material> allMaterials = new List<Material>();

    public bool start = false;

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
                int numChild = levels[stage + (fadingIn && start ? 1 : 0)].transform.childCount;
                for(int i = 0; i < numChild; i++)
                {
                    GameObject child = levels[stage + (fadingIn && start ? 1 : 0)].transform.GetChild(i).gameObject;
                    for(int j = 0; j < child.GetComponent<Renderer>().sharedMaterials.Length; j++)
                    {
                        mats.Add(child.GetComponent<Renderer>().sharedMaterials[j]);
                    }
                }
                fading = true;
                mats = mats.Distinct<Material>().ToList<Material>();
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
                    if(fadingIn && mats[mats.Count -1].color.a >= 1)
                    {
                        fadingIn = false;
                        next = false;
                        if(start) 
                        {
                            stage++;
                        }
                        if(stage == 0 && !start) // if 0 and not start: start. if 0 and start: stage ++ if tart and stage != 0 ++
                        {
                            start = true;
                        }
                        return;
                    }
                    if(!fadingIn && mats[mats.Count -1].color.a <= 0)
                    {
                        fadingIn = true;
                        fading = false;
                        if(start)
                        {
                            levels[stage].gameObject.SetActive(false);
                            if(stage < 2)
                            levels[stage + 1].SetActive(true);
                        //stage++;
                        }
                    }
                }
            }
        }
    }

    public void LoadStage()
    {
        next = true;
        fadingIn = stage == 0 && !start ? true : false;

        //levels[stage].gameObject.SetActive(true);
        //if(stage < 2)
        //levels[stage+1].gameObject.SetActive(true);
    }

    public void OnApplicationQuit() 
    {
        foreach (Material m in allMaterials)
        {
            Color c = m.color;
            c.a = 0;
           m.color = c;
        }
    }

    private void Awake() 
    {
         foreach (Material m in allMaterials)
        {
            Color c = m.color;
            c.a = 0;
           m.color = c;
        }
    }
}
