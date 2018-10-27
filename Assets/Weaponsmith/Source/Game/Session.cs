using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session
{
    int Gold;
    int Magic;
    int Organic;
    int Scrap;

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
        // Spawn workstations
    }
}
