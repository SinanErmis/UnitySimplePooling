using UnityEngine;

namespace SimplePooling.Runtime
{
    public class ComponentPool<T> : BasePool<T> where T : Component
    {
        public bool ToggleActiveStatusBeforeGetAndAdd;
        
        public ComponentPool(T prefab, bool toggleActiveStatusBeforeGetAndAdd) : base(prefab)
        {
            ToggleActiveStatusBeforeGetAndAdd = toggleActiveStatusBeforeGetAndAdd;
        }

        protected override T CreateObject()
        {
            return Object.Instantiate(Original);
        }

        protected override void OnBeforeAddObject(T obj)
        {
            if (ToggleActiveStatusBeforeGetAndAdd)
            {
                obj.gameObject.SetActive(false);
            }
        }

        protected override void OnBeforeGetObject(T obj)
        {
            if (ToggleActiveStatusBeforeGetAndAdd)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}