using UnityEngine;
using UnityEngine.InputSystem;

public class TimeRewinder : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxFrames = 300;
    public static bool isRewinding = false;

    private CircularBuffer<Vector3> positionHistory;
    private CircularBuffer<Quaternion> rotationHistory;
    private CircularBuffer<Vector3> scaleHistory;
    private CircularBuffer<Vector2> linearVelocityHistory;

    private Rigidbody2D rBody;

    

    private void Awake()
    {
        positionHistory = new CircularBuffer<Vector3>(maxFrames);
        rotationHistory = new CircularBuffer<Quaternion>(maxFrames);
        scaleHistory = new CircularBuffer<Vector3>(maxFrames);
        linearVelocityHistory = new CircularBuffer<Vector2>(maxFrames);

        rBody = GetComponent<Rigidbody2D>();
    }

    // Handle the "Rewind" action from the Input System
    public void OnRewind(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isRewinding = true;
            Debug.Log("Rewind Performed");
        }
        else if (context.canceled)
        {
            isRewinding = false;
            Debug.Log("Rewind Cancelled");
        }
    }

    private void FixedUpdate()
    {
        if(isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    // Record
    private void Record()
    {
        positionHistory.Push(transform.position);
        rotationHistory.Push(transform.rotation);
        scaleHistory.Push(transform.localScale);
        linearVelocityHistory.Push(rBody.linearVelocity);
    }

    // Rewind
    private void Rewind()
    {
        if(rotationHistory.Count > 0)    // Make sure my buffer has something in it
        {
            transform.position = positionHistory.Pop();
            transform.rotation = rotationHistory.Pop();

            Vector3 tempLocalScale = scaleHistory.Pop();
            transform.localScale = new Vector3(tempLocalScale.x * -1, tempLocalScale.y, tempLocalScale.z);
        }
        else
        {
            isRewinding = false;    // Stop if we run out of items to process.
        }
    }
}
