using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Synotech.SMP.Core.Models.Parent;

namespace Synotech.SMP.Core.Interfaces.Parent
{
    public interface IParentService
    {
        List<ParentModel> GetParents();
        Task<bool> SaveParent(ParentModel parent);
        Task<bool> DeleteParent(int studentId);
        Task<ParentModel> GetParentById(int studentId);
        ParentModel SearchParent(string strSearch);

    }
}
