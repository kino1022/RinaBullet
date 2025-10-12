using System;
using GeneralModule.Symbol;
using RinaBullet.Context.Container;
using VContainer;

namespace RinaBullet.Symbol {
    public class Bullet : ASerializedSymbol {

        protected IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }

        protected virtual void Start() {
            var container = m_resolver.Resolve<IContextContainer>() ?? throw new NullReferenceException();
            foreach (var context in container.Contexts) {
                context?.Apply(gameObject);
            }
        }
    }
}