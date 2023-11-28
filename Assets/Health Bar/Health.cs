using UnityEngine;

// enemies can be damageable, which have simple monobehaviour based health,
// but the player can also be damageable, which has a more complicated
// scriptable object based health system.
public interface IDamageable
{
    public void SetHealth(float to);
    public float GetHealth();
    public float GetTotalHealth();
}

public interface IDamageReceiver
{
    public void OnReceiveDamage(float percentHealthRemaining, GameObject damageDealer, float rawDamageDealt);
}

public interface IDeathReceiver
{
    public void OnDeath(GameObject deathDealer);
}

public class Health : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    private IDamageable _damageable;

    [Header("Debug Options")]
    [SerializeField] private bool _printReceiverOrders = false;

    private IDamageReceiver[] _damageReceivers;
    private IDeathReceiver[] _deathReceivers;

    private bool _dead = false;

    private void Awake()
    {
        // TODO: this isn't pretty... idk if there is a smarter way to do this
        _damageable = GetComponent<IDamageable>();
        if (_damageable == null)
        {
            _damageable = _playerStatsSO;
        }
        if (_damageable == null)
        {
            Debug.LogWarning("instantiated a health MonoBehaviour without an attached IDamageable!");
        }

        _damageReceivers = GetComponentsInChildren<IDamageReceiver>();
        _deathReceivers = GetComponentsInChildren<IDeathReceiver>();

        if (_printReceiverOrders)
        {
            print("Damage receivers:");
            foreach (var receiver in _damageReceivers)
            {
                print(receiver.GetType().ToString());
            }

            print("Death receivers:");
            foreach (var receiver in _deathReceivers)
            {
                print(receiver.GetType().ToString());
            }
        }
    }

    // returns true if health is 0 after decrement
    public bool DecrementHealth(float amount, GameObject damageDealer)
    {
        // in case this is called multiple times in the same frame, (e.g.) lots of deadly damage!
        // make sure we dont double proc the death receivers
        if (_dead)
        {
            return true;
        }

        _damageable.SetHealth(_damageable.GetHealth() - amount);
        if (_damageable.GetHealth() <= 0)
        {
            _dead = true;
            _damageable.SetHealth(0);
            ProcessDeathReceivers(damageDealer);
            return true;
        }
        else
        {
            ProcessDamageReceivers(_damageable.GetHealth() / _damageable.GetTotalHealth(), damageDealer, amount);
            return false;
        }
    }

    public void Kill(GameObject damageDealer)
    {
        _damageable.SetHealth(0);
        ProcessDeathReceivers(damageDealer);
    }

    private void ProcessDamageReceivers(float healthRemaining, GameObject damageDealer, float rawDamageDealt)
    {
        foreach (IDamageReceiver damageReceiver in _damageReceivers)
        {
            damageReceiver.OnReceiveDamage(healthRemaining, damageDealer, rawDamageDealt);
        }
    }

    private void ProcessDeathReceivers(GameObject damageDealer)
    {
        foreach (IDeathReceiver deathReceiver in _deathReceivers)
        {
            deathReceiver.OnDeath(damageDealer);
        }
    }
}