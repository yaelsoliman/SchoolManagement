using SchoolManagement.Application.Interface.Repository;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Service
{
    public class TSCService : BaseService<TSC>, ITSCService
    {
        private readonly ApplicationDbContext _dbContext;

        public TSCService(IRepository<TSC> repo, ApplicationDbContext dbContext) : base(repo)
        {
            _dbContext = dbContext;
        }
    }
}
