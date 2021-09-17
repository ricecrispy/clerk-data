using clerk_data_data_access.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public interface ICommitteeRepository
    {
        Task<IEnumerable<Committee>> SearchComitteesAsync();
        Task CreateComitteeAsync(Committee committee);
        Task UpdateComitteeAsync(string code, Committee committee);
    }
}
