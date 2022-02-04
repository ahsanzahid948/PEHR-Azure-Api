using Application.DTOs.Common.Test;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ITestService
    {
        Task<Response<TestResponse>> HandShake(string Name);
    }
}
