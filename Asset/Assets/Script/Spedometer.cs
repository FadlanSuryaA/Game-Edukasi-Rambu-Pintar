using UnityEngine;
using TMPro;

public class Spedometer : MonoBehaviour
{
    public Rigidbody carRb;
    public TextMeshProUGUI speedText;

    private float displayedSpeed;

    void Update()
    {
        float speed = carRb.linearVelocity.magnitude * 3.6f;

        // Smooth speed
        displayedSpeed = Mathf.Lerp(displayedSpeed, speed, Time.deltaTime * 5f);

        speedText.text = Mathf.FloorToInt(displayedSpeed) + " KM/H";
    }
}