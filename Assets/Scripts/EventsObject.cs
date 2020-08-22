using UnityEngine;
using UnityEngine.UI;

public class EventsObject : MonoBehaviour {

    [Header("Animators")]
    public Animator BottomBarAnimator;
    public Animator InspectorBarAnimator;

    [Header("Inspector Bar")]
    public Text InspectorBarText;

    private bool IsBottomBarOpen;
    private bool IsInspectorBarOpen;
    private BuildManager bm;
    public static EventsObject EventsObjectInstance;

    void Awake() {
        if (EventsObjectInstance != null) {

            Debug.LogError("More than one EventsObject in the scene!");
            return;

        }

        EventsObjectInstance = this;
        bm = BuildManager.BuildManagerInstance;

    }

    void Start() {
        IsBottomBarOpen = true;
        IsInspectorBarOpen = false;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
            if (IsInspectorBarOpen) {
                InspectorBarClose();
                Destroy(bm.YellowSelectionInstant);
                bm.YellowSelectionInstant = null;
                bm.YellowSelectionGameObject = null;
            }

    }

    public void InspectorBarOpen() {

        if (AnimatorIsPlaying(InspectorBarAnimator)) {
            return;
        }

        InspectorBarAnimator.Play("InspectorBarOpen");
        IsInspectorBarOpen = !IsInspectorBarOpen;

    }

    public void ChangeInspectorInfo(GameObject buildonhex) {
        InspectorBarText.text = buildonhex.GetComponent<BuildInfo>().Name;
    }

    public void InspectorBarClose() {

        if (AnimatorIsPlaying(InspectorBarAnimator)) {
            return;
        }

        InspectorBarAnimator.Play("InspectorBarClose");
        IsInspectorBarOpen = !IsInspectorBarOpen;

    }

    public void BottomBarButton() {

        if (AnimatorIsPlaying(BottomBarAnimator)) {
            return;
        }

        if (IsBottomBarOpen == true) {
            BottomBarAnimator.Play("BottomBarClose");
        } else {
            BottomBarAnimator.Play("BottomBarOpen");
        }

        IsBottomBarOpen = !IsBottomBarOpen;
    }
    
    bool AnimatorIsPlaying(Animator animator) {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public bool IsInspectorBarOpenVal() {
        return IsInspectorBarOpen;
    }

}
