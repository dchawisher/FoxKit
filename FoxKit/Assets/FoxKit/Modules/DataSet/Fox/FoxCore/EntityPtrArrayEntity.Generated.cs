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
    public partial class EntityPtrArrayEntity : Entity
    {
        [OdinSerializeAttribute, NonSerializedAttribute, PropertyInfoAttribute(Core.PropertyInfoType.EntityPtr, 48, 1, Core.ContainerType.DynamicArray, PropertyExport.EditorOnly, PropertyExport.EditorOnly, typeof(FoxCore.Entity), null)]
        private List<FoxCore.Entity> array = new List<FoxCore.Entity>();
        
        public override short ClassId => 40;
        
        public override ushort Version => 0;
        
        public override string Category => "";
    }
}
