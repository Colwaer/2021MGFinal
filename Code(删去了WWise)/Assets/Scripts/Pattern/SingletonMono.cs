using UnityEngine;
namespace Pattern
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>, new()
    {
        protected static T m_instance = default;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    //If Scene Exit Find It
                    m_instance = FindObjectOfType<T>();

                    //Or Create New One
                    if (m_instance == null)
                        m_instance = new T();

                    m_instance.OnInit();
                }
                return m_instance;
            }
        }

        protected virtual void OnInit()
        {

        }

        virtual protected void Awake()
        {
            //If Has Exit,Destory
            if (m_instance != null && m_instance != this)
            {
                Destroy(this);
            }
        }
    }
}
