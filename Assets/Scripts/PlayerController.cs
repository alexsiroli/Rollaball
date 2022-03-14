using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool finished;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        finished = false;

        SetCountText();
        winText.enabled = false;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = finished ? 0 : movementVector.x;
        movementY = finished ? 0 : movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 8)
        {
            countText.enabled = false;
            winText.enabled = true;
            finished = true;
        }
    }
}
