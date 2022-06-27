using Days.Game.Object.Infra;
using UnityEngine;
using UnityEngine.UI;

namespace Days.UI.Prefab.ViewModel
{
    public class DungeonAdvanceUnitItemViewModel : MonoBehaviour
    {
        private ObjectInfo _unitInfo;
        public Text unitName;
        public Text info1;
        public Text info2;

        public SelectorViewModel selector; 
        public void SetItem(ObjectInfo unit)
        {
            _unitInfo = unit;
            unitName.text = unit.Name;
        }

        public byte GetUnitIndex()
        {
            return _unitInfo.Index;
        }
        
    }
}
