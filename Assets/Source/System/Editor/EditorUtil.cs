using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

class EditorUtil
{
    [MenuItem("GameObject/UI/Weaponsmith/Button")]
    private static void AddWeaponsmithButton()
    {
        AddUI("Assets/Weaponsmith/Prefabs/UI/Button.prefab");
    }

    [MenuItem("GameObject/UI/Weaponsmith/Dropdown")]
    private static void AddWeaponsmithDropdown()
    {
        AddUI("Assets/Weaponsmith/Prefabs/UI/Dropdown.prefab");
    }

    private static void AddUI(string AssetPath)
    {
        var Asset = AssetDatabase.LoadAssetAtPath<GameObject>(AssetPath);

        var Instance = (GameObject)PrefabUtility.InstantiatePrefab(Asset);
        PrefabUtility.DisconnectPrefabInstance(Instance);

        if (Selection.activeGameObject != null)
        {
            // Set its parent
            Instance.transform.SetParent(Selection.activeGameObject.transform);
            Instance.transform.localPosition = Vector3.zero;
        }
    }
}
