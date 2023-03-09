using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AKhvalov.IdleFarm.Runtime.Pool
{
    public class GameObjectPool
    {
        public event Action OnObjectDeactivated;
        
        private Stack<IPoolable> _unactivatedObjects;
        private List<IPoolable> _activatedObjects;

        private GameObject _poolablePrefab;

        public GameObjectPool(GameObject poolablePrefab)
        {
            _poolablePrefab = poolablePrefab;

            _unactivatedObjects = new Stack<IPoolable>();
            _activatedObjects = new List<IPoolable>();

            #if UNITY_EDITOR
                
                UnityEngine.Assertions.Assert.IsTrue(poolablePrefab.TryGetComponent(typeof(IPoolable), out _));
                    
            #endif
        }

        public void GenerateObject(Vector3 position)
        {
            var generatedObject = _unactivatedObjects.Count == 0 
                ? InstantiateNew(position) 
                : ActivateUsed(position);
        }

        public void UnsubscribeEvents()
        {
            while (_unactivatedObjects.Count > 0)
            {
                _unactivatedObjects.Pop().OnObjectUsed -= DeactivateObject;
            }

            foreach (var activatedObject in _activatedObjects)
            {
                activatedObject.OnObjectUsed -= DeactivateObject;
            }
        }

        private IPoolable InstantiateNew(Vector3 position)
        {
            var instantiated = Object.Instantiate(_poolablePrefab, Vector3.zero, Quaternion.identity);
            var deactivated = instantiated.GetComponent<IPoolable>();
            deactivated.OnObjectUsed += DeactivateObject;
            return ActivateObject(position, deactivated);
        }

        private IPoolable ActivateUsed(Vector3 position)
        {
            var deactivated = _unactivatedObjects.Pop();
            return ActivateObject(position, deactivated);
        }

        private IPoolable ActivateObject(Vector3 position, IPoolable deactivated)
        {
            deactivated.Activate(position);
            _activatedObjects.Add(deactivated);
            
            return deactivated;
        }

        private void DeactivateObject(GameObject sender)
        {
            var poolable = sender.GetComponent<IPoolable>();
            _activatedObjects.Remove(poolable);
            poolable.Deactivate();
            _unactivatedObjects.Push(poolable);
            OnObjectDeactivated?.Invoke();
        }
    }
}
