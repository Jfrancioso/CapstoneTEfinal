using Microsoft.AspNetCore.Mvc;
using Capstone.DAO.Interfaces;
using Capstone.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class BusinessRuleController : ControllerBase
    {
        private readonly IBusinessRuleDao businessRuleDao;

        public BusinessRuleController(IBusinessRuleDao businessRuleDao)
        {
            this.businessRuleDao = businessRuleDao;
        }

        [HttpGet]
        public ActionResult<IList<BusinessRule>> GetAll()
        {
            return Ok(businessRuleDao.GetAllRules());
        }

        [HttpGet("{id}")]
        public ActionResult<BusinessRule> Get(int id)
        {
            BusinessRule rule = businessRuleDao.GetRuleById(id);
            if (rule == null)
            {
                return NotFound();
            }
            return Ok(rule);
        }

        [HttpPost]
        public ActionResult<BusinessRule> Create(BusinessRule rule)
        {
            BusinessRule added = businessRuleDao.AddRule(rule);
            return Created($"businessrule/{added.Id}", added);
        }

        [HttpPut("{id}")]
        public ActionResult<BusinessRule> Update(int id, BusinessRule rule)
        {
            if (businessRuleDao.GetRuleById(id) == null)
            {
                return NotFound();
            }
            rule.Id = id;
            return Ok(businessRuleDao.UpdateRule(rule));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!businessRuleDao.DeleteRule(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
