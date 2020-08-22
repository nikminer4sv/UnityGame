using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Hex : MonoBehaviour {

    [Header("Selections")]
    public GameObject WhiteSelection;
    public GameObject RedSelection;
    public GameObject YellowSelection;

    [Header("Attributes")]
    public bool IsBlocked;
    private GameObject SelectionInstant;
    private BuildManager bm;
    private GamePlay gp;
    private EventsObject eo;
    private Mesh MeshBuffer;
    private Color NativeColor;

    //[HideInInspector]
    public GameObject BuildOnHex;

    void Start() {

        MeshBuffer = gameObject.GetComponent<MeshFilter>().mesh;
        bm = BuildManager.BuildManagerInstance;
        eo = EventsObject.EventsObjectInstance;
        gp = GamePlay.GamePlayInstance;
        NativeColor = gameObject.GetComponent<Renderer>().material.color;

    }

    void OnMouseEnter() {

        if (EventSystem.current.IsPointerOverGameObject()) 
            return;

        bm.CurrentHex = this;

        if (bm.YellowSelectionInstant != null)
            if (bm.YellowSelectionGameObject == gameObject)
                return;

        if (IsBlocked == false) {

            SelectionInstant = (GameObject)Instantiate(WhiteSelection, transform.position + new Vector3(0, 0.005f, 0), transform.rotation);

            if (BuildOnHex == null) {


                if (bm.GetReplaceGroundVal()) {

                    bm.CurrentHex.GetComponent<MeshFilter>().mesh = bm.GetMeshObject();
                    bm.MouseEnterBuild = bm.GetObjectToBuild();

                } else {

                    bm.MouseEnterBuild = (GameObject)Instantiate(bm.GetObjectToBuild(), transform.position, transform.rotation);

                }

            }


        } else {

            SelectionInstant = (GameObject)Instantiate(RedSelection, transform.position + new Vector3(0, 0.005f, 0), transform.rotation);

        }

    }

    void Update() {

        if (EventSystem.current.IsPointerOverGameObject()) {

            Destroy(SelectionInstant);
            if (BuildOnHex == null) {

                gameObject.GetComponent<MeshFilter>().mesh = MeshBuffer;

            }

        }

    }

    void OnMouseExit() {
        
        Destroy(SelectionInstant);

        if (IsBlocked == false) {

            if (BuildOnHex == null) {

                if (bm.GetReplaceGroundVal()) {

                    bm.CurrentHex.GetComponent<MeshFilter>().mesh = MeshBuffer;
                    bm.CurrentHex.GetComponent<Renderer>().material.color = NativeColor;
                    bm.MouseEnterBuild = null;

                } else {

                    Destroy(bm.MouseEnterBuild);
                    bm.MouseEnterBuild = null;

                }

            }

        }

    }

    void OnMouseDown() {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Destroy(bm.YellowSelectionInstant);
        bm.YellowSelectionInstant = null;

        if (IsBlocked == false) {

            if (BuildOnHex == null) {

                bm.YellowSelectionGameObject = null;

                if (gp.CanWeBuild(bm.GetObjectToBuild())) {

                    gp.Build(bm.GetObjectToBuild());

                } else {

                    return;

                }

                Destroy(bm.MouseEnterBuild);
                bm.MouseEnterBuild = null;

                if (!bm.GetReplaceGroundVal()) {

                    BuildOnHex = (GameObject)Instantiate(bm.GetObjectToBuild(), transform.position, transform.rotation);

                } else {

                    BuildOnHex = bm.GetObjectToBuild();
                    bm.CurrentHex.GetComponent<MeshFilter>().mesh = bm.GetMeshObject();
                    bm.CurrentHex.GetComponent<Renderer>().material.color = Color.white;

                }

            } else {

                Destroy(SelectionInstant);
                eo.ChangeInspectorInfo(BuildOnHex);
                bm.YellowSelectionInstant = (GameObject)Instantiate(YellowSelection, transform.position + new Vector3(0, 0.005f, 0), transform.rotation);
                bm.YellowSelectionGameObject = gameObject;
                if (!eo.IsInspectorBarOpenVal()) 
                    eo.InspectorBarOpen();

            }

        }

    }

}
