using UnityEngine;

public class Transformations : MonoBehaviour
{
    CreateGrid createGrid;

    Vector3[] baseLocalPositions;
    Vector3 startScale;

    [Header("Movement Controls")]
    public float moveFrequency = 2f;
    public float moveAmplitude = 5f;
    public float moveOffset = 0f;

    [Header("Scale Controls")]
    public float scaleFrequency = 2f;
    public float scaleAmplitude = 5f;
    public float scaleOffset = 0f;

    [Header("Rotation Controls")]
    public float rotationSpeed = 1f;

    void Start()
    {
        createGrid = GetComponent<CreateGrid>();

        int count = createGrid.grid.Length;
        baseLocalPositions = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            baseLocalPositions[i] = createGrid.grid[i].localPosition;
        }

        startScale = createGrid.grid[0].localScale;
    }

    void Update()
    {
        for (int i = 0; i < createGrid.grid.Length; i++)
        {
            Transform t = createGrid.grid[i];

            // Position animation
            t.localPosition =
                baseLocalPositions[i]
                + Vector3.up * Mathf.Sin(Time.time * moveFrequency + moveOffset) * moveAmplitude;

            // Scale animation
            t.localScale =
                startScale * Mathf.Sin(Time.time * scaleFrequency) * scaleAmplitude
                + Vector3.one * scaleOffset;

            // Rotation animation
            t.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
}
