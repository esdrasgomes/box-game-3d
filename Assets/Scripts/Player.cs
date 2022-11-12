using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float forceMultiplier = 6f;
    public float maximumVelocity = 4f;
    public ParticleSystem deathParticles;
    public GameObject mainVCam;
    public GameObject zoomVCam;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

/*
    void Awake()
    {
        #if UNITY_EDITOR
        //    QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 0; // frames por segundo
        #endif
    }
*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude <= maximumVelocity)
        {
            rb.AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime, 0, 0));
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.GameOver();
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            // Debug.Log("Destruindo o player");
            cinemachineImpulseSource.GenerateImpulse();

            // Desabilitando câmera original e habilitando zoom ao dar "game over"
            mainVCam.SetActive(false);
            zoomVCam.SetActive(true);
        }
    }
}
