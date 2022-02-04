using Application.DTOs.Common.Test;
using Application.Interfaces.Services;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
   public class TestService: ITestService
    {
        public virtual async Task<Response<TestResponse>> HandShake(string Name)
        {
            TestResponse response = new TestResponse()
            {
                Name = Name,
                Message = "I am awake"
            };
            return new Response<TestResponse>(response);
        }
    }
}
