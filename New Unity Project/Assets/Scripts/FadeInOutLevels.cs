using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutLevels : MonoBehaviour
{

    public List<GameObject> levels;
    public int stage;
    public float fadeSpeed;
    public bool next;
    // Start is called before the first frame update
    void Start()
    {
        levels = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void LoadNextStage()
    {
        int numChild = levels[stage].transform.childCount;
        List<Material> mats = new List<Material>();
        for(int i = 0; i < numChild; i++)
        {
            GameObject child = levels[stage].transform.GetChild(i).gameObject;
            foreach(Material mat in child.GetComponent<Renderer>().materials)
            mats.Add(mat);
        }
        for (int i = 0; i < mats.Count; i++)
        {
            Color ObjColor = mats[i].color;
            float fadeAmount = ObjColor.a - fadeSpeed;

            ObjColor = new Color(ObjColor.r, ObjColor.g, ObjColor.b, fadeAmount);
            mats[i].color = ObjColor;
        }
    }
}
