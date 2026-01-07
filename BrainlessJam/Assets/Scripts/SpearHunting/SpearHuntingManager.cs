using UnityEngine;

public class SpearHuntingManager : MonoBehaviour
{
    [SerializeField] GameObject spear;
    [SerializeField] GameObject[] targets;
    [SerializeField] SpearHuntingConfig config;

    [Header("Rotation Settings")]
    public GameObject objectToRotate;
    public Vector3 rotationAxis = Vector3.up; // Axis to rotate around
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float speed = 2f;

    [Header("Locked Rotation (Unused Axes)")]
    public Vector3 fixedEulerRotation; // Rotation that should NOT move


    private float startTime;
    private Quaternion baseRotation;
    private bool rotating;
    private int steps = 0;
    private Rigidbody spearRb;

    void Start()
    {
        startTime = Time.time;

        // Store the fixed rotation as a quaternion
        baseRotation = Quaternion.Euler(fixedEulerRotation);
        spearRb = spear.GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleRotation();
        if (Input.GetButtonDown("StopRotation"))
        {
            steps++;
            baseRotation = objectToRotate.transform.rotation;
            rotationAxis = Vector3.left;
            if (steps == 2)
            {
                spearRb.AddRelativeForce(0f, 0f, config.launchForce * 1000);
            }
        }
    }
    
    void HandleRotation()
    {
        float t = Mathf.PingPong((Time.time - startTime) * speed, 1f);
        float angle = Mathf.Lerp(minAngle, maxAngle, t);
        if (steps < 2)
        {    
            // Oscillation rotation
            Quaternion oscillation = Quaternion.AngleAxis(angle, rotationAxis);

            // Combine fixed rotation + oscillation
            objectToRotate.transform.localRotation = baseRotation * oscillation;
        }
    }
}
