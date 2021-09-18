using clerk_data_data_access.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public interface ICommitteeRepository
    {
        Task<IEnumerable<Committee>> SearchCommitteesAsync();
        Task CreateCommitteeAsync(Committee committee);
        Task<Committee> GetCommitteeByCommitteeCodeAsync(string code);
        Task UpdateCommitteeAsync(string code, Committee committee);
    }
}
