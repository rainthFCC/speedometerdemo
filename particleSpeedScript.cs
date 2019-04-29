using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSpeedScript : MonoBehaviour
{

    private speedometer sdmtr;
    private ParticleSystem ps1, ps2;

    // Start is called before the first frame update

    void Awake()
    {
        ps1 = transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        ps2 = transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        GameObject speedometerGO = GameObject.Find("speedometerReal");
        sdmtr = speedometerGO.GetComponent<speedometer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sdmtr.speed > 0) {
            var main = ps1.main;
            var main2 = ps2.main;
            if (sdmtr.speed > 120)
            {
                main.startSpeed = 120f;
                main2.startSpeed = 120f;
            }
            else
            {
                main.startSpeed = sdmtr.speed;
                main2.startSpeed = sdmtr.speed;
            }
            ps1.GetComponent<ParticleSystemRenderer>().lengthScale = sdmtr.speed / 15;
            ps2.GetComponent<ParticleSystemRenderer>().lengthScale = sdmtr.speed / 15;
        }
    }
}
