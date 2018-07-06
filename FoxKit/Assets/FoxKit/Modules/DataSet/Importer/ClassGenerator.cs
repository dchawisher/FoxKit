﻿namespace FoxKit.Modules.DataSet.Importer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using FoxKit.Modules.DataSet.FoxCore;
    using FoxKit.Modules.DataSet.PartsBuilder;

    using FoxLib;

    using Microsoft.FSharp.Core;

    using UnityEditor;

    using UnityEngine;
    using UnityEngine.Assertions;

    public static class ClassGenerator
    {
        private static readonly string OutputDirectory = Application.dataPath + @"/FoxKit/Modules/DataSet/Generated/";

        public static void GenerateClassFromEntity(Core.Entity entity)
        {
            var sourceCode = GenerateClassSourceCode(entity);
            File.WriteAllText(OutputDirectory + $"{entity.ClassName}.cs", sourceCode);
            
            // TODO Don't do this each time
            AssetDatabase.Refresh();
        }

        private static string GenerateClassSourceCode(Core.Entity entity)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(MakeNamespaceStatement());

            // Open namespace block.
            stringBuilder.AppendLine("{");
            
            AddUsingStatements(stringBuilder);

            stringBuilder.AppendLine("    /// <inheritdoc />");
            stringBuilder.AppendLine("    [Serializable]");

            var parentClass =
                DetermineBaseClass((from property in entity.StaticProperties
                                    select property.Name).ToList());
            stringBuilder.AppendLine(MakeClassStatement(entity.ClassName, parentClass.Name));

            // Open class block.
            stringBuilder.AppendLine("    {");

            var staticProperties = GetPrunedStaticPropertyFields(entity.StaticProperties, parentClass);
            AddStaticPropertyFields(stringBuilder, staticProperties.ToArray(), entity.ClassName);

            AddProperties(entity.ClassId, entity.Version, stringBuilder);
            stringBuilder.AppendLine(string.Empty);

            // TODO MakeWritableStaticProperties
            // TODO ReadProperty
            AddReadPropertyFunction(stringBuilder, staticProperties);
            stringBuilder.AppendLine(string.Empty);

            // TODO OnAssetsImported (if there are files referenced)

            // Close class block.
            stringBuilder.AppendLine("    }");

            // Close namespace block.
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }

        private static void AddReadPropertyFunction(StringBuilder stringBuilder, IEnumerable<Core.PropertyInfo> staticProperties)
        {
            stringBuilder.AppendLine("        /// <inheritdoc />");
            stringBuilder.AppendLine(
                "        protected override void ReadProperty(Core.PropertyInfo propertyData, Importer.EntityFactory.EntityInitializeFunctions initFunctions)");

            // Open function block.
            stringBuilder.AppendLine("        {");

            stringBuilder.AppendLine("            base.ReadProperty(propertyData, initFunctions);");
            stringBuilder.AppendLine(string.Empty);

            stringBuilder.AppendLine("            switch (propertyData.Name)");

            // Open switch statement block.
            stringBuilder.AppendLine("            {");

            foreach (var property in staticProperties)
            {
                string propertyName = property.Name;
                if (property.Type == Core.PropertyInfoType.FilePtr || property.Type == Core.PropertyInfoType.Path)
                {
                    propertyName += "Path";
                }

                AddReadPropertyBlock(stringBuilder, property, propertyName);
            }

            // Close switch statement block.
            stringBuilder.AppendLine("            }");

            // Close function block.
            stringBuilder.AppendLine("        }");
        }

        private static void AddReadPropertyBlock(StringBuilder stringBuilder, Core.PropertyInfo property, string backingPropertyName)
        {
            stringBuilder.AppendLine($"                case \"{property.Name}\":");

            var rawTypeString = GetRawTypeString(property.Type);
            var convertedTypeString = GetConvertedTypeString(property.Type);

            var conversionFunctionString = GetConversionFunctionString(property.Type);

            if (property.ContainerType == Core.ContainerType.StringMap)
            {
                stringBuilder.AppendLine($"                    var dictionary = DataSetUtils.GetStringMap<{rawTypeString}>(propertyData);");
                stringBuilder.AppendLine($"                    var finalValues = new OrderedDictionary_string_{convertedTypeString}();");
                stringBuilder.AppendLine("                    foreach(var entry in dictionary)");
                stringBuilder.AppendLine("                    {");
                stringBuilder.AppendLine($"                        this.finalValues.Add(entry.Key, {conversionFunctionString}(entry.Value));");
                stringBuilder.AppendLine("                    }");
                stringBuilder.AppendLine("                    ");
                stringBuilder.AppendLine($"                    this.{backingPropertyName} = finalValues;");
            }
            else if (property.ContainerType == Core.ContainerType.StaticArray && property.Container.ArraySize == 1)
            {
                stringBuilder.AppendLine($"                    this.{backingPropertyName} = {conversionFunctionString}(DataSetUtils.GetStaticArrayPropertyValue<{rawTypeString}>(propertyData));");
            }
            else
            {
                var extractValuesFunctionString = "DataSetUtils.GetStaticArrayValues";
                if (property.ContainerType == Core.ContainerType.DynamicArray)
                {
                    extractValuesFunctionString = "DataSetUtils.GetDynamicArrayValues";
                }
                else if (property.ContainerType == Core.ContainerType.List)
                {
                    extractValuesFunctionString = "DataSetUtils.GetListValues";
                }

                stringBuilder.AppendLine($"                    this.{backingPropertyName} = (from rawValue in {extractValuesFunctionString}<{rawTypeString}>(propertyData) select {conversionFunctionString}(rawValue)).ToList();");
            }

            stringBuilder.AppendLine("                    break;");
        }

        private static string GetConversionFunctionString(Core.PropertyInfoType propertyType)
        {
            // TODO: EntityLink
            var conversionFunctionString = string.Empty;
            switch (propertyType)
            {
                case Core.PropertyInfoType.Color:
                    conversionFunctionString = "FoxUtils.FoxColorRGBAToUnityColor";
                    break;
                case Core.PropertyInfoType.EntityHandle:
                    conversionFunctionString = "initFunctions.GetEntityFromAddress";
                    break;
                case Core.PropertyInfoType.EntityPtr:
                    conversionFunctionString = "initFunctions.GetEntityFromAddress";
                    break;
                case Core.PropertyInfoType.FilePtr:
                    // TODO: Should this be in FoxUtils?
                    conversionFunctionString = "DataSetUtils.ExtractFilePath";
                    break;
                case Core.PropertyInfoType.Matrix3:
                    conversionFunctionString = "FoxUtils.FoxToUnity";
                    break;
                case Core.PropertyInfoType.Matrix4:
                    conversionFunctionString = "FoxUtils.FoxToUnity";
                    break;
                case Core.PropertyInfoType.Path:
                    conversionFunctionString = "DataSetUtils.ExtractFilePath";
                    break;
                case Core.PropertyInfoType.Quat:
                    conversionFunctionString = "FoxUtils.FoxToUnity";
                    break;
                case Core.PropertyInfoType.Vector3:
                    conversionFunctionString = "FoxUtils.FoxToUnity";
                    break;
                case Core.PropertyInfoType.Vector4:
                    conversionFunctionString = "FoxUtils.FoxToUnity";
                    break;
            }

            return conversionFunctionString;
        }

        private static IEnumerable<Core.PropertyInfo> GetPrunedStaticPropertyFields(
            IEnumerable<Core.PropertyInfo> staticProperties,
            Type parentClass)
        {
            var propertiesToPrune = new List<string>();
            if (parentClass == typeof(DataElement))
            {
                propertiesToPrune.AddRange(DataElementPropertyNames);
            }
            else if (parentClass == typeof(Data))
            {
                propertiesToPrune.AddRange(DataPropertyNames);
            }
            else if (parentClass == typeof(TransformData))
            {
                propertiesToPrune.AddRange(DataPropertyNames);
                propertiesToPrune.AddRange(TransformDataPropertyNames);
            }
            else if (parentClass == typeof(PartDescription))
            {
                propertiesToPrune.AddRange(DataPropertyNames);
                propertiesToPrune.AddRange(PartDescriptionPropertyNames);
            }

            return from property in staticProperties where !propertiesToPrune.Contains(property.Name) select property;
        }

        private static void AddProperties(short classId, ushort version, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        /// <inheritdoc />");
            stringBuilder.AppendLine(MakeClassIdDeclaration(classId));
            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine("        /// <inheritdoc />");
            stringBuilder.AppendLine(MakeVersionDeclaration(version));
        }

        private static void AddStaticPropertyFields(StringBuilder stringBuilder, Core.PropertyInfo[] properties, string className)
        {
            foreach (var property in properties)
            {
                AddStaticPropertyField(stringBuilder, property, className);
                stringBuilder.AppendLine(string.Empty);
            }
        }

        private static void AddStaticPropertyField(StringBuilder stringBuilder, Core.PropertyInfo property, string className)
        {
            // TODO Initialize string to string.Empty
            stringBuilder.AppendLine($"        [SerializeField, Modules.DataSet.Property(\"{className}\")]");
            stringBuilder.AppendLine($"        private {GetPropertyFieldTypeString(property.ContainerType, property.Type, property.Container)} {property.Name};");

            if (property.Type != Core.PropertyInfoType.FilePtr && property.Type != Core.PropertyInfoType.Path)
            {
                return;
            }

            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine("        [SerializeField, HideInInspector]");
            stringBuilder.AppendLine($"        private string {property.Name}Path;");
        }
        
        private static string GetPropertyFieldTypeString(
            Core.ContainerType containerType,
            Core.PropertyInfoType propertyType,
            Core.IContainer container)
        {
            var innerTypeString = GetConvertedTypeString(propertyType);
            
            if (containerType == Core.ContainerType.StaticArray && container.ArraySize == 1)
            {
                return innerTypeString;
            }

            if (containerType != Core.ContainerType.StringMap)
            {
                return $"List<{innerTypeString}>";
            }
            
            return $"OrderedDictionary_string_{GetConvertedTypeString(propertyType)}";
        }

        private static string GetConvertedTypeString(Core.PropertyInfoType type)
        {
            switch (type)
            {
                case Core.PropertyInfoType.Int8:
                    return "sbyte";
                case Core.PropertyInfoType.UInt8:
                    return "byte";
                case Core.PropertyInfoType.Int16:
                    return "short";
                case Core.PropertyInfoType.UInt16:
                    return "ushort";
                case Core.PropertyInfoType.Int32:
                    return "int";
                case Core.PropertyInfoType.UInt32:
                    return "uint";
                case Core.PropertyInfoType.Int64:
                    return "long";
                case Core.PropertyInfoType.UInt64:
                    return "ulong";
                case Core.PropertyInfoType.Float:
                    return "float";
                case Core.PropertyInfoType.Double:
                    return "double";
                case Core.PropertyInfoType.Bool:
                    return "bool";
                case Core.PropertyInfoType.String:
                    return "string";
                case Core.PropertyInfoType.Path:
                    return "string";
                case Core.PropertyInfoType.EntityPtr:
                    return "Entity";    // TODO
                case Core.PropertyInfoType.Vector3:
                    return "UnityEngine.Vector3";
                case Core.PropertyInfoType.Vector4:
                    return "UnityEngine.Vector4";
                case Core.PropertyInfoType.Quat:
                    return "UnityEngine.Quaternion";
                case Core.PropertyInfoType.Matrix3:
                    return "UnityEngine.Matrix3x3";
                case Core.PropertyInfoType.Matrix4:
                    return "UnityEngine.Matrix4x4";
                case Core.PropertyInfoType.Color:
                    return "UnityEngine.Color";
                case Core.PropertyInfoType.FilePtr:
                    return "UnityEngine.Object";
                case Core.PropertyInfoType.EntityHandle:
                    return "Entity";    // TODO
                case Core.PropertyInfoType.EntityLink:
                    return "FoxCore.EntityLink";
                case Core.PropertyInfoType.PropertyInfo:
                    Assert.IsTrue(false, "Unsupported property type: PropertyInfo.");
                    break;
                case Core.PropertyInfoType.WideVector3:
                    Assert.IsTrue(false, "Unsupported property type: WideVector3.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return "ERROR";
        }

        private static string GetRawTypeString(Core.PropertyInfoType type)
        {
            switch (type)
            {
                case Core.PropertyInfoType.Int8:
                    return "sbyte";
                case Core.PropertyInfoType.UInt8:
                    return "byte";
                case Core.PropertyInfoType.Int16:
                    return "short";
                case Core.PropertyInfoType.UInt16:
                    return "ushort";
                case Core.PropertyInfoType.Int32:
                    return "int";
                case Core.PropertyInfoType.UInt32:
                    return "uint";
                case Core.PropertyInfoType.Int64:
                    return "long";
                case Core.PropertyInfoType.UInt64:
                    return "ulong";
                case Core.PropertyInfoType.Float:
                    return "float";
                case Core.PropertyInfoType.Double:
                    return "double";
                case Core.PropertyInfoType.Bool:
                    return "bool";
                case Core.PropertyInfoType.String:
                    return "string";
                case Core.PropertyInfoType.Path:
                    return "string";
                case Core.PropertyInfoType.EntityPtr:
                    return "Entity";    // TODO
                case Core.PropertyInfoType.Vector3:
                    return "Core.Vector3";
                case Core.PropertyInfoType.Vector4:
                    return "Core.Vector4";
                case Core.PropertyInfoType.Quat:
                    return "Core.Quaternion";
                case Core.PropertyInfoType.Matrix3:
                    return "Core.Matrix3x3";
                case Core.PropertyInfoType.Matrix4:
                    return "Core.Matrix4x4";
                case Core.PropertyInfoType.Color:
                    return "Core.Color";
                case Core.PropertyInfoType.FilePtr:
                    return "Core.Object";
                case Core.PropertyInfoType.EntityHandle:
                    return "Entity";    // TODO
                case Core.PropertyInfoType.EntityLink:
                    return "Core.EntityLink";
                case Core.PropertyInfoType.PropertyInfo:
                    Assert.IsTrue(false, "Unsupported property type: PropertyInfo.");
                    break;
                case Core.PropertyInfoType.WideVector3:
                    Assert.IsTrue(false, "Unsupported property type: WideVector3.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return "ERROR";
        }

        private static void AddUsingStatements(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine(MakeUsingStatement("System"));
            stringBuilder.AppendLine(MakeUsingStatement("System.Linq"));
            stringBuilder.AppendLine(MakeUsingStatement("System.Collections.Generic"));
            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine(MakeUsingStatement("FoxKit.Modules.DataSet.Exporter"));
            stringBuilder.AppendLine(MakeUsingStatement("FoxKit.Modules.DataSet.FoxCore"));
            stringBuilder.AppendLine(MakeUsingStatement("FoxKit.Utils"));
            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine(MakeUsingStatement("FoxLib"));
            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine(MakeUsingStatement("NUnit.Framework"));
            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine(MakeUsingStatement("UnityEditor"));
            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine(MakeUsingStatement("UnityEngine"));
            stringBuilder.AppendLine(string.Empty);
        }

        private static string MakeNamespaceStatement()
        {
            // TODO: Generate correct namespace
            return "namespace FoxKit.Modules.DataSet";
        }

        private static string MakeUsingStatement(string @namespace)
        {
            return $"    using {@namespace};";
        }

        private static string MakeClassStatement(string className, string parentClassName)
        {
            return $"    public class {className} : {parentClassName}";
        }

        private static string MakeClassIdDeclaration(short classId)
        {
            return $"        public override short ClassId => {classId};";
        }

        private static string MakeVersionDeclaration(ushort version)
        {
            return $"        public override ushort Version => {version};";
        }

        private static readonly List<string> DataElementPropertyNames = new List<string>{"owner"};
        private static readonly List<string> DataPropertyNames = new List<string> { "name", "dataSet" };
        private static readonly List<string> TransformDataPropertyNames = new List<string> { "parent", "transform", "shearTransform", "pivotTransform", "children", "flags" };
        private static readonly List<string> PartDescriptionPropertyNames = new List<string> { "depends", "partName", "buildType" };

        private static Type DetermineBaseClass(ICollection<string> staticPropertyNames)
        {
            // DataElement
            if (DataElementPropertyNames.All(staticPropertyNames.Contains))
            {
                return typeof(DataElement);
            }

            // Data
            if (!DataPropertyNames.All(staticPropertyNames.Contains))
            {
                return typeof(Entity);
            }

            if (TransformDataPropertyNames.All(staticPropertyNames.Contains))
            {
                return typeof(TransformData);
            }

            return PartDescriptionPropertyNames.All(staticPropertyNames.Contains) ? typeof(PartDescription) : typeof(Data);
        }
    }
}