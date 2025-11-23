using UnityEngine;
using VContainer;

namespace RinaBullet.Move.Modifiier {
    
    /// <summary>
    /// 最終的な運動量を補正するためのクラスに対して約束するインターフェース
    /// </summary>
    public interface IMoveModifier {
        
        Vector3 Movement { get; }
        
        bool IsEnabled { get; set; }
        
        void Initialize (GameObject bullet, IObjectResolver resolver);
        
    }
    
    public abstract class AMoveModifier {
        
        public Vector3 Movement => IsEnabled ? CalculateMovement() : Vector3.zero;
        
        public bool IsEnabled { get; set; } = true;
        
        
        protected GameObject _bullet;

        protected IObjectResolver _resolver;
        
        public virtual void Initialize(GameObject bullet, IObjectResolver resolver) {
            
            _resolver = resolver;
            
            _bullet = bullet;
            
            OnInitialize();
            
        }
        
        protected virtual void OnInitialize() { }

        protected abstract Vector3 CalculateMovement();
    }
}