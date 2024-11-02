using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEventRasied;
    public void RaiseEvent()
    {
        OnEventRasied?.Invoke();    
    }
}
