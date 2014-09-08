using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Data.Models;

namespace Logic.LeadLogic
{
    public class LeadLogic
    {

        private readonly LeadContext _context;

        public LeadLogic()
        {
            _context = new LeadContext();
        }

        public LeadLogic(LeadContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Lead> GetLeads(String userName)
        {
            return _context.Leads.Where(l => l.LeadUser == userName);
        }

        public void AddLead(Lead lead)
        {
            _context.Leads.Add(lead);
            _context.SaveChanges();
        }

        public Lead GetLeadById(int leadId)
        {
            return _context.Leads.FirstOrDefault(l => l.LeadId == leadId);
        }

        public void UpdateLead(Lead lead)
        {
            //_context.Leads.AddOrUpdate(l => l.LeadId, lead);
            
            _context.Entry(lead).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteLead(Lead lead)
        {
            _context.Entry(lead).State = EntityState.Deleted;
            
            _context.SaveChanges();
        }
    }

}
