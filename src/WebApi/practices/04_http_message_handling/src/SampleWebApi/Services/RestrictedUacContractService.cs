using System.Linq;
using Newtonsoft.Json.Linq;
using SampleWebApi.DomainModel;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class RestrictedUacContractService
    {
        readonly RoleRepository roleRepository;

        public RestrictedUacContractService(RoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public bool RemoveRestrictedInfo(long userId, JObject restricetedResource)
        {
            if (roleRepository.Get(userId) == Role.Admin) { return false; }

            var Links = restricetedResource["links"] as JArray;
            if (Links == null) { return false; }
            JToken[] restrictedLinks = Links.Where(link =>
            {
                var jObjectLink = link as JObject;
                if (jObjectLink == null)
                {
                    return false;
                }
                JToken jTokenLink = jObjectLink?["restricted"];
                if (jTokenLink?.Type != JTokenType.Boolean)
                {
                    return false;
                }

                return jTokenLink.Value<bool>();
            }).ToArray();

            if (restrictedLinks.Length == 0)
            {
                return false;
            }

            foreach (JToken restrictedLink in restrictedLinks)
            {
                Links.Remove(restrictedLink);
            }
            return true;
        }
    }
}