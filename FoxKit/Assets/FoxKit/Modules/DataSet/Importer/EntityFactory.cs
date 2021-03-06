﻿namespace FoxKit.Modules.DataSet.Importer
{
    using System;

    using FoxKit.Modules.DataSet.Fox.FoxCore;
    using FoxKit.Modules.DataSet.FoxCore;

    using FoxLib;
    
    using UnityEngine;

    /// <summary>
    /// Helper class to facilitate construction of entities.
    /// </summary>
    public static class EntityFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="createFunctions">
        /// The create functions.
        /// </param>
        /// <returns>
        /// The <see cref="Entity"/>.
        /// </returns>
        public static Entity Create(Core.Entity data, string filename, EntityCreateFunctions createFunctions)
        {
            var type = createFunctions.GetEntityType(data.ClassName);
            if (type == null)
            {
                // TODO: Only once for each type
                //ClassGenerator.GenerateClassFromEntity(data, filename);
                return null;
            }

            var instance = Activator.CreateInstance(type) as Entity;//ScriptableObject.CreateInstance(type) as Entity;
            return instance;
        }

        /// <summary>
        /// The entity create functions.
        /// </summary>
        public class EntityCreateFunctions
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EntityCreateFunctions"/> class.
            /// </summary>
            /// <param name="getEntityType">
            /// Function to get the type of an entity.
            /// </param>
            public EntityCreateFunctions(GetEntityTypeDelegate getEntityType)
            {
                this.GetEntityType = getEntityType;
            }

            /// <summary>
            /// Delegate to get the type of an entity by its class name.
            /// </summary>
            /// <param name="className">
            /// The entity's class name.
            /// </param>
            /// <returns>
            /// The type of the entity.
            /// </returns>
            public delegate Type GetEntityTypeDelegate(string className);

            /// <summary>
            /// Gets the function to get an entity's type.
            /// </summary>
            public GetEntityTypeDelegate GetEntityType { get; }

        }

        /// <summary>
        /// The entity initialize functions.
        /// </summary>
        public class EntityInitializeFunctions
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EntityInitializeFunctions"/> class.
            /// </summary>
            /// <param name="getEntityFromAddress">
            /// Function to get an Entity from its address.
            /// </param>
            /// <param name="makeEntityLink">
            /// Function to make an EntityLink.</param>
            public EntityInitializeFunctions(GetEntityFromAddressDelegate getEntityFromAddress, MakeEntityLinkDelegate makeEntityLink)
            {
                this.GetEntityFromAddress = getEntityFromAddress;
                this.MakeEntityLink = makeEntityLink;
            }

            /// <summary>
            /// Delegate to get an Entity from its address (only useful for imported DataSets).
            /// </summary>
            /// <param name="address">
            /// The address of the Entity in its DataSet file.
            /// </param>
            /// <returns>
            /// The Entity with the given address.
            /// </returns>
            public delegate dynamic GetEntityFromAddressDelegate(ulong address);

            /// <summary>
            /// Gets the function to get an Entity from its address.
            /// </summary>
            public GetEntityFromAddressDelegate GetEntityFromAddress { get; }

            /// <summary>
            /// Delegate to make an EntityLink from raw EntityLink data.
            /// </summary>
            /// <param name="rawEntityLink">
            /// The raw EntityLink.
            /// </param>
            /// <returns>
            /// The converted EntityLink.
            /// </returns>
            public delegate EntityLink MakeEntityLinkDelegate(Core.EntityLink rawEntityLink);

            /// <summary>
            /// Gets the function to get an Entity from its address.
            /// </summary>
            public MakeEntityLinkDelegate MakeEntityLink { get; }
        }
    }
}