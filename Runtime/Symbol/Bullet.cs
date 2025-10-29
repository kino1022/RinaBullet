using System;
using System.Collections.Generic;
using GeneralModule.Symbol;
using RinaBullet.Context;
using RinaBullet.Context.Container;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using VContainer;

namespace RinaBullet.Symbol {
    public class Bullet : ASerializedSymbol {
        
        [Title("参照")]
        
        [OdinSerialize]
        [ReadOnly]
        protected IContextContainer m_container;

        [OdinSerialize] 
        [ReadOnly] 
        private IReadOnlyList<IBulletContext> m_contexts;

        protected IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }

        protected virtual void Start() {
            
            m_container = m_resolver.Resolve<IContextContainer>();
            
            m_contexts = new List<IBulletContext>(m_container.Contexts);

            if (m_contexts is null || m_contexts.Count is 0) {
                foreach (var context in m_contexts) {
                    context.Apply(gameObject);
                }
            }
        }
    }
}