
using System;
using System.Collections.Generic;
using System.Text;

namespace PRODUCT_LOGIC.Automaper
{
    public interface IRepositoryMappingService { 
         TDestination Map<TDestination>(object source);
        

    }
}
