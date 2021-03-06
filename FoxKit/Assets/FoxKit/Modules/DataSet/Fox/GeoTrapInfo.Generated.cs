//------------------------------------------------------------------------------ 
// <auto-generated> 
// This code was automatically generated.
// 
// Changes to this file may cause incorrect behavior and will be lost if 
// the code is regenerated. 
// </auto-generated> 
//------------------------------------------------------------------------------
namespace FoxKit.Modules.DataSet.Fox
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
    public partial class GeoTrapInfo : Entity
    {
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.UInt8, 48, 1, Core.ContainerType.StringMap, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private Dictionary<string, System.Byte> moverTags = new Dictionary<string, System.Byte>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityHandle, 96, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private FoxKit.Modules.DataSet.Fox.FoxCore.Entity moverHandle;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Vector3, 112, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private UnityEngine.Vector3 moverPosition;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Vector3, 128, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private UnityEngine.Vector3 moverRotation;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.String, 144, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private System.String trapName = string.Empty;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Vector3, 160, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private UnityEngine.Vector3 trapPosition;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityHandle, 176, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private FoxKit.Modules.DataSet.Fox.FoxCore.Entity trapBodyHandle;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityHandle, 184, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private FoxKit.Modules.DataSet.Fox.FoxCore.Entity conditionHandle;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityHandle, 192, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private FoxKit.Modules.DataSet.Fox.FoxCore.Entity conditionBodyHandle;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.String, 200, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private System.String trapFlagString = string.Empty;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.UInt32, 208, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private System.UInt32 trapFlag;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.UInt16, 104, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private System.UInt16 moverGameObjectId;
        
        public override short ClassId => 0;
        
        public override ushort Version => 0;
        
        public override string Category => "";
    }
}
