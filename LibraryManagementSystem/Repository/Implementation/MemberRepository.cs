using LibraryManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Member> entities;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<Member>();
        }

        public Member GetById(Guid id)
        {
            return entities.First(e => e.Id == id);
        }
    }
}
