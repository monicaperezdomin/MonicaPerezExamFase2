using Newtonsoft.Json;
using PVueling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class ServiceApiRateTest
    {
      

        [Fact]



         public async Task GetAsyncTestAsync()
        {
            List<Rate> ListRate = null;
            string uri = "http://quiet-stone-2094.herokuapp.com/rates.json";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(uri))
            using (HttpContent content = response.Content)
            {

                string result = await content.ReadAsStringAsync();

                ListRate = JsonConvert.DeserializeObject<List<Rate>>(""); 

            }
            Assert.Null(ListRate);

        }
    }



}

    

