using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberRepository;
        public MemberService(IRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public List<Member> GetAllMembers()
        {
            return _memberRepository.GetAll(x => x).ToList();
        }
        public Member? GetMemberById(Guid id)
        {
            return _memberRepository.Get(x => x, x => x.Id == id);
        }
        public Member InsertMember(Member member)
        {
            member.Id = Guid.NewGuid();
            return _memberRepository.Insert(member);
        }
        public Member UpdateMember(Member member)
        {
            return _memberRepository.Update(member);
        }
        public Member DeleteMember(Guid id)
        {
            var member = GetMemberById(id);
            if (member == null) throw new Exception("Member not found");
            _memberRepository.Delete(member);
            return member;
        }
    }
}
