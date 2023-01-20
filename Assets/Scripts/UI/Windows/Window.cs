using UnityEngine;

namespace UI.Windows
{
    public class Window : MonoBehaviour
    {
        public WindowType WindowType;
        public void Switch(bool flag)
        {
            gameObject.SetActive(flag);
        }
    }
}