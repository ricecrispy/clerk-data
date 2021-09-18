using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.Factory
{
    public class PostgreSqlConnectionFactoryOptions
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public int CommandTimeout { get; set; } = 30;
    }
}
