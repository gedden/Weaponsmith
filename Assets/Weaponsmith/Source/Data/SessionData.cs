using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SessionData
{
    // If this session is saveable or not
    public bool Transient = false;

    public int Gold;
    public int Magic;
    public int Organic;
    public int Scrap;

    public List<CharacterData> Characters;
    public List<WorkstationData> Workstations;

    public SessionData()
    {
        Workstations = new List<WorkstationData>();
        Characters = new List<CharacterData>();
    }
}
