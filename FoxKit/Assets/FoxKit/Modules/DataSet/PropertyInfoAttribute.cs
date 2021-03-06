﻿namespace FoxKit.Modules.DataSet
{
    using System;

    using FoxKit.Modules.DataSet.Fox.FoxCore;
    using FoxKit.Modules.DataSet.FoxCore;

    using FoxLib;

    public enum PropertyStorage
    {
        Instance,
        Class
    }

    [Flags]
    public enum PropertyExport
    {
        Never,
        EditorOnly,
        EditorAndGame
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class PropertyInfoAttribute : Attribute
    {
        public Core.PropertyInfoType Type { get; }
        public uint Offset { get; }
        public uint ArraySize { get; }
        public Core.ContainerType Container { get; }
        public PropertyStorage Storage { get; }
        public Type PtrType { get; }
        public Type Enum { get; }
        public PropertyExport Readable { get; }
        public PropertyExport Writable { get; }

        public PropertyInfoAttribute(
            Core.PropertyInfoType type,
            uint offset,
            uint arraySize = 1,
            Core.ContainerType container = Core.ContainerType.StaticArray,
            PropertyExport readable = PropertyExport.EditorAndGame,
            PropertyExport writable = PropertyExport.EditorAndGame,
            Type ptrType = null,
            Type enumType = null,
            PropertyStorage storage = PropertyStorage.Instance)
        {
            this.Type = type;
            this.Offset = offset;
            this.ArraySize = arraySize;
            this.Container = container;
            this.Storage = storage;
            this.PtrType = ptrType ?? typeof(Entity);
            this.Enum = enumType;
            this.Readable = readable;
            this.Writable = writable;
        }

        /// <summary>
        /// Some properties are not written to fox2 files and seem to be automatically generated from other properties.
        /// </summary>
        public bool IsAutoProperty => this.Offset == 0;
    }
}