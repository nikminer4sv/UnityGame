using System.Linq;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {

    [Header("Hexagons")]
    public GameObject Grass;

    [Header("Environment")]
    public GameObject Rocks;
    public GameObject Forest;

    [Header("Attributes")]
    public int SizeX;
    public int SizeY;

    private Transform CurrentPos;
    private GameObject[] Hexagons;
    private GameObject[] Environment;

    void Start() {

        Environment = new GameObject[3];
        Environment[0] = null;
        Environment[1] = Rocks;
        Environment[2] = Forest;

        CurrentPos = transform;
        CurrentPos.position = new Vector3(0, 0, 0);

        Hexagons = new GameObject[SizeX*SizeY];

        for (int y = 0; y < SizeX; y++) {

            float xOffset = 0;

            if (y % 2 != 0) {
                xOffset = 0f;
            } else {
                xOffset = 0.5f;
            }

            for (int x = 0; x < SizeY; x++) {

                Hexagons[y*SizeY+x] = (GameObject)Instantiate(Grass, CurrentPos.position, CurrentPos.rotation);
                CurrentPos.position += new Vector3(1, 0, 0);

            }


            float z = CurrentPos.position.z + 0.86f;

            CurrentPos.position = new Vector3(xOffset, 0, z);

        }

        for (int x = 0;x<Hexagons.Length;x++) {
            GameObject obj = Environment[Random.Range(0, 3)];
            if (obj != null)
                Hexagons[x].GetComponent<Hex>().BuildOnHex = (GameObject)Instantiate(obj, Hexagons[x].transform.position, Hexagons[x].transform.rotation);
        }

    }
}
