using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanningTurret : TurretController
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    float ScanTime = 10.0f;

    float scanningTime = 0;

    bool isScanned = false;

    List<ItemData> IncommingItems;

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
        if (biome && biome.Items.Count > 0)
        {
            IncommingItems = biome.Items;
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

                    Vector2 vector = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f);

                    int index = 0;
                    InventoryManager inventoryManager = InventoryManager.Instance;
                    foreach(ItemData data in IncommingItems)
                    {
                        if(data != null)
                        {
                            if(data.Info.ItemType == ItemType.TurretBlueprint && inventoryManager.GetItem(data.Info.Name))
                            {
                                continue;
                            }

                            GameObject gameObject = Instantiate(data.Info.ItemPrefab, vector, Quaternion.identity);
                            ResourceController resourceController = gameObject.GetComponent<ResourceController>();
                            if(resourceController)
                            {
                                resourceController.Data = data;
                                resourceController.DelayTime = index * 0.25f;
                            }
                        }
                        index++;
                    }
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
