using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public AudioClip hitBlock; // Âm thanh va chạm Block
    private AudioSource audioSource; // Nguồn phát âm thanh

    // Start is called before the first frame update
    void Start()
    {
        // Lấy AudioSource gắn trên GameObject này
        audioSource = GetComponent<AudioSource>();
        // Nếu chưa có thì tự thêm vào
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ramp") || collision.gameObject.CompareTag("Floor"))
        {
            // Debug.Log("Va chạm với Ramp or Floor");
            // Kiểm tra nếu có âm thanh thì phát
            if (audioSource != null)
            {
                // Debug.Log("Phát âm thanh va chạm Ramp or Floor");
                audioSource.enabled = true; // Bật lại nếu bị disable
                audioSource.Play();
            }
        }
        else if (collision.gameObject.CompareTag("Block"))
        {
            // Debug.Log("Va chạm với Block");
            // Có thể phát âm thanh khác, hoặc không làm gì
            if  (audioSource != null && hitBlock != null)
            {
                // Debug.Log("Phát âm thanh va chạm Block");
                audioSource.enabled = true; // Bật lại nếu bị disable
                audioSource.PlayOneShot(hitBlock);
            }
        }
    }
}
