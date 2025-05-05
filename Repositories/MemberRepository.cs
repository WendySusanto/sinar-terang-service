using Microsoft.EntityFrameworkCore;
using Workspace.Models;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task AddAsync(Member Member);
    Task SaveChangesAsync();
}

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MemberRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _dbContext.Members.ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _dbContext.Members.FindAsync(id);
    }

    public async Task AddAsync(Member member)
    {
        await _dbContext.Members.AddAsync(member);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}