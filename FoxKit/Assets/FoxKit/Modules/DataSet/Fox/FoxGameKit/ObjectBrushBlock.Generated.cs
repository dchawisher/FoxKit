//------------------------------------------------------------------------------ 
// <auto-generated> 
// This code was automatically generated.
// 
// Changes to this file may cause incorrect behavior and will be lost if 
// the code is regenerated. 
// </auto-generated> 
//------------------------------------------------------------------------------
namespace FoxKit.Modules.DataSet.Fox.FoxGameKit
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
    public partial class ObjectBrushBlock : Data
    {
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Path, 120, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private UnityEngine.Object filePath;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.FilePtr, 128, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private UnityEngine.Object obrbFile;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.UInt32, 184, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorOnly, null, null)]
        private System.UInt32 blockId;
        
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.String, 160, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorOnly, null, null)]
        private System.String objectBrushName = string.Empty;
        
        public override short ClassId => 128;
        
        public override ushort Version => 3;
        
        public override string Category => "";
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void Activate(lua_State lua);
    }
}
