using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session
{
    int Gold;
    int Magic;
    int Organic;
    int Scrap;

    Workshop Workshop;

    // Property assignments
    private SessionData _Data;
    public SessionData Data
    {
        get
        {
            return _Data;
        }
        set
        {
            _Data = value;
            Initialize();
        }
    }

    /// <summary>
    /// Transient data accessor
    /// </summary>
    public bool Transient
    {
        get
        {
            return _Data.Transient;
        }
    }

    /// <summary>
    /// Initialize this session from the data
    /// </summary>
    public void Initialize()
    {
        Gold = Data.Gold;
        Magic = Data.Magic;
        Organic = Data.Organic;
        Scrap = Data.Scrap;
    }


    /// <summary>
    /// Clear out the session
    /// </summary>
    public void Clear()
    {
        Gold = 0;
        Magic = 0;
        Organic = 0;
        Scrap = 0;
    }

    public void CreateFakeGame()
    {
        // Create new data
        Data = FakeFactory.CreateSession();
    }
}
