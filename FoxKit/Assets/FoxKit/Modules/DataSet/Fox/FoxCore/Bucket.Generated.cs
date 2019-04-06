//------------------------------------------------------------------------------ 
// <auto-generated> 
// This code was automatically generated.
// 
// Changes to this file may cause incorrect behavior and will be lost if 
// the code is regenerated. 
// </auto-generated> 
//------------------------------------------------------------------------------
namespace FoxKit.Modules.DataSet.Fox.FoxCore
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
    public partial class Bucket : Entity
    {
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityHandle, 56, 1, Core.ContainerType.StaticArray, PropertyExport.Never, PropertyExport.Never, null, null)]
        private FoxKit.Modules.DataSet.Fox.FoxCore.Entity collector;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.String, 64, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private System.String name = string.Empty;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.String, 72, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.Never, null, null)]
        private System.String sceneName = string.Empty;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityPtr, 88, 1, Core.ContainerType.List, PropertyExport.Never, PropertyExport.Never, typeof(FoxCore.Actor), null)]
        private List<FoxCore.Actor> actors = new List<FoxCore.Actor>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.FilePtr, 120, 1, Core.ContainerType.StringMap, PropertyExport.Never, PropertyExport.Never, null, null)]
        private Dictionary<string, UnityEngine.Object> dataSetFiles = new Dictionary<string, UnityEngine.Object>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityPtr, 168, 1, Core.ContainerType.StringMap, PropertyExport.Never, PropertyExport.Never, typeof(FoxCore.DataBodySet), null)]
        private Dictionary<string, FoxCore.DataBodySet> dataBodySets = new Dictionary<string, FoxCore.DataBodySet>();
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityPtr, 224, 1, Core.ContainerType.StaticArray, PropertyExport.Never, PropertyExport.Never, typeof(FoxCore.DataSet), null)]
        private FoxCore.DataSet editableDataSet;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Path, 232, 1, Core.ContainerType.StaticArray, PropertyExport.Never, PropertyExport.Never, null, null)]
        private UnityEngine.Object editableDataSetPath;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityPtr, 240, 1, Core.ContainerType.StaticArray, PropertyExport.Never, PropertyExport.Never, typeof(FoxCore.DataBodySet), null)]
        private FoxCore.DataBodySet editableDataBodySet;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Bool, 248, 1, Core.ContainerType.StaticArray, PropertyExport.Never, PropertyExport.Never, null, null)]
        private System.Boolean editableDataSetChanged;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Bool, 0, 1, Core.ContainerType.StaticArray, PropertyExport.EditorOnly, PropertyExport.EditorOnly, null, null)]
        private System.Boolean isEditableLocked;
        
        public override short ClassId => 0;
        
        public override ushort Version => 0;
        
        public override string Category => "";
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void GetScene(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void AddActor(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void RemoveActor(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void LoadDataSetFile(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void UnloadDataSetFile(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void LoadProjectFile(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void GetEditableDataSet(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void GetEditableDataSetPath(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void GetEditableDataBodySet(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void SaveEditableDataSet(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void LoadEditableDataSet(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void RecreateDataBody(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void RemoveAll(lua_State lua);
    }
}
