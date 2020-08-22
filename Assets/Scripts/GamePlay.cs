using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour {

    [Header("Attributes")]
    public int StartGold;
    public int StartStone;
    public int StartWood;
    public int StartAllHumans;
    public int StartFreeHumans;
    public int GoldPerSecond;
    public int StonePerSecond;
    public int WoodPerSecond;

    [Header("Resources Bar")]
    public Text StoneText;
    public Text WoodText;
    public Text GoldText;
    public Text HumanText;

    [Header("Costs Bar")]
    public Text CostStoneText;
    public Text CostWoodText;
    public Text CostGoldText;
    public Text CostHumanText;

    private BuildManager bm;
    private int Stone;
    private int Wood;
    private int Gold;
    private int MinesCount;
    private int SawmillsCount;
    private int AllHumans;
    private int FreeHumans;
    public static GamePlay GamePlayInstance;

    void Awake() {
        if (GamePlayInstance != null) {

            Debug.LogError("More than one GamePlay in the scene!");
            return;

        }

        GamePlayInstance = this;
    }

    void Start() {

        bm = BuildManager.BuildManagerInstance;
        AllHumans = StartAllHumans;
        FreeHumans = StartFreeHumans;
        Gold = StartGold;
        Stone = StartStone;
        Wood = StartWood;
        MinesCount = 0;
        SawmillsCount = 0;
        InvokeRepeating("ChangeResources", 0, 1f);
        UpdateCostBar(bm.GetObjectToBuild());
    }


    void ChangeResources() {
        Stone += StonePerSecond;
        Wood += WoodPerSecond;
        Gold += GoldPerSecond;
        UpdateResources();
    }

    void UpdateResources() {
        StoneText.text = Stone.ToString();
        WoodText.text = Wood.ToString();
        GoldText.text = Gold.ToString();
        UpdateHumans();

    }

    public void MinesCountPlus() {
        MinesCount++;
    }

    public void SawmillsCountPlus() {
        SawmillsCount++;
    }

    public bool CanWeBuild(GameObject build) {

        int StoneVal = build.GetComponent<BuildInfo>().Stone;
        int WoodVal = build.GetComponent<BuildInfo>().Wood;
        int GoldVal = build.GetComponent<BuildInfo>().Gold;
        int HumansVal = build.GetComponent<BuildInfo>().Human;

        if (Stone < StoneVal || Wood < WoodVal || Gold < GoldVal || FreeHumans < HumansVal) {
            return false;
        } else {
            return true;
        }

    }

    public void Build(GameObject build) {
        Stone -= build.GetComponent<BuildInfo>().Stone;
        Wood -= build.GetComponent<BuildInfo>().Wood;
        Gold -= build.GetComponent<BuildInfo>().Gold;
        FreeHumans -= build.GetComponent<BuildInfo>().Human;

        string ResourceType = build.GetComponent<BuildInfo>().ResourceType.ToString();
        int CountOfResource = build.GetComponent<BuildInfo>().CountOfResource;

        if (ResourceType == "Gold") {
            GoldPerSecond += CountOfResource;
        } else if (ResourceType == "Stone") {
            StonePerSecond += CountOfResource;
        } else if (ResourceType == "Wood") {
            WoodPerSecond += CountOfResource;
        } else if (ResourceType == "Human") {
            AllHumans += CountOfResource;
            FreeHumans += CountOfResource;
        }

        UpdateResources();

    }

    public void UpdateCostBar(GameObject build) {
        CostStoneText.text = build.GetComponent<BuildInfo>().Stone.ToString();
        CostWoodText.text = build.GetComponent<BuildInfo>().Wood.ToString();
        CostGoldText.text = build.GetComponent<BuildInfo>().Gold.ToString();
        CostHumanText.text = build.GetComponent<BuildInfo>().Human.ToString();
    }

    void UpdateHumans() {
        HumanText.text = FreeHumans + "/" + AllHumans;
    }

}
