using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RinaBullet.Direction {
    public interface IBulletDirectionHolder : IStartable {
        
        Vector3 Direction { get; }
        
        void Initialize(IObjectResolver resolver, GameObject bullet);
        
    }
}