// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// interface IAilmentDealer
// {
//   public BaseAilmentSO GetAilment();
//   public Collider GetCollider();
// }

// public class AilmentStatus
// {
//   public Collider AgentCollider;
//   public BaseAilmentSO Ailment;
//   public Coroutine Routine;
//   public float TimeRemaining;

//   public AilmentStatus(BaseAilmentSO ailment, Collider agentCollider)
//   {
//     AgentCollider = agentCollider;
//     Ailment = ailment;
//     TimeRemaining = ailment.Duration;
//   }
// }

// public class AilmentController : MonoBehaviour
// {
//   private Dictionary<int, AilmentStatus> currAilments;

//   private void Awake()
//   {
//     currAilments = new Dictionary<int, AilmentStatus>();
//   }

//   // As of right now, no ailment stacking exists.  Reapplying an ailment
//   // just refreshes its timer
//   private void OnTriggerEnter(Collider other)
//   {
//     RegisterOrRefreshAilment(other.gameObject);
//   }

//   private void OnCollisionEnter(Collision other)
//   {
//     RegisterOrRefreshAilment(other.gameObject);
//   }

//   private void RegisterOrRefreshAilment(GameObject gameObject)
//   {
//     IAilmentDealer ailmentDealer = gameObject.GetComponent<IAilmentDealer>();
//     if (ailmentDealer != null)
//     {
//       BaseAilmentSO ailment = ailmentDealer.GetAilment();
//       Collider collider = ailmentDealer.GetCollider();
//       if (currAilments.ContainsKey(ailment.Id))
//       {
//         RefreshAilment(ailment);
//       }
//       else
//       {
//         RegisterAilment(ailment, collider);
//       }
//     }
//   }

//   private void Update()
//   {
//     foreach (AilmentStatus ailmentStatus in currAilments.Values)
//     {
//       ailmentStatus.TimeRemaining -= Time.deltaTime;
//     }
//   }

//   private void RegisterAilment(BaseAilmentSO ailment, Collider agentCollider)
//   {
//     AilmentStatus ailmentStatus = new AilmentStatus(ailment, agentCollider);
//     currAilments.Add(ailment.Id, ailmentStatus);
//     ailmentStatus.Ailment.OnStart(gameObject, ailmentStatus.AgentCollider);
//     ailmentStatus.Routine = StartCoroutine(ExecuteAilmentRoutine(ailment));
//   }

//   private void RefreshAilment(BaseAilmentSO ailment)
//   {
//     AilmentStatus ailmentStatus = currAilments[ailment.Id];
//     ailmentStatus.TimeRemaining = ailment.Duration;
//   }

//   public void RemoveAilment(BaseAilmentSO ailment)
//   {
//     AilmentStatus ailmentStatus = currAilments[ailment.Id];
//     StopCoroutine(ailmentStatus.Routine);
//     ailmentStatus.Ailment.OnFinish(gameObject, ailmentStatus.AgentCollider);
//     currAilments.Remove(ailment.Id);
//   }

//   public void ClearAllAilments()
//   {
//     foreach (AilmentStatus ailmentStatus in currAilments.Values)
//     {
//       StopCoroutine(ailmentStatus.Routine);
//       ailmentStatus.Ailment.OnFinish(gameObject, ailmentStatus.AgentCollider);
//     }

//     currAilments.Clear();
//   }

//   private IEnumerator ExecuteAilmentRoutine(BaseAilmentSO ailment)
//   {
//     AilmentStatus ailmentStatus = currAilments[ailment.Id];

//     while (ailmentStatus.TimeRemaining > 0)
//     {
//       ailmentStatus.Ailment.Tick(gameObject, ailmentStatus.AgentCollider);
//       yield return CoroutineUtils.GetWaitForSeconds(ailment.Rate);
//     }

//     ailmentStatus.Ailment.OnFinish(gameObject, ailmentStatus.AgentCollider);
//     currAilments.Remove(ailmentStatus.Ailment.Id);
//   }
// }