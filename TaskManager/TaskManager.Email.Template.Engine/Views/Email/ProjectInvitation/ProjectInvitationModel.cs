namespace TaskManager.Email.Template.Engine.Views.Email.ProjectInvitation
{
    public class ProjectInvitationModel : ParentViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDesc { get; set; }
    }
}