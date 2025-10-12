using VContainer;
using VContainer.Unity;

namespace RinaBullet.Speed {
    public interface IBulletSpeedHolder : IStartable {
        
        float Speed { get; }
        
        void Initialize(IObjectResolver resolver);
        
    }
}