using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace clerk_data_data_access.Factory
{
    public interface IDbConnectionFactory
    {
        int CommandTimeout { get; }
        IDbConnection GetDataBaseConnection();
    }
}
