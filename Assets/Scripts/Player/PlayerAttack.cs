using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    //private varible for unity input system
    private bool _attack = false;

    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private int _damage = 10;

    private float _attackRange = 0.5f;
    [SerializeField]
    private LayerMask _enemyLayers;
    private int _playerID;

    public void Start()
    {
        // Obtener el ID único del jugador
        _playerID = GetComponent<PlayerIdentifier>().playerID;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        _attack = context.action.triggered;
    }

    // Update is called once per frame
    void Update()
    {
        if(_attack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        //Player animation init

        _anim.SetTrigger("Attack");

        //Detect any enemy
        Collider[] hitEnemy = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);
        //Set damage
        foreach(Collider enemy in hitEnemy)
        {
            // Verificar que el jugador detectado no sea el mismo que ataca
            PlayerIdentifier identifier = enemy.GetComponent<PlayerIdentifier>();
            if (identifier != null && identifier.playerID != _playerID)
            {
                // Aplicar daño al jugador enemigo
                enemy.GetComponent<PlayerDamage>().TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
