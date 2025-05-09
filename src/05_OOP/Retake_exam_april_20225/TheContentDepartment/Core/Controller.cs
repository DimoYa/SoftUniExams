using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Core
{
    public class Controller : IController
    {
        private MemberRepository members;
        private ResourceRepository resources;

        public Controller() 
        {
            members = new MemberRepository();
            resources = new ResourceRepository();
        }

        public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
        {
            var resource = resources.TakeOne(resourceName);

            if (!resource.IsTested)
            {
                return string.Format(OutputMessages.ResourceNotTested, resourceName);
            }

            var teamLead = members.Models.FirstOrDefault(x => x.GetType().Name == "TeamLead");

            if (isApprovedByTeamLead)
            {
                resource.Approve();
                teamLead!.FinishTask(resource.Name);

                return string.Format(OutputMessages.ResourceApproved, teamLead.Name, resourceName);
            }
            else
            {
                resource.Test();

                return string.Format(OutputMessages.ResourceReturned, teamLead!.Name, resourceName);
            }
        }

        public string CreateResource(string resourceType, string resourceName, string path)
        {
            IResource newResource = resources.TakeOne(resourceName);

            var matchedResource = members.Models.FirstOrDefault(x => x.Path == path);

            if (matchedResource is null) 
            {
                return string.Format(OutputMessages.NoContentMemberAvailable, resourceName);
            }

            if (matchedResource.InProgress.Contains(resourceName))
            {
                return string.Format(OutputMessages.ResourceExists, resourceName);
            }

            var creatorName = matchedResource.Name;

            switch (resourceType)
            {
                case "Exam":
                    newResource = new Exam(resourceName, creatorName);
                    break;

                case "Workshop":
                    newResource = new Workshop(resourceName, creatorName);
                    break;

                case "Presentation":
                    newResource = new Presentation(resourceName, creatorName);
                    break;

                default:
                    return string.Format(OutputMessages.ResourceTypeInvalid, resourceType);
            }
            matchedResource.WorkOnTask(resourceName);
            resources.Add(newResource);

            return string.Format(OutputMessages.ResourceCreatedSuccessfully, creatorName, resourceType, resourceName);
        }

        public string DepartmentReport()
        {
            var report = new StringBuilder();

            report.AppendLine("Finished Tasks:");
            foreach (var resource in resources.Models.Where(x => x.IsApproved))
            {
                report.AppendLine($"--{resource}");
            }

            report.AppendLine("Team Report:");

            var teamLead = members.Models.FirstOrDefault(x => x.Path == "Master");
            report.AppendLine($"--{teamLead!.Name} (TeamLead) - Currently working on {teamLead.InProgress.Count} tasks.");

            foreach (var member in members.Models.Where(x => x.Path != "Master"))
            {
                report.AppendLine($"{member.Name} - {member.Path} path. Currently working on {member.InProgress.Count} tasks.");
            }

            return report.ToString().TrimEnd();
        }

        public string JoinTeam(string memberType, string memberName, string path)
        {
            if (memberType != "TeamLead" && memberType != "ContentMember")
            {
                return string.Format(OutputMessages.MemberTypeInvalid, memberType);
            }

            bool positionOccupied = members.Models.Any(x => x.Path == path);
            if (positionOccupied)
            {
                return "Position is occupied.";
            }

            var existingMember = members.TakeOne(memberName);
            if (existingMember is not null)
            {
                return string.Format(OutputMessages.MemberExists, memberName);
            }

            ITeamMember newMember;
            switch (memberType)
            {
                case "TeamLead":
                    newMember = new TeamLead(memberName, path);
                    break;
                case "ContentMember":
                    newMember = new ContentMember(memberName, path);
                    break;
                default:
                    return string.Format(OutputMessages.MemberTypeInvalid, memberType);
            }

            members.Add(newMember);
            return string.Format(OutputMessages.MemberJoinedSuccessfully, memberName);
        }


        public string LogTesting(string memberName)
        {
            var member = members.TakeOne(memberName);

            if (member is null) 
            {
                return string.Format(OutputMessages.WrongMemberName);
            }

            var resource = resources.Models
                .Where(x => !x.IsTested && x.Creator == member.Name)
                .OrderBy(x => x.Priority)
                .FirstOrDefault();

            if (resource is null)
            {
                return string.Format(OutputMessages.NoResourcesForMember, memberName);
            }

            var teamLead = members.Models.FirstOrDefault(x => x.GetType().Name == "TeamLead");


            member.FinishTask(resource.Name);
            teamLead!.WorkOnTask(resource.Name);

            resource.Test();

            return string.Format(OutputMessages.ResourceTested, resource.Name);
        }
    }
}
