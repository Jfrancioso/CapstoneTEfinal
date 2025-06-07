using Capstone.DAO.Interfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Capstone.DAO
{
    public class BusinessRuleSqlDao : IBusinessRuleDao
    {
        private readonly string connectionString;

        public BusinessRuleSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<BusinessRule> GetAllRules()
        {
            IList<BusinessRule> rules = new List<BusinessRule>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT rule_id, rule_name, description, is_active FROM business_rules", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rules.Add(MapRowToBusinessRule(reader));
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error getting business rules");
            }
            return rules;
        }

        public BusinessRule GetRuleById(int id)
        {
            BusinessRule rule = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT rule_id, rule_name, description, is_active FROM business_rules WHERE rule_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        rule = MapRowToBusinessRule(reader);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error getting business rule by id");
            }
            return rule;
        }

        public BusinessRule AddRule(BusinessRule rule)
        {
            int newId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO business_rules (rule_name, description, is_active) OUTPUT INSERTED.rule_id VALUES (@name, @desc, @active)", conn);
                    cmd.Parameters.AddWithValue("@name", rule.Name);
                    cmd.Parameters.AddWithValue("@desc", rule.Description);
                    cmd.Parameters.AddWithValue("@active", rule.IsActive);
                    newId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error adding business rule");
            }
            return GetRuleById(newId);
        }

        public BusinessRule UpdateRule(BusinessRule rule)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE business_rules SET rule_name=@name, description=@desc, is_active=@active WHERE rule_id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", rule.Id);
                    cmd.Parameters.AddWithValue("@name", rule.Name);
                    cmd.Parameters.AddWithValue("@desc", rule.Description);
                    cmd.Parameters.AddWithValue("@active", rule.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error updating business rule");
            }
            return GetRuleById(rule.Id);
        }

        public bool DeleteRule(int id)
        {
            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM business_rules WHERE rule_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error deleting business rule");
            }
            return rows > 0;
        }

        private BusinessRule MapRowToBusinessRule(SqlDataReader reader)
        {
            return new BusinessRule
            {
                Id = Convert.ToInt32(reader["rule_id"]),
                Name = Convert.ToString(reader["rule_name"]),
                Description = Convert.ToString(reader["description"]),
                IsActive = Convert.ToBoolean(reader["is_active"])
            };
        }
    }
}
