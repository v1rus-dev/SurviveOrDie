using UnityEngine;

namespace Player
{
    public class HelpItem : MonoBehaviour, IHelpItem
    {
        [field: SerializeField]
        public string HelpText { get; set; }
    }
}