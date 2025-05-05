using System.Collections.Generic;
using System.Threading.Tasks;
using Workspace.Models;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task<Member?> GetMemberByIdAsync(int id);
    Task AddMemberAsync(Member Member);
}

public class MemberService : IMemberService
{
    private readonly IMemberRepository _MemberRepository;

    public MemberService(IMemberRepository MemberRepository)
    {
        _MemberRepository = MemberRepository;
    }

    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        return await _MemberRepository.GetAllAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(int id)
    {
        return await _MemberRepository.GetByIdAsync(id);
    }

    public async Task AddMemberAsync(Member Member)
    {
        await _MemberRepository.AddAsync(Member);
        await _MemberRepository.SaveChangesAsync();
    }
}