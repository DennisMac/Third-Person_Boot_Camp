using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack : MonoBehaviour
{
    private Transform m_Cam;
    public float fuel = 10;
    public float maxFuel = 10;
    public float replenishRate = 5f;

    // GameObject parent;
    public Vector3 ThrustStrength;// = Vector3.zero;
    public float drag = 0.2f;
    Rigidbody rBody;


    void Start()
    {
        m_Cam = Camera.main.transform;
        rBody = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    float jetPackDelay = 0f;
    void FixedUpdate()
    {
        bool jetting = false;
        Vector3 vectoredThrust = Vector3.zero;
        float jetPackThrust = Input.GetAxis("Fire2");
        if (jetPackThrust > 0.2f)
        {
            jetPackDelay += Time.deltaTime;
            if (jetPackDelay < 0.25f)
                jetPackThrust = 0f;
        }
        else
        {
            jetPackDelay = 0f;
        }

        if (jetPackThrust > 0.2f && fuel > 0f)
        {
            fuel -= jetPackThrust * Time.deltaTime;

                vectoredThrust = (-drag * rBody.velocity.magnitude * rBody.velocity); // velocity squared
            
            vectoredThrust += transform.rotation * (jetPackThrust * new Vector3( ThrustStrength.x, ThrustStrength.y, ThrustStrength.z));

            jetting = true;
        }
        else
        {
            if (fuel < maxFuel) fuel += Time.deltaTime * replenishRate;

        }
        rBody.AddForce(vectoredThrust);
    }
}



