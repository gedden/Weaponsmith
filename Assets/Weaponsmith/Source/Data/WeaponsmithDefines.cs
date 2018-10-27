using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum ECurrenyType
{
    Gold,
    Scrap,
    Organic,
    Magic
}

public enum EArcana
{
    Conjuration,    // Yellow   -- make stuff
    Enchantment,    // Blue     -- enchant stuff
    Divination,     // White    -- info/discover stuff

}


public static class WeaponsmithDefines
{
    private static Dictionary<ECurrenyType, Sprite> IconCache = new Dictionary<ECurrenyType, Sprite>();

    /// <summary>
    /// Get the icon for the resource type in question
    /// </summary>
    /// <param name="ResourceType"></param>
    /// <returns></returns>
    public static Sprite GetIcon(this ECurrenyType ResourceType)
    {
        if( IconCache.ContainsKey(ResourceType) )
        {
            return IconCache[ResourceType];
        }

        Sprite Icon = Resources.Load<Sprite>(GetIconPath(ResourceType));
        IconCache.Add(ResourceType, Icon);
        return Icon;
    }

    /// <summary>
    /// Get the display name
    /// </summary>
    /// <param name="Currency"></param>
    /// <returns></returns>
    public static string GetDisplayName(this ECurrenyType Currency)
    {
        switch (Currency)
        {
            default:
            case ECurrenyType.Gold:
                return "Gold";
            case ECurrenyType.Magic:
                return "Magic";
            case ECurrenyType.Organic:
                return "Organic";
            case ECurrenyType.Scrap:
                return "Scrap";
        }
    }

    public static string GetIconPath(this ECurrenyType ResourceType)
    {
        switch (ResourceType)
        {
            case ECurrenyType.Gold:
                return "gold";
            case ECurrenyType.Magic:
                return "magic";
            case ECurrenyType.Organic:
                return "organic";
            case ECurrenyType.Scrap:
                return "scrap2";

        }
        return null;
    }

    
}
