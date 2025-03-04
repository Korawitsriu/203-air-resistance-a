using UnityEngine;

public class Airplane : MonoBehaviour
{
    [SerializeField] Rigidbody rb;  // ✨ Unchanged
    [SerializeField] float enginePower = 50f;  // ✨ Unchanged
    [SerializeField] float liftBooster = 0.5f;  // ✨ Unchanged
    [SerializeField] float drag = 0.003f;  // ✨ Unchanged
    [SerializeField] float angularDrag = 0.03f;  // ✨ Unchanged

    [SerializeField] float yawPower = 50f;   // Turning speed ✨ Unchanged
    [SerializeField] float pitchPower = 50f; // Nose up/down speed ✨ Unchanged
    [SerializeField] float rollPower = 30f;  // ✨ Unchanged

    // ✨ Event function _ @wyadath
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // ✨ Event function _ @wyadath
    void FixedUpdate()
    {
        // 1. Engine Thrust (Engine Power)
        // Pressing Spacebar applies force in the forward direction of the airplane (transform.forward).
        // Simulates engine thrust, making the airplane accelerate forward.
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(-transform.forward * enginePower);
        }

        // 2. Lift Force - Simulates how airplanes gain altitude
        Vector3 lift = Vector3.Project(rb.linearVelocity, transform.forward);
        rb.AddForce(transform.up * lift.magnitude * liftBooster);

        // 3. Drag (Air Resistance) - Prevents infinite acceleration
        rb.linearDamping = rb.linearVelocity.magnitude * drag;
        rb.angularDamping = rb.linearVelocity.magnitude * angularDrag;

        // 4. Rotation Controls - Pitch, Yaw, and Roll
        float yaw = Input.GetAxis("Horizontal") * yawPower;   // Left/Right (A/D)
        float pitch = Input.GetAxis("Vertical") * pitchPower; // Nose Up/Down (W/S)
        float roll = Input.GetAxis("Roll") * rollPower;       // Roll (Q/E)

        rb.AddTorque(transform.up * yaw);       // Yaw (Turn Left/right)
        rb.AddTorque(transform.right * pitch);  // Pitch (Nose Up/down)
        rb.AddTorque(transform.forward * roll); // Roll (Tilting)
    }
}
