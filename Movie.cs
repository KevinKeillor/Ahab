using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AhabRestService
{
    class MovieInfo : MovieSumary
    {
        public
        String m_Director;
    }

    [DataContract(Name = "Movie", Namespace = "AhabRestService")]
    public class MovieSumary : IExtensibleDataObject
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

        // Movie Id
        [DataMember(Name = "I")]
        public
        String m_Id;
        // Movie name
        [DataMember(Name = "N")]
        public
        String m_Title;
        // release year 
        [DataMember(Name = "Y")]
        public
        String m_Year;
        // genre
        [DataMember(Name = "G")]
        public
        String m_Genre;
        // runtime
        [DataMember(Name = "R")]
        public
        String m_Runtime;

        // Tag a shortish descripiton
        [DataMember(Name = "T")]
        public
        String m_Tagline;

        // List of cast
        [DataMember(Name = "C")]
        public
        List<Artist> m_CastList;

        // Director
        [DataMember(Name = "D")]
        public
        String m_Director;

        // Writer
        [DataMember(Name = "W")]
        public
        String m_Writer;

        // Writer
        [DataMember(Name = "P")]
        public
        String m_Plot;

        // HD
        [DataMember(Name = "H")]
        public
        String m_HD;


    }
}
