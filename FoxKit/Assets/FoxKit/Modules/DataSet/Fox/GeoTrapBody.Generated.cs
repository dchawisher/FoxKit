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
    public partial class GeoTrapBody : TransformDataBody
    {
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.Bool, 152, 1, Core.ContainerType.StaticArray, PropertyExport.EditorAndGame, PropertyExport.EditorAndGame, null, null)]
        private System.Boolean enable;
        
        public override short ClassId => 0;
        
        public override ushort Version => 0;
        
        public override string Category => "Trap";
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void SetValid(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void SetInvalid(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Static)]
        static partial void GetByName(lua_State lua);
    }
}
