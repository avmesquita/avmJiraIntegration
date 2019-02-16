using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avmJiraIntegration.App
{
    public class JiraIssue
    {
        public JiraIssue()
        {

        }

        public JiraIssue(string project, string key, string description, string status, string summary, string reporter, string assignee, 
                         string idPriority, string namePriority, DateTime? duedate, DateTime? created, DateTime? updated, DateTime? resolution)
        {
            this.Project = project;
            this.Key = key;
            this.Description = description;
            this.Status = status;
            this.Reporter = reporter;
            this.Assignee = assignee;
            this.Summary = summary;
            this.Priority = namePriority + "(" + idPriority + ")";
            this.DueDate = duedate;
            this.Created = created;
            this.Updated = updated;
            this.Resolution = resolution;
        }

        public string Project { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Reporter { get; set; }
        public string Assignee { get; set; }
        public string Summary { get; set; }
        public string Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Resolution { get; set; }
    }
}
