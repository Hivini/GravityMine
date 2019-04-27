using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScr : MonoBehaviour
{
    bool IsSelected;
    public Material mSelected;
    public Material mNoSelected;
    public bool IsSelected1 { get => IsSelected; set => IsSelected = value; }
    Renderer ren;

    // Start is called before the first frame update
    void Start()
    {
        IsSelected = false;
        ren = GetComponent<Renderer>();
        ren.sharedMaterial = mNoSelected;
    }

    // Update is called once per frame
    public void Select(bool selected)
    {
        IsSelected = selected;
        ren.sharedMaterial = IsSelected ? mSelected : mNoSelected;
        //print("change");
    }


}
