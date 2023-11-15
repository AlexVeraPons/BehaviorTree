using UnityEngine;

public class WASDController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        // Get the horizontal and vertical input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement vector
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * _speed * Time.deltaTime;

        // Map the movement to the correct directions for a 2D controller
        movement = transform.TransformDirection(movement);

        // Move the object
        transform.position += movement;
    }
}
