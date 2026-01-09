using UnityEngine;

public class Floating : MonoBehaviour
{
    public float amplitude = 0.25f;
    public float speed = 1f;

    Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startLocalPos + Vector3.up * y;
    }
}
