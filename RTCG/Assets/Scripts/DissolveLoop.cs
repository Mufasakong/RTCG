using UnityEngine;

public class DissolveLoop : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private string cutoffProperty = "_CutOff";
    [SerializeField] private float speed = 5f;

    private float cutoff = 15f;

    void Update()
    {
        cutoff -= speed * Time.deltaTime;

        if (cutoff < -15f)
            cutoff = 15f;

        targetRenderer.material.SetFloat(cutoffProperty, cutoff);
    }
}
