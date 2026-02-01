using UnityEngine;

[CreateAssetMenu(fileName = "bulletData", menuName = "Scriptable Objects/bulletData")]

public class bulletData : ScriptableObject
{
    [SerializeField] private string bulletName;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private bool doBounce;
    [SerializeField] private bool isFireDamage;

    public string BulletName { get { return bulletName; } }
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public bool DoBounce { get { return doBounce; } }
    public bool IsFireDamage { get { return isFireDamage; } }

}
