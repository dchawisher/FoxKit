//------------------------------------------------------------------------------ 
// <auto-generated> 
// This code was automatically generated.
// 
// Changes to this file may cause incorrect behavior and will be lost if 
// the code is regenerated. 
// </auto-generated> 
//------------------------------------------------------------------------------
namespace FoxKit.Modules.DataSet.Fox.Demox
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
    public partial class ProceduralDemoData : TransformData
    {
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.FilePtr, 304, 1, Core.ContainerType.StringMap, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private Dictionary<string, UnityEngine.Object> evfFiles = new Dictionary<string, UnityEngine.Object>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.FilePtr, 352, 1, Core.ContainerType.StringMap, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private Dictionary<string, UnityEngine.Object> eventFiles = new Dictionary<string, UnityEngine.Object>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Int32, 400, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private System.Int32 priority;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.String, 408, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private System.String demoId = string.Empty;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.String, 416, 1, Core.ContainerType.DynamicArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private List<System.String> stringParams = new List<System.String>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityLink, 432, 1, Core.ContainerType.StringMap, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private Dictionary<string, FoxKit.Modules.DataSet.FoxCore.EntityLink> entityParams = new Dictionary<string, FoxKit.Modules.DataSet.FoxCore.EntityLink>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.FilePtr, 480, 1, Core.ContainerType.StringMap, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private Dictionary<string, UnityEngine.Object> fileParams = new Dictionary<string, UnityEngine.Object>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Int32, 528, 1, Core.ContainerType.StringMap, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private Dictionary<string, System.Int32> objectNum = new Dictionary<string, System.Int32>();
        
        public override short ClassId => 0;
        
        public override ushort Version => 0;
        
        public override string Category => "";
    }
}
