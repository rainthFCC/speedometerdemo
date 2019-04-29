using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedometer : MonoBehaviour
{

    private const float MAX_SPEED_ANGLE = -110;
    private const float ZERO_SPEED_ANGLE = 110;

    private Transform needleTransform;
    private Transform speedLabelTemplateTransform;
    private Transform odometerTransform;
    private Transform digiSpeedTransform;

    private float speedMax;
    public float speed;
    private float odometer;

    private void Awake()
    {
        needleTransform = transform.Find("needle");
        speedLabelTemplateTransform = transform.Find("speedlabeltemplate");
        Debug.Log(speedLabelTemplateTransform.name);
        speedLabelTemplateTransform.gameObject.SetActive(false);
        odometerTransform = transform.Find("odometer");
        digiSpeedTransform = transform.Find("digiSpeed");

        speed = 0f;
        odometer = 0f;
        speedMax = 280f;
        CreateSpeedLabel();
    }

    // Update is called once per frame
    private void Update()
    {
        if (odometer < 0)
            odometer = 0;
        HandlePlayerInput();

        needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }

    private void HandlePlayerInput() {

        if (Input.GetMouseButton(0))
        {
            float acceleration = 50f;
            speed += acceleration * Time.deltaTime;
        }
        else {
            float deceleration = 20f;
            speed -= deceleration * Time.deltaTime;
         }
        if (speed > 0) {
            odometer += speed * Time.deltaTime / 60f;
            digiSpeedTransform.GetComponent<Text>().text = Mathf.RoundToInt(speed).ToString() + "kmh";
            odometerTransform.GetComponent<Text>().text = Mathf.RoundToInt(odometer).ToString() + "KM";
        }
        speed = Mathf.Clamp(speed, 0f, speedMax);
    }

    private void CreateSpeedLabel() {
        int labelAmount = 7;
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        for (int i = 0; i <= labelAmount; i++) {
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);
            float labelSpeedNormalized = (float)i / labelAmount;
            float speedLabelAngle = (ZERO_SPEED_ANGLE - labelSpeedNormalized * totalAngleSize)+90;
            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedLabelTransform.Find("speedText").GetComponent<Text>().text = Mathf.RoundToInt(labelSpeedNormalized * speedMax).ToString();
            speedLabelTransform.Find("speedText").eulerAngles = Vector3.zero;

            speedLabelTransform.gameObject.SetActive(true);
        }
    }


    private float GetSpeedRotation() {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        float speedNormalized = speed / speedMax;
        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }

}
