using UnityEngine;

namespace SimplePooling.Runtime
{
    public class GameObjectPool : BasePool<GameObject>
    {
        public bool ToggleActiveStatusBeforeGetAndAdd;
        
        public GameObjectPool(GameObject prefab, bool toggleActiveStatusBeforeGetAndAdd) : base(prefab)
        {
            ToggleActiveStatusBeforeGetAndAdd = toggleActiveStatusBeforeGetAndAdd;
        }

        protected override GameObject CreateObject()
        {
            return Object.Instantiate(Original);
        }

        protected override void OnBeforeAddObject(GameObject obj)
        {
            if (ToggleActiveStatusBeforeGetAndAdd)
            {
                obj.SetActive(false);
            }
        }

        protected override void OnBeforeGetObject(GameObject obj)
        {
            if (ToggleActiveStatusBeforeGetAndAdd)
            {
                obj.SetActive(true);
            }
        }
    }
}