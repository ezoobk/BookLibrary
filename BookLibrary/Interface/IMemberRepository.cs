using BookLibrary.Models;

namespace BookLibrary.Interface
{
    public interface IMemberRepository
    {
        ICollection<Member> GetMembers();
        Member GetMember(int memberId);
        bool MemberExists(int memberId);
        bool CreateMember(Member member);
        bool Save();
    }
}
