using NordNetApiPoC.NordNetAPI.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordNetApiPoC.NordNetAPI.AbstractRequests.MarketInfo
{
    class MarketInfoRequest : AbstractRequestClass
    {
        private IEnumerable<Indices> _AllAvailableIndices;
        public IEnumerable<Indices> AllAvailableIndices
        {
            get
            {
                if (_AllAvailableIndices != null)
                    return _AllAvailableIndices;
             
                return (_AllAvailableIndices= MakeRequest<IEnumerable<Indices>>(HttpMethods.POST,"indicies"));
            }
          }

    }
}
