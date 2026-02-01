using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "abilityData", menuName = "Scriptable Objects/abilityData")]
public class abilityData : ScriptableObject
{
    [Header("Meta")]
    public string id;
    public AbilityCategory category;
    public int tier; // 1 - 4

    [Header("Requisitos")]
    public abilityData prerequisite;

    [Header("Efectos")]
    public List<AbilityEffect> effects;

    public enum AbilityCategory
    {
        Life,
        Shield,
        Weapon,
        Drone,
        Bullet
    }

    [System.Serializable]
    public class AbilityEffect
    {
        public AbilityEffectType type;
        public float value;
    }

    public enum AbilityEffectType
    {
        // Vida
        MaxHealth,
        RegenOnPossess,
        HealthOnKill,
        RegenWhileIdle,

        // Escudo
        ShieldNegateHit,
        ShieldPush,
        ShieldCooldown,
        ShieldSaveChance,

        // Arma
        CadencyMultiplier,
        MaxAmmo,
        AmmoBonus,
        ReloadSpeedMultiplier,
        AmmoOnShootChance,
        DoubleReloadChance,

        // Drone
        DroneEnable,
        DroneBulletDamage,
        DroneFireRate,
        DroneOrbit,
        DroneOrbitDamage,
        DroneDoubleShot,
        DroneBurn,

        // Balas
        BulletDamageMultiplier,
        BulletBurnChance,
        BulletBounce,
        BulletDoubleShot
    }

}

