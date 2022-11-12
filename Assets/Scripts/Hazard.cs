using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

    Vector3 rotation;

    public ParticleSystem breakingEffect;

    private CinemachineImpulseSource cinemachineImpulseSource;
    private Player player;

    // Colocando rotação no eixo "x" do hazard
    private void Start() {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        player = FindObjectOfType<Player>();

        var xRotation = Random.Range(90f, 180f); // Definindo uma rotação aleatória entre um valor e outro
        rotation = new Vector3(-xRotation, 0);
    }

    private void Update() {
        transform.Rotate(rotation * Time.deltaTime);
    }

    // Destruindo o hazard ao colidir com algo
    private void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.CompareTag("Hazard")) 
        {
            Destroy(gameObject);
            //Debug.Log("Destruindo o hazard");
            Instantiate(breakingEffect, transform.position, Quaternion.identity);

            if (player != null) 
            {
                var distance = Vector3.Distance(transform.position, player.transform.position);
                var force = 1f / distance;
                //Debug.Log(force);

                cinemachineImpulseSource.GenerateImpulse(force);
            }
        }
    }
}
