/********************************************************************************
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

namespace DotNet.Common.Serialization
{
    /// <summary>
    /// Enum representing the serialization Mode.
    /// </summary>
    public enum SerializationMode
    {
		XmlSerializer,
		DataContractSerializer,
		NetDataContractSerializer,
		BinaryFormatter
    }
}