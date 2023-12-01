using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHelper : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI BiomeName;
    [SerializeField] TextMeshProUGUI BiomeBonus;
    [SerializeField] TextMeshProUGUI ScanResult;
    // Start is called before the first frame update
    private void Update()
    {
        Biome biome= MapManager.Instance.GetBiomeConfig(player.transform.position);
        switch (biome.type)
        {
            case BiomeType.MUD:
                BiomeName.text ="You are on MUD";
                BiomeBonus.text = "All Turret range x1.5";
                ScanResult.text = "Scan result: Sniper Blueprints";
                break;
            case BiomeType.JUNGLE:
                BiomeName.text = "You are on JUNGLE";
                BiomeBonus.text = "All Turret Damage x1.5, Enemy Drop x1.5";
                ScanResult.text = "Scan result: Blade Blueprints";
                break;
            case BiomeType.PLAINS:
                BiomeName.text = "You are on PLAINS";
                BiomeBonus.text = "All Turret Damage x1.5";
                ScanResult.text = "Scan result: Flamethrower Blueprints";
                break;
            case BiomeType.MOUNTAINS:
                BiomeName.text = "You are on MOUNTAINS";
                BiomeBonus.text = "All Fire rate x1.5";
                ScanResult.text = "Scan result: Lightning Blueprints";
                break;
            default:
                BiomeName.text = "Biome Unknown";
                BiomeBonus.text = "N/A";
                ScanResult.text = "N/A";
                break;
        }
    }
}
