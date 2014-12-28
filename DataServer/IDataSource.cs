using System.Collections.Generic;
using System.Threading.Tasks;
using MightyFX.Users;

namespace MightyFX.Data
{
    public interface IDataSource
    {
        string Name
        {
            get;
        }

        string Description
        {
            get;
        }

        Task<IEnumerable<ITag>> ListTags();

        Task<ITag> FindTag(string name);

        Task<AccessRoles> CheckAccess(IUser user);

        Task QueryField(DataField[] fields);
    }
}