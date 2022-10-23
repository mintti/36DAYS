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
        None,
        [Description("범위 내의 적을 대상으로")]
        TargetWithinGrid,
        [Description("범위 내의 적을 대상으로")]
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

        public SkillModel()
        {
            Coordinate = new List<Grid>();
            SkillTypeList = new List<SkillInfo>();
        }
    }
}