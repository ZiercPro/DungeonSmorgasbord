using UnityEngine;

[CreateAssetMenu(menuName = "ScriptObj/Attributes/Enemy", fileName = "Enemy")]
public class EnemyAttributeSO : AttributesBaseSO
{
    public int difficulty;
    public int damageAmount;
    public DamageType damageType;
    public LayerMask attackLayer;
    public float attackSpeed;
    public float attackRange;
}
