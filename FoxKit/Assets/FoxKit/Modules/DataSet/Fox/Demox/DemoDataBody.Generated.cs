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
    public partial class DemoDataBody : TransformDataBody
    {
        public override short ClassId => 0;
        
        public override ushort Version => 0;
        
        public override string Category => "";
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void Start(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void Stop(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void StreamStart(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void StreamPause(lua_State lua);
        
        [ExposeMethodToLua(MethodStaticity.Instance)]
        partial void SetupPlayback(lua_State lua);
    }
}
