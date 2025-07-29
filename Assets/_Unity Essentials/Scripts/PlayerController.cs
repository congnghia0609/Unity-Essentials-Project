using UnityEngine;
using System; // Required for Type handling

// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.
    public float jumpForce = 5.0f;
    private AudioSource audioSource; // Nguồn phát âm thanh
    public GameObject onFinalEffect;
    public AudioClip audioWin; // Âm thanh Winner.

    private Rigidbody rb; // Reference to player's Rigidbody.

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
        // Lấy AudioSource gắn trên GameObject này
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null) return; // Prevent access if Rigidbody is missing

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }


    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        if (rb == null) return; // Prevent access if Rigidbody is missing

        // Move player based on vertical input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
    
    private int CheckFinalCollectible()
    {
        int totalCollectibles = 0;

        // Check and count objects of type Collectible
        Type collectibleType = Type.GetType("Collectible");
        if (collectibleType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsOfType(collectibleType).Length;
        }

        return totalCollectibles;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            // Kiểm tra nếu có âm thanh thì phát
            // if (hitSound != null && audioSource != null) {
            //     audioSource.PlayOneShot(hitSound);
            // }
            if (audioSource != null)
            {
                int totalCollectibles = CheckFinalCollectible();
                // Debug.Log($"totalCollectibles = {totalCollectibles}");
                if (totalCollectibles == 1)
                {
                    if  (audioSource != null && audioWin != null)
                    {
                        // Debug.Log("Phát âm thanh Winner");
                        audioSource.enabled = true; // Bật lại nếu bị disable
                        audioSource.PlayOneShot(audioWin);
                    }
                    // instantiate the particle effect
                    Instantiate(onFinalEffect, transform.position, transform.rotation);
                }
                else
                {
                    // Debug.Log("Phát âm thanh va chạm");
                    audioSource.enabled = true; // Bật lại nếu bị disable
                    audioSource.Play();
                }
            }
        }
    }
}
