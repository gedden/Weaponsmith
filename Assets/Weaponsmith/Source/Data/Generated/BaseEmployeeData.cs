// Auto-Generated File. Do Not Edit. 
// (c) Gedden Games
// 

using UnityEngine;

public class BaseEmployeeData : GeneratedData
{
	public string Name;
	public string Flavor;
	public string AssetId;
	public long Race;
	public long Class;
	public decimal Activated;


    /// <summary>
    /// Class Constructor
    /// </summary>
	public BaseEmployeeData()
	{
	}
	
    /// <summary>
    /// Class Constructor
    /// </summary>
	public BaseEmployeeData(long IdIn, string NameIn, string FlavorIn, string AssetIdIn, long RaceIn, long ClassIn, decimal ActivatedIn)
	{
		Id = IdIn;
		Name = NameIn;
		Flavor = FlavorIn;
		AssetId = AssetIdIn;
		Race = RaceIn;
		Class = ClassIn;
		Activated = ActivatedIn;

	}
}
