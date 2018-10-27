using System;
using UnityEngine;

public class FakeFactory
{
    private static long Seed = -1;

    /// <summary>
    /// Get the next fake seed
    /// </summary>
    /// <returns></returns>
    private static long GetNextFakeSeed()
    {
        return --Seed;
    }

    /// <summary>
    /// Create a fake workstation
    /// </summary>
    /// <param name="arcana"></param>
    /// <returns></returns>
    public static WorkstationData CreateWorkstation(EArcana arcana = EArcana.Conjuration)
    {
        WorkstationData station = new WorkstationData();
        station.Id = GetNextFakeSeed();
        station.Arcana = arcana;

        return station;
    }

    /// <summary>
    /// Generate a character
    /// </summary>
    /// <param name="CharacterClass"></param>
    /// <returns></returns>
    public static CharacterData CreateCharacter(EClass CharacterClass = EClass.Conjurer)
    {
        // CharacterData Character = new CharacterData(-1, "Fake McGee", "", "", )
        CharacterData Character = new CharacterData(-1, NameGenerator.GenerateFullElvenName(), "", ERace.Human, CharacterClass, 1);
        return Character;
    }

    /// <summary>
    /// Create a fake session
    /// </summary>
    /// <returns></returns>
    public static SessionData CreateSession()
    {
        SessionData session = new SessionData();
        session.Gold = UnityEngine.Random.Range(0, 1000);
        session.Magic = UnityEngine.Random.Range(0, 1000);
        session.Organic = UnityEngine.Random.Range(0, 1000);
        session.Scrap = UnityEngine.Random.Range(0, 1000);

        // Create some fake workstations
        long Pos = 0;
        foreach( EArcana Arcana in GameUtil.Values<EArcana>() )
        {
            WorkstationData Station = new WorkstationData(GetNextFakeSeed(), Arcana, UnityEngine.Random.Range(0, 1000), Pos, 1);
            Pos++;

            session.Workstations.Add(Station);
        }

        // Create a fake worker
        session.Characters.Add(CreateCharacter());

        return session;
    }
}
