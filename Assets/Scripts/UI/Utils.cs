using UnityEngine.UIElements;

namespace UI
{
    public static class Utils
    {
        public static VisualElement Create(VisualElement addTo = null, params string[] classes)
        {
            return Create<VisualElement>(addTo, classes);
        }

        public static T Create<T>(VisualElement addTo = null, params string[] classes)
            where T : VisualElement, new()
        {
            var ele = new T();
            addTo?.Add(ele);
            foreach (var className in classes)
            {
                ele.AddToClassList(className);
            }
            return ele;
        }
    }
}
