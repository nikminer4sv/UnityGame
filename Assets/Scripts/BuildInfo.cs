using UnityEngine;

public class BuildInfo : MonoBehaviour {

    public enum myEnum {
        None,
        Gold,
        Stone,
        Wood,
        Human
    };

    [Header("Attributes")]
    public string Name;
    public myEnum ResourceType = myEnum.None;
    public int CountOfResource;
    
    [Header("Cost")]
    public int Stone;
    public int Wood;
    public int Gold;
    public int Human;

}
