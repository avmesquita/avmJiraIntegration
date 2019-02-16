using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace avmJiraIntegration.App
{
	class Program
	{
		static void Main(string[] args)
		{
			var allIssues = Integration("username", "password", "project-name").GetAwaiter();
			
		}

		static async Task<List<JiraIssue>> Integration(string username, string password, string project)
		{
			var services = new ServiceLocator();
			var credentials = new Atlassian.Jira.JiraCredentials(username, password);
			var cache = new JiraCache();
			
			var jira = new Jira(services, credentials, cache);			
			jira.Issues.MaxIssuesPerRequest = 5000;

			/*
             Status
             0 - Open
             1 - In Progress
             2 - Reopened
             3 - Resolved
             4 - Closed
             5 - Building
             6 - Build Broken
             7 - To Do
             8 - Done
             9 - Backlog
             10 - Selected for Development
             */

			IEnumerable<IssueStatus> listaStatus = await jira.Statuses.GetStatusesAsync();


			List<JiraIssue> list = new List<JiraIssue>();

			var issues = jira.Issues;
			var projetos = jira.Projects;
			Console.WriteLine("===============================================");
			Console.WriteLine("Looking for Project Issues...");
		    Console.WriteLine("===============================================");
			foreach (var item in issues.Queryable)
			{
				if (item.Project == project && 
					(item.Status.Id == "0" || item.Status.Id == "1" || item.Status.Id == "2" || item.Status.Id == "7" || item.Status.Id == "9")
					)
				{
					DateTime? resolucao = item.ResolutionDate;
					resolucao = resolucao == null ? DateTime.Now.AddDays(1) : resolucao;

					DateTime? criacao = item.Created == null ? DateTime.Now.AddDays(1) : item.Created;
					DateTime? duedate = item.DueDate == null ? DateTime.Now.AddDays(1) : item.DueDate;
					DateTime? updated = item.Updated == null ? DateTime.Now.AddDays(1) : item.Updated;

					Console.WriteLine("===============================================");
					Console.WriteLine(item.Project);
					Console.WriteLine(item.Summary);
					Console.WriteLine(item.Priority);
					Console.WriteLine(item.Assignee);
					Console.WriteLine(item.Reporter);
					Console.WriteLine(item.Status.Description);

					list.Add(getIssue(item));
				}
			}
			Console.WriteLine("===============================================");

			return list;
		}

		private static JiraIssue getIssue(Issue item)
		{

			JiraIssue jiraissue = new JiraIssue();

			jiraissue.Project = (item.Project == null ? "" : item.Project.ToString());
			jiraissue.Key = (item.Key == null ? "" : item.Key.ToString());
			jiraissue.Summary = (item.Summary == null ? "" : item.Summary.ToString());
			jiraissue.Description = (item.Description == null ? "" : item.Description.ToString());
			jiraissue.Status = (item.Status == null ? "" : item.Status.Name.ToString() + "(" + item.Status.Id.ToString() + ")");
			jiraissue.Priority = (item.Priority == null ? null : item.Priority.Name + "(" + item.Priority.Id + ")");
			jiraissue.Reporter = (item.Reporter == null ? "" : item.Reporter.ToString());
			jiraissue.Assignee = (item.Assignee == null ? "" : item.Assignee.ToString());
			jiraissue.DueDate = item.DueDate;
			jiraissue.Created = item.Created;
			jiraissue.Updated = item.Updated;
			jiraissue.Resolution = item.ResolutionDate;

			return jiraissue;
		}

	}
}
