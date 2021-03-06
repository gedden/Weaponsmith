// Auto-Generated File. Do Not Edit. 
// (c) Gedden Games
// 

using UnityEngine;

public class CharacterData : GeneratedData
{
	public string Name;
	public string Flavor;
	public ERace Race;
	public EClass Class;
	public EGender Gender;
	public decimal Activated;


    /// <summary>
    /// Class Constructor
    /// </summary>
	public CharacterData()
	{
	}
	
    /// <summary>
    /// Class Constructor
    /// </summary>
	public CharacterData(long IdIn, string NameIn, string FlavorIn, ERace RaceIn, EClass ClassIn, EGender GenderIn, decimal ActivatedIn)
	{
		Id = IdIn;
		Name = NameIn;
		Flavor = FlavorIn;
		Race = RaceIn;
		Class = ClassIn;
		Gender = GenderIn;
		Activated = ActivatedIn;

	}
}
