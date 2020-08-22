using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
    public GameObject gg;
    public Color HexagonsPackColor;
    public Color OwnModelsColor;

    void Start() {
        if (gg.TryGetComponent(out Renderer rend)) {
            rend.material.color = Color.green;
        } else {
            foreach (Transform child in gg.transform) {
                IEnumerator enumerator = child.GetComponentInParent<Renderer>().materials.GetEnumerator();

                while (enumerator.MoveNext()) {
                    Material mat = (Material)enumerator.Current;
                    if (mat.name == "HexagonsShared (Instance)") {
                        mat.color = HexagonsPackColor;
                    } else {
                        mat.color = OwnModelsColor;
                    }
                }

            }
        }

    }

    /*void Update() {

        foreach (Transform child in gg.transform) {
            IEnumerator enumerator = child.GetComponentInParent<Renderer>().materials.GetEnumerator();

            while (enumerator.MoveNext()) {
                Material mat = (Material)enumerator.Current;
                if (mat.name == "HexagonsShared (Instance)") {
                    mat.color = HexagonsPackColor;
                } else {
                    mat.color = OwnModelsColor;
                }
            }

        }

    } */

}
