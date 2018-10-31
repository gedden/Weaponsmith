using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectDialog : MonoBehaviour
{
    public ScrollViewController Scroll;
	// Use this for initialization
	void Start ()
    {
        Scroll.Select = OnSelect;
    }

    public void OnSelect(GeneratedData Option)
    {
        WeaponTypeData WeaponType = Option as WeaponTypeData;
        Debug.Log(WeaponType.Id + ": " + WeaponType.Name);
    }

    public void OnAddOption()
    {
        foreach( var WeaponType in DatabaseUtil.QueryAll<WeaponTypeData>() )
        {
            Debug.Log(WeaponType.Id + ": " + WeaponType.Name);
            Scroll.Add(WeaponType.Name, WeaponType);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
