﻿using System.Collections.Generic;

using PrismMetroSample.Infrastructure.Interceptor.HandlerAttributes;
using PrismMetroSample.Infrastructure.Models;

namespace PrismMetroSample.Infrastructure.Services
{
    [LogHandler]
    public interface IUserService
    {
        List<User> GetAllUsers();
    }
}
