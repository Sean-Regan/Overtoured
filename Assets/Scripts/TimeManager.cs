using System;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI timeText;
    
    [SerializeField] Light sun;
    [SerializeField] Light moon;
    [SerializeField] AnimationCurve lightIntensityCurve;
    [SerializeField] float maxSunIntensity = 1;
    [SerializeField] float maxMoonIntensity = 0.5f;
    
    [SerializeField] Color dayAmbientLight;
    [SerializeField] Color nightAmbientLight;
    [SerializeField] Volume volume;
    [SerializeField] Material skyboxMaterial;

    [SerializeField] GameObject people;
    bool peopleEnabled = false;
    
    ColorAdjustments colorAdjustments;
    
    [SerializeField] TimeSettings timeSettings;
    
    public event Action OnSunrise {
        add => service.OnSunrise += value;
        remove => service.OnSunrise -= value;
    }
    
    public event Action OnSunset {
        add => service.OnSunset += value;
        remove => service.OnSunset -= value;
    }
    
    public event Action OnHourChange {
        add => service.OnHourChange += value;
        remove => service.OnHourChange -= value;
    }    

    TimeService service;

    void Start() {
        service = new TimeService(timeSettings);
        volume.profile.TryGet(out colorAdjustments);
        OnSunrise += () => TogglePeople();
    }

    void Update() {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        UpdateSkyBlend();
    }

    void TogglePeople()
    {
        if(peopleEnabled)
        {
            people.gameObject.SetActive(false);
            peopleEnabled = false;
        }
        else
        {
            people.gameObject.SetActive(true);
            peopleEnabled = true;
        }
    }

    void UpdateSkyBlend() {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.up);
        float blend = Mathf.Lerp(0, 1, lightIntensityCurve.Evaluate(dotProduct));
        skyboxMaterial.SetFloat("_Blend", blend);
    }
    
    void UpdateLightSettings() {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.down);
        float lightIntensity = lightIntensityCurve.Evaluate(dotProduct);
        
        sun.intensity = Mathf.Lerp(0, maxSunIntensity, lightIntensity);
        moon.intensity = Mathf.Lerp(maxMoonIntensity, 0, lightIntensity);
        
        if (colorAdjustments == null) return;
        colorAdjustments.colorFilter.value = Color.Lerp(nightAmbientLight, dayAmbientLight, lightIntensity);
    }

    void RotateSun() {
        float rotation = service.CalculateSunAngle();
        sun.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.right);
    }

    void UpdateTimeOfDay() {
        service.UpdateTime(Time.deltaTime);
        if (timeText != null) {
            timeText.text = RoundDown(service.CurrentTime).ToString("hh:mm");
        }
    }

    DateTime RoundDown(DateTime dt)
    {
        var delta = dt.Minute % 10;
        return dt.AddMinutes(-delta);
    }
}