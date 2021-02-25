using Business.Abstract;
using Entities.Concrete;
using MernisServiceReference;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Adapters
{
    public class MernisCheckService : IPersonCheckService
    {
        public bool CheckIfRealPerson(User user)
        {
            /*
            KPSPublicSoapClient client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);

            return client.TCKimlikNoDogrulaAsync(
                new TCKimlikNoDogrulaRequest
                (new TCKimlikNoDogrulaRequestBody(user.UserId, user.FirstName, user.LastName, user.dateOfBirt)))
                .Result.Body.TCKimlikNoDogrulaResult;*/
            return true; // user a dan dogum tariihi ve tc almak lazim maalesef bitirmiyorum. mantikli gelmedi
        }
    }
}
