//------------------------------------------------------------------------------ 
// <auto-generated> 
// This code was automatically generated.
// 
// Changes to this file may cause incorrect behavior and will be lost if 
// the code is regenerated. 
// </auto-generated> 
//------------------------------------------------------------------------------
namespace FoxKit.Modules.DataSet.Fox.Sdx
{
    using System;
    using System.Collections.Generic;
    using FoxKit.Modules.DataSet.Fox.FoxCore;
    using FoxKit.Modules.Lua;
    using FoxLib;
    using static KopiLua.Lua;
    using OdinSerializer;
    using UnityEngine;
    using DataSetFile2 = DataSetFile2;
    using TppGameKit = FoxKit.Modules.DataSet.Fox.TppGameKit;
    
    [SerializableAttribute, ExposeClassToLuaAttribute]
    public partial class SoundSource : TransformData
    {
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.String, 304, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private System.String eventName = string.Empty;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityLink, 312, 1, Core.ContainerType.DynamicArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private List<FoxKit.Modules.DataSet.FoxCore.EntityLink> shapes = new List<FoxKit.Modules.DataSet.FoxCore.EntityLink>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Float, 328, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private System.Single lodRange;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Float, 332, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private System.Single playRange;
        
        public override short ClassId => 288;
        
        public override ushort Version => 2;
        
        public override string Category => "Sound";
    }
}
