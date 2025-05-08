using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UsecaseMediator
{
    public interface IUsecaseMediator
    {
        public Task<TResponse> ExecuteAsync<TResponse>(IUsecaseRequest usecaseRequest)
            where TResponse : IUsecaseResponse;
    }
}
