using UnityEngine;

public class VerticalLoop : MonoBehaviour
{
    [SerializeField] private float amplitude = 1f; // height
    [SerializeField] private float speed = 1f;     // frequency

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPos + Vector3.up * yOffset;
    }
}
