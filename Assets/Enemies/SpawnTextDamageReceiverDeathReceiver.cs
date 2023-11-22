using UnityEngine;

public class SpawnTextDamageReceiverDeathReceiver : MonoBehaviour, IDamageReceiver, IDeathReceiver
{
    // Its a bit weird that we arent just doing the floating text spawning in the death receiver
    [SerializeField] private GiveExpDeathReceiver _expDeathReceiver;
    [SerializeField] private GameObject _floatingTextPrefab;
    [SerializeField] private float _spread = 0.2f;

    private Transform _floatingTextContainer;

    private void Start()
    {
        _floatingTextContainer = TagUtils.FindWithTag(TagName.FloatingTextContainer).transform;
    }

    public void OnDeath(GameObject deathDealer)
    {
        // You don't get money if the cause of death was END OF DAY lol
        // TODO: IS THERE A BETER WAY
        if (TagUtils.CompareTag(deathDealer, TagName.EnemySpawner))
        {
            return;
        }

        InstantiateWithName($"+{_expDeathReceiver.GetExpContribution()} XP");
    }

    public void OnReceiveDamage(float percentHealthRemaining, GameObject damageDealer, float rawDamageDealt)
    {
        InstantiateWithName(rawDamageDealt.ToString());
    }

    private void InstantiateWithName(string name)
    {
        Vector3 position = transform.position + Random.insideUnitSphere * _spread;

        // Pass the damage data through by game object name.  This is cheeky....
        GameObject instantiated = Instantiate(_floatingTextPrefab, position, Quaternion.identity, _floatingTextContainer);
        instantiated.name = name;
    }
}
