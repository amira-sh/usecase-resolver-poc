using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UsecaseMediator
{
    public interface IUsecaseAsync<U, T> : IUsecase
        where U : IUsecaseResponse
        where T : IUsecaseRequest
    {
        public Task<U> ExecuteAsync(T usecaseRequest);
    }
}
