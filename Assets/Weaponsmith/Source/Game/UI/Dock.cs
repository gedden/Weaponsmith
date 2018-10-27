using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dock : MonoBehaviour
{
    public Text DayLabel;
    public Text TimeLabel;

    public CurrencyDisplay GoldDisplay;
    public CurrencyDisplay ScrapDisplay;
    public CurrencyDisplay OrganicDisplay;
    public CurrencyDisplay MagicDisplay;

    // Use this for initialization
    void Start ()
    {
        ShowDisplays(false);
        Clear();

    }

    public void Clear()
    {
        ShowDisplays(false);
        TimePercent = 0;
        Day = 0;
    }

    /// <summary>
    /// Set the day visual
    /// </summary>
    public int Day
    {
        set
        {
            DayLabel.text = "" + value;
        }
    }


    /// <summary>
    /// Set the time (something between 6am and 6pm)
    /// </summary>
    public int Time
    {
        set
        {
            TimeLabel.text = "" + (value%12) + (value>12?" pm":" am");
        }
    }

    public float TimePercent
    {
        set
        {
            float Normalized = 12 * value;
            Time = ((int)Mathf.Floor(Normalized)) + 6;
        }
    }

    public SessionData SessionData
    {
        set
        {
            GoldDisplay.Value = value.Gold;
            ScrapDisplay.Value = value.Scrap;
            OrganicDisplay.Value = value.Organic;
            MagicDisplay.Value = value.Magic;
        }
    }


    /// <summary>
    /// Set the display state for all the currencies
    /// </summary>
    /// <param name="Show"></param>
    public void ShowDisplays(bool Show=true)
    {
        GoldDisplay.gameObject.SetActive(Show);
        ScrapDisplay.gameObject.SetActive(Show);
        OrganicDisplay.gameObject.SetActive(Show);
        MagicDisplay.gameObject.SetActive(Show);
    }
}
