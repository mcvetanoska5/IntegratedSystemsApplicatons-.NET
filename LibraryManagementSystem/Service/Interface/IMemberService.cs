using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IMemberService
    {
        List<Member> GetAllMembers();
        Member? GetMemberById(Guid id);
        Member InsertMember(Member member);
        Member UpdateMember(Member member);
        Member DeleteMember(Guid id);
    }
}
