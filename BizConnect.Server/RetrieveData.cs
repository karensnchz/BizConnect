using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;


namespace BizConnect.Server
{
    //TODO
        //Methodos que vas abstract llamar desde else telefono para agarrar los datos
        // regresar datos en caso de que los haga

    [ServiceContract]
    public interface RetrieveData
    {
        [OperationContract]
        String[] GetCustomer();
        [OperationContract]
        String[] GetInvoice();
    }
}
