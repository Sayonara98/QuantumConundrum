using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanningTurret : TurretController
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    float ScanTime = 30.0f;
    [SerializeField]
    BlueprintController BlueprintPrefab;

    float scanningTime = 0;

    bool isScanned = false;

    Blueprint IncommingBlueprint;

    protected override void OnAwake()
    {
        base.OnAwake();
        scanningTime = 0;
        if (slider)
        {
            slider.minValue = 0;
            slider.value = scanningTime;
            slider.maxValue = ScanTime;
        }

        Biome biome = MapManager.Instance.GetBiomeConfig(gameObject.transform.position);
        if (biome && biome.blueprint)
        {
            IncommingBlueprint = biome.blueprint;
        }
        else
        {
            //isScanned = true;
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        
        if(!isScanned && scanningTime < ScanTime)
        {
            scanningTime += Time.deltaTime;
            if (slider)
            {
                slider.value = scanningTime;
                if (scanningTime > ScanTime)
                {
                    isScanned = true;
                    Vector2 randomPoint = RandomPointInAnnulus(gameObject.transform.position, 2.0f, 5.0f);

                    Vector2 vector = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f);

                    BlueprintController blueprint = Instantiate(BlueprintPrefab, vector, Quaternion.identity);
                    blueprint.blueprint = IncommingBlueprint;

                    slider.gameObject.SetActive(false);
                }
            }
        }
    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // There are more efficient ways, but well
        float minRadius2 = minRadius * minRadius;
        float maxRadius2 = maxRadius * maxRadius;
        float randomDistance = Mathf.Sqrt(Random.value * (maxRadius2 - minRadius2) + minRadius2);
        return origin + randomDirection * randomDistance;
    }
}
