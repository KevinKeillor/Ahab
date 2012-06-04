using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AhabRestService
{
    class MovieInfo : MovieSumary
    {
        String m_Director;
    }
    class MovieSumary
    {
        String m_Title;
        String m_Year;
        String m_Genre;
        String m_Runtime;
        String m_Tagline;
    }
}
