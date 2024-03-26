using UnityEngine;
using UnityEngine.Events;
[System.Obsolete]
public class EnemyAI : MyInput
{
    [Header("Static")]
    [Header("Unity Event")]
    [Space]
    public UnityEvent<Vector2> OnTarPositionInput;
    public UnityEvent<Vector2> OnMoveInput;
    public UnityEvent OnAttackInput;

    protected bool isTargetExist;

    private Transform attackTarget;

    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    private void Awake()
    {
        InitEvent();
        FindDefaultTarget();
    }
    private void Update()
    {
        IsTargetExist();
        switch (isTargetExist)
        {
            case true:
            UpdateMoveDir();
            OnMoveInput?.Invoke(moveDir);
            OnTarPositionInput?.Invoke(viewPos);
            break;
            case false:
            if (moveDir.magnitude != 0)
            {
                moveDir = Vector2.zero;
                OnMoveInput?.Invoke(moveDir);
            }
            break;
        }
    }
    #region EnemyAI
    private void FindDefaultTarget()
    {
        attackTarget = FindAnyObjectByType<InputManager>().transform;
    }
    protected void UpdateMoveDir()
    {
        viewPos = attackTarget.position;
        if (CheckAttackPerform())
        {
            OnAttackInput?.Invoke();
            moveDir = Vector2.zero;
        }
        else
        {
            moveDir = (viewPos - (Vector2)transform.position).normalized;
        }
    }
    protected void IsTargetExist()
    {
        if (viewPos == null || !attackTarget.gameObject.activeSelf)
        {
            isTargetExist = false;
            return;
        }
        if (attackTarget.TryGetComponent(out Health h))
        {
            if (h == null || !(h.currentHealth > 0) || !h.isActiveAndEnabled)
            {
                isTargetExist = false;
                return;
            }
        }
        isTargetExist = true;
    }
    protected bool CheckAttackPerform()
    {
        return MyMath.CompareDistanceWithRange(attackPos.position, viewPos, attackRange);
    }
    #endregion

    #region Input
    protected override bool IsEventBeSet()
    {
        if (OnMoveInput != null || OnAttackInput != null || OnAttackInput != null) return true; return false;
    }
    protected override void InitEvent()
    {
        if (!IsEventBeSet())
        {
            OnTarPositionInput = new UnityEvent<Vector2>();
            OnMoveInput = new UnityEvent<Vector2>();
            OnAttackInput = new UnityEvent();
        }
    }
    #endregion
}
