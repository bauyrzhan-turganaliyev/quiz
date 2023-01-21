using UnityEngine;

namespace UI.Windows
{
    public class Window : MonoBehaviour, IWindow
    {
        public WindowType WindowType;
        public void Switch(bool flag)
        {
            gameObject.SetActive(flag);
        }
    }
}