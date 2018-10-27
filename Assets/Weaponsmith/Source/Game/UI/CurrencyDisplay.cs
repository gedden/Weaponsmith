using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    public Image Icon;
    public Text LabelField;
    public Text ValueField;

    [SerializeField]
    private ECurrenyType _currencyType;

    [ExecuteInEditMode]
    public ECurrenyType CurrencyType
    {
        get
        {
            return _currencyType;
        }

        set
        {
            _currencyType = value;
            if( Icon != null )
            {
                Icon.sprite = _currencyType.GetIcon();
            }

            // Set the display name
            if( LabelField )
            {
                LabelField.text = _currencyType.GetDisplayName();
            }
        }
    }

    public int Value
    {
        set
        {
            // Cant set what we are not showing
            if (ValueField == null)
                return;

            // Set the value field
            ValueField.text = string.Format("{0:n0}", value);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        CurrencyType = CurrencyType;
    }
#endif

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
