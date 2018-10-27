using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameUtil
{
    public static string[] Affirmitive = { "Yes", "Yeah", "Ok", "Okey dokey", "By all means", "Affirmative", "Roger", "Yass Queen", "Very Well", "Make it so", "Ya sure", "Right on", "Ja", "Sure", "fo' shizzle", "Totally", "You bet", "Allright", "Sound Good", "Got it", "Understood", "Indeed", "Yeah, yeah", "Fine", "Aye", "Yea", "Mhmm" };

    /// <summary>
    /// For fun, get a random "Affirmitive"
    /// </summary>
    /// <returns></returns>
    public static string GetRandomAffirmitive()
    {
        System.Random Rand = new System.Random(System.DateTime.Now.Millisecond);
        return Affirmitive[Rand.Next(Affirmitive.Length)];
    }

    public static IEnumerable<T> Values<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static void DestroyChildren(Transform Transform)
    {
        foreach( Transform child in Transform )
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

