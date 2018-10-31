using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollViewController : MonoBehaviour
{
    public ListOption OptionTemplate;
    public Transform Content;
    private ScrollRect _Scroll;
    public delegate void OnSelectDelegate(GeneratedData Data);
    public OnSelectDelegate Select;

    // Use this for initialization
    void Start ()
    {
        // Disable the template just in case!
        OptionTemplate.gameObject.SetActive(false);

    }

    private ScrollRect Scroll
    {
        get
        {
            if( _Scroll == null )
                _Scroll = GetComponent<ScrollRect>();
            return _Scroll;
        }
    }
	
    /// <summary>
    /// Add a new entry
    /// </summary>
    /// <param name="Label"></param>
    /// <param name="Entry"></param>
	public void Add(string Label, GeneratedData Entry)
    {
        // Initialize the template
        var option = Instantiate<ListOption>(OptionTemplate, Content);
        option.Set(Label, Entry);
        option.gameObject.SetActive(true);
    }

    public void OnSelectOption(ListOption Option)
    {
        if (Select != null)
        {
            Select(Option.Value);
        }
    }
}
