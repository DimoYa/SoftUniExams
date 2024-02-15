using System.Runtime.CompilerServices;
using System.Text;

namespace MailClient
{
    public class MailBox
    {
        public MailBox(int capacity)
        {
            this.Capacity = capacity;
            this.Inbox = new List<Mail>();
            this.Archive = new List<Mail>();
        }

        public int Capacity { get; set; }

        public List<Mail> Inbox { get; set; }

        public List<Mail> Archive { get; set; }

        public void IncomingMail(Mail mail)
        {
            if (this.Inbox.Count < this.Capacity)
            {
                this.Inbox.Add(mail);
            }
        }

        public bool DeleteMail(string sender)
        {
            var mailToDelete = this.Inbox.FirstOrDefault(x => x.Sender == sender);

            if (mailToDelete is null)
            {
                return false;
            }

            this.Inbox.Remove(mailToDelete);
            return true;
        }

        public int ArchiveInboxMessages()
        {
            var count = Inbox.Count;

            this.Archive.AddRange(this.Inbox);

            this.Inbox.Clear();

            return count;
        }

        public string GetLongestMessage()
        {
           var mail = this.Inbox.OrderByDescending(s => s.Body.Length).First();
            return mail.ToString();
        }

        public string InboxView()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Inbox:");
            Inbox.ToList().ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().TrimEnd();
        }
    }
}
