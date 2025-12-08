using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Cache
{
    public interface ICacheService
    {
        Task SetAsync(string Key, object value, TimeSpan duration);
        Task<string?> GetAsync(string Key);

    }
}
