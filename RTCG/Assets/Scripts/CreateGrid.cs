using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public Transform prefab;
    public int gridResolution = 10;

    public Transform[] grid;

    void Awake()
    {
        grid = new Transform[gridResolution * gridResolution * gridResolution];

        for (int i = 0, z = 0; z < gridResolution; z++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
                for (int x = 0; x < gridResolution; x++, i++)
                {
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }
    }

    Transform CreateGridPoint(int x, int y, int z)
    {
        Transform point = Instantiate(prefab, transform); // parented
        point.localPosition = GetCoordinates(x, y, z);

        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / gridResolution,
            (float)y / gridResolution,
            (float)z / gridResolution
        );

        return point;
    }

    Vector3 GetCoordinates(int x, int y, int z)
    {
        float half = (gridResolution - 1) * 0.5f;
        return new Vector3(x - half, y - half, z - half);
    }
}
