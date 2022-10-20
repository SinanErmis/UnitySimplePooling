using UnityEngine;

namespace SimplePooling.Runtime
{
    //This class is implemented to prevent any possible confusion between MonoBehaviorPool and ComponentPool 
    public class MonoBehaviorPool<T> : ComponentPool<T> where T : MonoBehaviour
    {
        public MonoBehaviorPool(T prefab, bool toggleActiveStatusBeforeGetAndAdd) : base(prefab, toggleActiveStatusBeforeGetAndAdd)
        {
        }
    }
}