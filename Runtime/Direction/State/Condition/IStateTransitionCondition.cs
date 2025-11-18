using UnityEngine;
using VContainer;

namespace RinaBullet.Direction.State.Condition {
    public interface IStateTransitionCondition {
        
        void Initialize (GameObject bullet, IObjectResolver resolver);
        
        bool Condition { get; }
        
    }
    
    public abstract class AStateTransitionCondition : IStateTransitionCondition {
        
        protected GameObject _bullet;
        
        protected IObjectResolver _resolver;
        
        public bool Condition => EvaluateCondition();
        
        public void Initialize(GameObject bullet, IObjectResolver resolver) {
            _bullet = bullet;
            _resolver = resolver;
            OnInitialize();
        }
        
        protected virtual void OnInitialize() { }
        
        protected abstract bool EvaluateCondition();
        
    }
}