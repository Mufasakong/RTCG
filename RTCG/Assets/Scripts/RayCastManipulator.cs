using UnityEngine;
using System.Collections.Generic;

public class RayCastManipulator : MonoBehaviour
{
    public int numRaycasts = 10;
    public LayerMask cubeLayerMask;

    CreateGrid createGrid;
    List<GameObject> hitCubes = new List<GameObject>();

    void Start()
    {
        createGrid = GetComponent<CreateGrid>();
    }

    void Update()
    {
        if (hitCubes.Count == createGrid.grid.Length)
        {
            foreach (GameObject cube in hitCubes)
            {
                cube.SetActive(true);
            }
            hitCubes.Clear();
        }

        Vector3 cubeCenter = createGrid.grid[0].position +
            new Vector3(
                createGrid.gridResolution / 2f,
                createGrid.gridResolution / 2f,
                createGrid.gridResolution / 2f
            );

        for (int i = 0; i < numRaycasts; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(cubeCenter, Random.onUnitSphere, out hit, 100f, cubeLayerMask))
            {
                GameObject hitObj = hit.transform.gameObject;

                if (!hitCubes.Contains(hitObj))
                {
                    hitObj.SetActive(false);
                    hitCubes.Add(hitObj);
                }
            }
        }
    }
}