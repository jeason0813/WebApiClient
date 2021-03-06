﻿using System;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Contexts;
using WebApiClient.Interfaces;
using WebApiClient.Parameterables;
using Xunit;

namespace WebApiClientTest.Parameterables
{
    public class BasicAuthTest
    {
        [Fact]
        public async Task Test()
        {
            var context = new ApiActionContext
            {
                RequestMessage = new HttpApiRequestMessage(),
                ApiActionDescriptor = ApiDescriptorCache.GetApiActionDescriptor(typeof(IMyApi).GetMethod("PostAsync"))
            };

            var parameter = context.ApiActionDescriptor.Parameters[0];
            IApiParameterable basicAuth = new BasicAuth("laojiu", "123456");
            await basicAuth.BeforeRequestAsync(context, parameter);

            var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("laojiu:123456"));
            Assert.True(context.RequestMessage.Headers.Authorization.Parameter == auth);
        }
    }
}
