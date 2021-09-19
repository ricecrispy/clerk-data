using clerk_data_data_access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public interface ICommitteeRepository
    {
        /// <summary>
        /// Search and retrieve all Committee objects.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Committee>> SearchCommitteesAsync();
        /// <summary>
        /// Create a Committee object in the database.
        /// </summary>
        /// <param name="committee"></param>
        /// <returns></returns>
        Task CreateCommitteeAsync(Committee committee);
        /// <summary>
        /// Retrieve a Committee object by committee code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<Committee> GetCommitteeByCommitteeCodeAsync(string code);
        /// <summary>
        /// Update a Committee object with matching committee code.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="committee"></param>
        /// <returns></returns>
        Task UpdateCommitteeAsync(string code, Committee committee);
        /// <summary>
        /// Get and add all SubCommittee objects that belongs to a Committee.
        /// </summary>
        /// <param name="committee"></param>
        /// <returns></returns>
        Task<Committee> GetSubCommitteesAsync(Committee committee);
    }
}
