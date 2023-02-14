using System.Collections.Generic;
using System.ComponentModel;
using Days.Util.Infra;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Grid = Days.Util.Infra.Grid;

namespace Days.Resource.Model
{
    public enum SelectType : byte
    {
        [Description("대상을 선택하지 않는")]
        None,
        [Description("지정된 범위 내의 모든 적을 대상으로")]
        TargetWithinGrid,
        [Description("지정된 Area 범위 내 선택한 적을 대상으로")]
        Target
    }
    
    public class SkillModel
    {
        public string Name { get; set; }
        
        public byte Index { get; set; }
        
        public byte ClassIndex { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public List<SkillInfo> SkillTypeList { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SelectType SelectType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TargetType TargetType { get; set; }
        public byte TargetCount { get; set; }
        
        public byte Area { get; set; }
        
        public List<Grid> Coordinate { get; set; }

        public string Description;
        
        public SkillModel()
        {
            Coordinate = new List<Grid>();
            SkillTypeList = new List<SkillInfo>();
        }
    }
}