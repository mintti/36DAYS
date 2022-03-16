using System.Collections.Generic;
using Days.Resource.Model;
using Days.Game.Object.Infra;
using Object = Days.Game.Object.Infra;

namespace Days.Resource
{
    public class Resource
    {
        public static List<ObjectInfo> MonsterResources { get; set; }
        public static List<DungeonModel> DungeonResources { get; set; }
    }
}