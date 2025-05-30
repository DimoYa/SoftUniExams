using CyberSecurityDS.Models;
using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityDS.Core.Contracts
{
    public class Controller : IController
    {
        private ISystemManager systemManager;

        public Controller()
        {
            this.systemManager = new SystemManager();
        }

        public string AddCyberAttack(string attackType, string attackName, int severityLevel, string extraParam)
        {
            ICyberAttack cyberAttack;
            var isAttackerExist = systemManager.CyberAttacks.Exists(attackName);

            if (isAttackerExist)
            {
                return string.Format(OutputMessages.EntryAlreadyExists, attackName);
            }

            switch (attackType)
            {
                case "MalwareAttack":
                    cyberAttack = new MalwareAttack(attackName, severityLevel, extraParam);
                    break;

                case "PhishingAttack":
                    cyberAttack = new PhishingAttack(attackName, severityLevel, extraParam);
                    break;
                default:
                    return string.Format(OutputMessages.TypeInvalid, attackType);
            }

            this.systemManager.CyberAttacks.AddNew(cyberAttack);

            return string.Format(OutputMessages.EntryAddedSuccessfully, attackType, attackName);
        }

        public string AddDefensiveSoftware(string softwareType, string softwareName, int effectiveness)
        {
            IDefensiveSoftware defensiveSoftware;

            switch (softwareType)
            {
                case "Firewall":
                    defensiveSoftware = new Firewall(softwareName, effectiveness);
                    break;

                case "Antivirus":
                    defensiveSoftware = new Antivirus(softwareName, effectiveness);
                    break;
                default:
                    return string.Format(OutputMessages.TypeInvalid, softwareType);
            }

            var isDefensiveExist = systemManager.DefensiveSoftwares.Exists(softwareName);

            if (isDefensiveExist)
            {
                return string.Format(OutputMessages.EntryAlreadyExists, softwareName);
            }

            this.systemManager.DefensiveSoftwares.AddNew(defensiveSoftware);

            return string.Format(OutputMessages.EntryAddedSuccessfully, softwareType, softwareName);
        }

        public string AssignDefense(string cyberAttackName, string defensiveSoftwareName)
        {
            var isAttackerExist = systemManager.CyberAttacks.Exists(cyberAttackName);
            var isSoftwareExist = systemManager.DefensiveSoftwares.Exists(defensiveSoftwareName);

            if (!isAttackerExist)
            {
                return string.Format(OutputMessages.EntryNotFound, cyberAttackName);
            }

            if (!isSoftwareExist)
            {
                return string.Format(OutputMessages.EntryNotFound, defensiveSoftwareName);
            }

            var attacker = systemManager.CyberAttacks.GetByName(cyberAttackName);
            var software = systemManager.DefensiveSoftwares.GetByName(defensiveSoftwareName);

            if (this.systemManager.DefensiveSoftwares.Models
                    .SelectMany(x => x.AssignedAttacks)
                    .Contains(cyberAttackName))
            {
                var name = this.systemManager.DefensiveSoftwares.Models
                                              .FirstOrDefault(ds => ds.AssignedAttacks.Contains(cyberAttackName))?.Name;

                return string.Format(OutputMessages.AttackAlreadyAssigned, cyberAttackName, name);
            }

            software.AssignAttack(cyberAttackName);

            return string.Format(OutputMessages.AttackAssignedSuccessfully, cyberAttackName, defensiveSoftwareName);
        }

        public string GenerateReport()
        {
            var result = new StringBuilder();

            result.AppendLine("Security:");

            this.systemManager.DefensiveSoftwares.Models.OrderBy(x=> x.Name).ToList().ForEach(x=> {
                result.AppendLine(x.ToString());
            });

            result.AppendLine("Threads:");
            result.AppendLine("-Mitigated:");


            this.systemManager.CyberAttacks.Models.Where(x => x.Status).OrderBy(x=> x.AttackName).ToList().ForEach(x =>
            {
                result.AppendLine(x.ToString());
            });

            result.AppendLine("-Pending:");


            this.systemManager.CyberAttacks.Models.Where(x=> !x.Status).OrderBy(x => x.AttackName).ToList().ForEach(x =>
            {
                result.AppendLine(x.ToString());
            });

            return result.ToString().TrimEnd();
        }

        public string MitigateAttack(string cyberAttackName)
        {
            var isAttackerExist = systemManager.CyberAttacks.Exists(cyberAttackName);

            if (!isAttackerExist)
            {
                return string.Format(OutputMessages.EntryNotFound, cyberAttackName);
            }

            var attacker = systemManager.CyberAttacks.GetByName(cyberAttackName);

            if (attacker.Status)
            {
                return string.Format(OutputMessages.AttackAlreadyMitigated, cyberAttackName);
            }

            var defensiveSoftware = this.systemManager.DefensiveSoftwares.Models
                .FirstOrDefault(ds => ds.AssignedAttacks.Contains(cyberAttackName));

            if (defensiveSoftware == null)
            {
                return string.Format(OutputMessages.AttackNotAssignedYet, cyberAttackName);
            }

            var softwareType = defensiveSoftware.GetType().Name;
            var attackType = attacker.GetType().Name;

            bool isCompatible =
                (softwareType == "Firewall" && attackType == "MalwareAttack") ||
                (softwareType == "Antivirus" && attackType == "PhishingAttack");

            if (!isCompatible)
            {
                return string.Format(OutputMessages.CannotMitigateDueToCompatibility,
                    defensiveSoftware.GetType().Name, attacker.GetType().Name);
            }

            if (defensiveSoftware.Effectiveness >= attacker.SeverityLevel)
            {
                attacker.MarkAsMitigated();
                return string.Format(OutputMessages.AttackMitigatedSuccessfully, cyberAttackName);
            }

            return string.Format(OutputMessages.SoftwareNotEffectiveEnough, cyberAttackName, defensiveSoftware.Name);
        }
    }
}
