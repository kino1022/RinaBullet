using RinaBullet.Context.Container;
using RinaBullet.Shooter.Interface;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using VContainer;
using VContainer.Unity;

namespace RinaBullet.Installer {
    public class CharacterInstaller : SerializedMonoBehaviour, IInstaller {

        [OdinSerialize]
        [LabelText("弾丸射出コンポーネント")]
        private IBulletShooter m_shooter;
        
        [OdinSerialize]
        [LabelText("コンテキスト管理コンポーネント")]
        private IContextContainer m_contextContainer;

        public void Install(IContainerBuilder builder) {

            builder
                .RegisterComponent(m_shooter);
            
            builder
                .RegisterComponent(m_contextContainer);
        }
    }
}