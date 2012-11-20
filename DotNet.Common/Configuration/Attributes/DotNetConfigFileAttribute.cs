/*******************************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author: Allen Wang(Allen.G.Wang@newegg.com) 
 * Create Date: 12/23/2008 
 * Description:
 *          
 * Revision History:
 *      Date         Author               Description
 * 
*********************************************************************************/
using System;
using DotNet.Common.Resources;


namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 指定DotNet配置文件的属性信息。
    /// </summary>
    /// <remarks>此属性只允许配置在类型为 <b>ConfigurationSection</b> 和 <b>ConfigurationSectionGroup</b> 的Class上。</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DotNetConfigFileAttribute : ConfigFileAttribute
    {
        private Type sectionGroupType;
        private string sectionGroupName;
        private string sectionName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        public DotNetConfigFileAttribute(string sectionName)
            : this(FrameworkConfig.ConfigPath, sectionName)
        {
        }

        /// <summary>
        /// 用正在属性化的DotNet配置文件位置特性初始化 <see cref="ConfigFileSectionAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="sectionGroupType">DotNet配置结点组类型。</param>
        /// <param name="sectionName">DotNet配置结点名。。</param>
        public DotNetConfigFileAttribute(string sectionName, Type sectionGroupType)
			: this(FrameworkConfig.ConfigPath, sectionName, sectionGroupType)
        {
        }

        /// <summary>
        /// 用正在属性化的DotNet配置文件位置特性初始化 <see cref="ConfigFileSectionAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="sectionGroupType">DotNet配置结点组类型。</param>
        /// <param name="sectionName">DotNet配置结点名。。</param>
        public DotNetConfigFileAttribute(string fileName, string sectionName, Type sectionGroupType)
            : base(fileName)
        {
            this.sectionName = sectionName;
            this.FileName = fileName;

            DotNetConfigFileAttribute attribute =
                Attribute.GetCustomAttribute(sectionGroupType, typeof(DotNetConfigFileAttribute)) as DotNetConfigFileAttribute;
            if (attribute == null)
            {
                ConfigThrowHelper.ThrowConfigException(
                    R.ConfigError_NoConfigAttribute, sectionGroupType.FullName, typeof(DotNetConfigFileAttribute).FullName);
            }

            this.sectionGroupType = sectionGroupType;
        }

        /// <summary>
        /// 用正在属性化的DotNet配置文件位置特性初始化 <see cref="ConfigFileSectionAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="supportMultiLanguages">支持多语言。</param>
        /// <param name="fileName">相对路径文件名。</param>
        /// <param name="sectionGroupName">DotNet配置结点组名。</param>
        /// <param name="sectionName">DotNet配置结点名。</param>
        public DotNetConfigFileAttribute(string fileName, string sectionName)
            : this(fileName, sectionName, string.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sectionName"></param>
        /// <param name="sectionGroupName"></param>
        public DotNetConfigFileAttribute(string fileName, string sectionName, string sectionGroupName)
            : base(fileName)
        {
            this.sectionName = sectionName;
            this.sectionGroupName = sectionGroupName;
        }      

        /// <summary>
        /// 配置结点组。
        /// </summary>
        public Type SectionGroupType
        {
            get { return sectionGroupType; }
        }

        /// <summary>
        /// 获取配置结点组名称。
        /// </summary>
        /// <value>配置结点组名称。</value>
        public string SectionGroupName
        {
            get { return sectionGroupName; }
        }

        /// <summary>
        /// 获取配置结点名称。
        /// </summary>
        /// <value>配置结点名称。</value>
        public string SectionName
        {
            get { return sectionName; }
        }
    }
}