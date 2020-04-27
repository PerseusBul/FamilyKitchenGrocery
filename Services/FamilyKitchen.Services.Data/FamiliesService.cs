using FamilyKitchen.Common;
using FamilyKitchen.Data.Common.Repositories;
using FamilyKitchen.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyKitchen.Services.Data
{
    public class FamiliesService : IFamiliesService
    {
        private readonly IDeletableEntityRepository<Family> familiesRepository;
        private readonly IDeletableEntityRepository<FamilyKitchenUser> usersRepository;
        private readonly UserManager<FamilyKitchenUser> userManager;

        public FamiliesService(IDeletableEntityRepository<Family> familiesRepository,
                                IDeletableEntityRepository<FamilyKitchenUser> usersRepository,
                                UserManager<FamilyKitchenUser> userManager)
        {
            this.familiesRepository = familiesRepository;
            this.usersRepository = usersRepository;
            this.userManager = userManager;
        }

        public async Task<bool> CreateFamily(string username, string familyNickname, List<string> familyMembers)
        {
            var userHead = this.usersRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var isHeadExist = this.familiesRepository.All().SelectMany(x => x.FamilyMembers).Any(y => y.UserName == username);
            var isExist = this.familiesRepository.All().Any(x => x.Name == familyNickname);

            if (userHead == null || familyNickname == null || isExist || isHeadExist)
            {
                return false;
            }

            var family = new Family
            {
                Name = familyNickname,
            };

            await this.familiesRepository.AddAsync(family);
            await this.familiesRepository.SaveChangesAsync();

            foreach (var member in familyMembers)
            {
                var userMember = this.usersRepository.All().Where(x => x.UserName == member).FirstOrDefault();
                var isMemberExist = this.familiesRepository.All().SelectMany(x => x.FamilyMembers).Any(y => y.UserName == userMember.UserName);

                if (userMember == null || isMemberExist)
                {
                    return false;
                }

                family.FamilyMembers.ToList().Add(userMember);
            }

            this.familiesRepository.Update(family);
            await this.familiesRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddMember(string familyNickname, string member)
        {
            var fam = this.familiesRepository.All().Where(x => x.Name == "Smurfs");

            return true;
        }

        public async Task<bool> RemoveMember(string familyNickname, string member)
        {
            return true;
        }

        public async Task<bool> DeleteFamily(string familyNickname)
        {

            return true;
        }
    }
}
