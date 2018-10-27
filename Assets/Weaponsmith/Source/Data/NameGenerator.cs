using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class NameGenerator
{
    private static readonly List<string> _prefixes = new List<string> { "Ael", "Aer", "Af", "Ah", "Al", "Am", "Ama", "An", "Ang", "Ansr", "Ar", "Ari", "Arn", "Aza", "Bael", "Bes", "Cael", "Cal", "Cas", "Cla", "Cor", "Cy", "Dae", "Dho", "Dre", "Du", "Eil", "Eir", "El", "Er", "Ev", "Fera", "Fi", "Fir", "Fis", "Gael", "Gar", "Gil", "Ha", "Hu", "Ia", "Il", "Ja", "Jar", "Ka", "Kan", "Ker", "Keth", "Koeh", "Kor", "La", "Laf", "Lam", "Lue", "Ly", "Mai", "Mal", "Mara", "My", "Na", "Nai", "Nim", "Nu", "Ny", "Py", "Raer", "Re", "Ren", "Rhy", "Ru", "Rua", "Rum", "Rid", "Sae", "Seh", "Sel", "Sha", "She", "Si", "Sim", "Sol", "Sum", "Syl", "Ta", "Tahl", "Tha", "Tho", "Ther", "Thro", "Tia", "Tra", "Ty", "Uth", "Ver", "Vil", "Von", "Ya", "Za", "Zy" };
    private static readonly List<string> _suffixes = new List<string> { "ae", "ael", "aer", "aias", "ah", "aith", "al", "ali", "am", "an", "ar", "ari", "aro", "as", "ath", "avel", "brar", "dar", "deth", "dre", "drim", "dul", "ean", "el", "emar", "en", "er", "ess", "evar", "fel", "hal", "har", "hel", "ian", "iat", "ik", "il", "im", "in", "ir", "is", "ith", "kash", "ki", "lan", "lam", "lar", "las", "lian", "lis", "lyn", "mah", "mil", "mus", "nal", "nes", "nin", "nis", "on", "or", "oth", "que", "quis", "rah", "rad", "rail", "ran", "reth", "ro", "ruil", "sal", "san", "sar", "sel", "sha", "spar", "tae", "tas", "ten", "thal", "thar", "ther", "thi", "thus", "ti", "tril", "ual", "uath", "us", "van", "var", "vain", "via", "vin", "wyn", "ya", "yr", "yth", "zair" };
    private static readonly Random _random = new Random(System.DateTime.Now.Millisecond);


    /// <summary>
    /// Geta  random entry 
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private static string GetRandom(List<string> list)
    {
        return list[_random.Next(list.Count - 1)];
    }

    public static string GenerateFullElvenName()
    {
        return GenerateElvenName() + " " + GenerateElvenName();
    }

    public static string GenerateElvenName()
    {
        return GetRandom(_prefixes) + GetRandom(_suffixes);
    }
}