using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WeaponCraftingProject : CraftingProject
{
    /// <summary>
    /// Class Constructor
    /// </summary>
    public WeaponCraftingProject() : base()
    {

    }

    public override void GenerateAffix()
    {
        throw new NotImplementedException();
    }

    public override void GenerateAffixCount()
    {
        throw new NotImplementedException();
    }

    public override void GenerateStats()
    {
        throw new NotImplementedException();
    }

    public override void SelectAffix()
    {
        throw new NotImplementedException();
    }

    public override void SelectItem()
    {
        // Get all the weapon types
        var weaponTypes = DatabaseUtil.QueryAll<WeaponTypeData>();

        // Show the weapon select popup
        Debug.Log("Show weapon select popup");
        // ModalManager.CreatePopup(MainMenuPrefab, true);
    }
}
