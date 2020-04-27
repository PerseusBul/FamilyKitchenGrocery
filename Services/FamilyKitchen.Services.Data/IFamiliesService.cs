namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFamiliesService
    {
        Task<bool> CreateFamily(string username, string famulyNickname, List<string> familyMeber);

        Task<bool> AddMember(string familyNickname, string member);
    }
}
