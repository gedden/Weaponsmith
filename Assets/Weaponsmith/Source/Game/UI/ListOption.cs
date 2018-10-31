using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ListOption : ListOption<GeneratedData>
{
    public ScrollViewController Controller;

    public void Set(string Label, GeneratedData Data)
    {
        ListOptionData OptionData = new ListOptionData();
        OptionData.Label = Label;
        OptionData.Value = Data;

        Option = OptionData;
    }

    public void OnSelect()
    {
        Controller.OnSelectOption(this);
    }
}


public class ListOption<T> : MonoBehaviour
{
    public Text Display;
    private ListOptionData _Option;

    public struct ListOptionData
    {
        public string Label;
        public T Value;
    }

    public ListOptionData Option
    {
        set
        {
            _Option = value;
            if (Display)
                Display.text = Label;
        }
        get
        {
            return _Option;
        }
    }

    public T Value
    {
        get
        {
            return _Option.Value;
        }
    }

    public string Label 
    {
        get
        {
            return _Option.Label;
        }
    }
}