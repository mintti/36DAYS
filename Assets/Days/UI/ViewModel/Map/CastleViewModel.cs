using System;
using Days.UI.Infra;
using UnityEngine;

namespace Days.UI.ViewModel.Map
{
    public class CastleViewModel : MonoBehaviour
    {
        public void Start()
        {
            this.tag = Tag.MapCastle.ToString();
        }
    }
}
