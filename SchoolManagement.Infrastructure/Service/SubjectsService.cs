﻿using SchoolManagement.Application.Interface.Repository;
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
    public class SubjectsService : BaseService<Subject>, ISubjectsService
    {
        private readonly ApplicationDbContext _dbContext;

        public SubjectsService(IRepository<Subject> repo, ApplicationDbContext dbContext) : base(repo)
        {
            _dbContext = dbContext;
        }
    }
}
