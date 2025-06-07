using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO.Interfaces
{
    public interface IBusinessRuleDao
    {
        IList<BusinessRule> GetAllRules();
        BusinessRule GetRuleById(int id);
        BusinessRule AddRule(BusinessRule rule);
        BusinessRule UpdateRule(BusinessRule rule);
        bool DeleteRule(int id);
    }
}
