﻿using FoxKit.Modules.DataSet.FoxCore;
using System;
using FoxTool.Fox;
using FoxKit.Utils;
using FoxTool.Fox.Types.Values;
using System.Collections.Generic;

namespace FoxKit.Modules.DataSet.TppGameCore
{
    [Serializable]
    public class TppVehicle2AttachmentData : Data
    {
        public byte VehicleTypeCode;
        public byte AttachmentImplTypeIndex;
        public UnityEngine.Object AttachmentFile;
        public byte AttachmentInstanceCount;
        public string BodyCnpName;
        public string AttachmentBoneName;
        public List<TppVehicle2WeaponParameter> WeaponParams;

        public string AttachmentFilePath;

        protected override void ReadProperty(FoxProperty propertyData, Importer.EntityFactory.EntityInitializeFunctions initFunctions)
        {
            base.ReadProperty(propertyData, initFunctions);

            if (propertyData.Name == "vehicleTypeCode")
            {
                VehicleTypeCode = DataSetUtils.GetStaticArrayPropertyValue<FoxUInt8>(propertyData).Value;
            }
            else if (propertyData.Name == "attachmentImplTypeIndex")
            {
                AttachmentImplTypeIndex = DataSetUtils.GetStaticArrayPropertyValue<FoxUInt8>(propertyData).Value;
            }
            else if (propertyData.Name == "attachmentFile")
            {
                AttachmentFilePath = DataSetUtils.ExtractFilePath(DataSetUtils.GetStaticArrayPropertyValue<FoxFilePtr>(propertyData));
            }
            else if (propertyData.Name == "attachmentInstanceCount")
            {
                AttachmentInstanceCount = DataSetUtils.GetStaticArrayPropertyValue<FoxUInt8>(propertyData).Value;
            }
            else if (propertyData.Name == "bodyCnpName")
            {
                BodyCnpName = DataSetUtils.GetStaticArrayPropertyValue<FoxString>(propertyData).ToString();
            }
            else if (propertyData.Name == "attachmentBoneName")
            {
                AttachmentBoneName = DataSetUtils.GetStaticArrayPropertyValue<FoxString>(propertyData).ToString();
            }
            else if (propertyData.Name == "weaponParams")
            {
                var list = DataSetUtils.GetDynamicArrayValues<FoxEntityPtr>(propertyData);
                WeaponParams = new List<TppVehicle2WeaponParameter>(list.Count);

                foreach (var param in list)
                {
                    var entity = initFunctions.GetEntityFromAddress(param.EntityPtr) as TppVehicle2WeaponParameter;
                    WeaponParams.Add(entity);
                    entity.Owner = this;
                }
            }
        }

        public override void OnAssetsImported(Core.AssetPostprocessor.TryGetAssetDelegate tryGetImportedAsset)
        {
            base.OnAssetsImported(tryGetImportedAsset);
            tryGetImportedAsset(AttachmentFilePath, out AttachmentFile);
        }
    }
}