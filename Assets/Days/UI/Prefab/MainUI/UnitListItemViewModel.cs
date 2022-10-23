using Days.Game.Object.Infra;
using Days.Game.Object.Infra.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Days.UI.Prefab.ViewModel
{
    public class UnitListItemViewModel : MonoBehaviour
    {
        public Text unitName;
        public Text info1;
        public Text info2;

        public void SetItem(UnitInfo unit)
        {
            unitName.text = unit.Name;
        }
    }
}
