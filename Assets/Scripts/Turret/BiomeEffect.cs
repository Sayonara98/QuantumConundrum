using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeEffect : MonoBehaviour
{
    [SerializeField]
    float PlainBiomDmgMultiplier = 2f;
    [SerializeField]
    float MudBiomRangeMultiplier = 1.5f;
    [SerializeField]
    float JungleBiomDmgMultiplier = 1.5f;
    [SerializeField]
    float JungleBiomRscMultiplier = 1.5f;
    [SerializeField]
    float MountainBiomFireRateMultiplier = 1.5f;

    public float PlainBiomeBuffDamage(float damage)
    {
        if (MapManager.Instance.GetBiomeType(transform.position) == BiomeType.PLAINS)
            return damage * PlainBiomDmgMultiplier;
        return damage;
    }

    public float MudBiomeBuffRange(float range)
    {
        if (MapManager.Instance.GetBiomeType(transform.position) == BiomeType.MUD)
            return range * MudBiomRangeMultiplier;
        return range;
    }

    public float JungleBiomeBuffDamage(float damage)
    {
        if (MapManager.Instance.GetBiomeType(transform.position) == BiomeType.JUNGLE)
            return damage * JungleBiomDmgMultiplier;
        return damage;
    }

    public float JungleBiomeBuffResource(float resource)
    {
        if (MapManager.Instance.GetBiomeType(transform.position) == BiomeType.JUNGLE)
            return resource * JungleBiomRscMultiplier;
        return resource;
    }

    public float MountainBiomeBuffRate(float fireRate)
    {
        if (MapManager.Instance.GetBiomeType(transform.position) == BiomeType.MOUNTAINS)
            return fireRate * MountainBiomFireRateMultiplier;
        return fireRate;
    }

}
