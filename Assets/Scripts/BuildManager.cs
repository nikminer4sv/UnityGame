using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour
{

    private GameObject ObjectToBuild;
    private bool ReplaceGround;
    private Mesh MeshObject;
    private GamePlay gp;
    public static BuildManager BuildManagerInstance;

    [Header("Colors")]
    public Color GreenColor;
    public Color RedColor;
    public Color OwnModelsRedColor;
    public Color OwnModelsGreenColor;

    [Header("Buildings")]
    public GameObject DefaultBuild;

    [HideInInspector]
    public GameObject YellowSelectionInstant;
    [HideInInspector]
    public GameObject YellowSelectionGameObject;
    [HideInInspector]
    public Hex CurrentHex;
    [HideInInspector]
    public GameObject MouseEnterBuild;

    void Awake() {
        if (BuildManagerInstance != null) {

            Debug.LogError("More than one BuildManager in the scene!");
            return;

        }

        BuildManagerInstance = this;

    }

    void Start() {
        SetObjectToBuild(BuildManagerInstance.DefaultBuild);
        gp = GamePlay.GamePlayInstance;
        MouseEnterBuild = null;
    }

    public void SetObjectToBuild(GameObject building) {
        ObjectToBuild = building;
    }

    public void SetReplaceGroundVal(bool replaceground) {
        ReplaceGround = replaceground;
    }

    public void SetMesh(Mesh mesh) {
        MeshObject = mesh;
    }

    public GameObject GetObjectToBuild() {
        return ObjectToBuild;
    }

    public bool GetReplaceGroundVal() {
        return ReplaceGround;
    }

    public Mesh GetMeshObject() {
        return MeshObject;
    }

    void Update() {

        if (MouseEnterBuild == null || CurrentHex.BuildOnHex != null) {
            return;
        }

        if (!CurrentHex.IsBlocked) {

            if (BuildManagerInstance.GetReplaceGroundVal()) {

                if (gp.CanWeBuild(MouseEnterBuild)) {

                    CurrentHex.GetComponent<Renderer>().material.color = GreenColor;

                } else {

                    CurrentHex.GetComponent<Renderer>().material.color = RedColor;

                }

            } else {

                if (gp.CanWeBuild(MouseEnterBuild)) {


                    if (MouseEnterBuild.TryGetComponent(out Renderer rend)) {
                        rend.material.color = GreenColor;
                    } else {
                        foreach (Transform child in MouseEnterBuild.transform) {
                            IEnumerator enumerator = child.GetComponentInParent<Renderer>().materials.GetEnumerator();

                            while (enumerator.MoveNext()) {
                                Material mat = (Material)enumerator.Current;
                                if (mat.name == "HexagonsShared (Instance)") {
                                    mat.color = GreenColor;
                                } else {
                                    mat.color = OwnModelsGreenColor;
                                }
                            }

                        }
                    }


                    //MouseEnterBuild.GetComponent<Renderer>().material.color = GreenColor;

                } else {

                    if (MouseEnterBuild.TryGetComponent(out Renderer rend)) {
                        rend.material.color = RedColor;
                    } else {
                        foreach (Transform child in MouseEnterBuild.transform) {
                            IEnumerator enumerator = child.GetComponentInParent<Renderer>().materials.GetEnumerator();

                            while (enumerator.MoveNext()) {
                                Material mat = (Material)enumerator.Current;
                                if (mat.name == "HexagonsShared (Instance)") {
                                    mat.color = RedColor;
                                } else {
                                    mat.color = OwnModelsRedColor;
                                }
                            }

                        }
                    }

                    //MouseEnterBuild.GetComponent<Renderer>().material.color = RedColor;

                }

            }

        }

    }

}
