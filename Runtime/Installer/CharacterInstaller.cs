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
            if (m_shooter == null) {
                m_shooter = gameObject.transform.root.GetComponentInChildren<IBulletShooter>();
            }

            builder.RegisterComponent(m_shooter);

            if (m_contextContainer == null) {
                m_contextContainer = gameObject.transform.root.GetComponentInChildren<IContextContainer>();
            }

            builder
                .RegisterComponent(m_contextContainer);
        }
    }
}