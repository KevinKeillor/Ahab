using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AhabRestService
{
    [DataContract(Name = "Movie", Namespace = "AhabRestService")]
    public class Artist : IExtensibleDataObject
    {
        // To implement the IExtensibleDataObject interface, you must also
        // implement the ExtensionData property.
        private ExtensionDataObject extensionDataObjectValue;
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return extensionDataObjectValue;
            }
            set
            {
                extensionDataObjectValue = value;
            }
        }

        public Artist() {}

        public Artist(int Id, String Name) {
            m_Id = Id.ToString();
            m_Name = Name;
        }
        // Artist Id
        [DataMember(Name = "I")]
        public
        String m_Id;
        // Movie name
        [DataMember(Name = "N")]
        public
        String m_Name;
    }

}
