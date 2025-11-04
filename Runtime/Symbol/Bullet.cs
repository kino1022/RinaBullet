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
        private IReadOnlyList<IBulletContext> m_contexts;

        protected IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }

        public void InitializeContext(IReadOnlyList<IBulletContext> contexts) {
            m_contexts = new List<IBulletContext>(contexts);
            
            if (m_contexts is not null && m_contexts.Count is not 0) {
                foreach (var context in m_contexts) {
                    context.Apply(gameObject);
                }
            }
        }

    }
}