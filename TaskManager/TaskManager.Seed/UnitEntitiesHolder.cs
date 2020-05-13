using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;

namespace TaskManager.Seed
{
    public class UnitEntitiesHolder
    {
        #region Units

        public List<Unit> GetUnits()
        {
            return new List<Unit>()
            {
                #region Tasks

                new Unit()
                {
                    Id = 1,
                    Name = "CRUD API creating and deleting users",
                    Description = "Create user api, spam list and blocking users",
                    UnitType = UnitType.Task,
                    Key = new Guid("dea2c6f6-3064-40fb-9f75-8e695939e839")
                },
                new Unit()
                {
                    Id = 2,
                    Name = "Api for email subscriptions",
                    Description = "Modify database, add email service for client sales",
                    UnitType = UnitType.Task,
                    Key = new Guid("814d9772-ef7c-4eb9-a932-18dc89d4a0b4")
                },
                new Unit()
                {
                    Id = 3,
                    Name = "Delete cascade",
                    Description = "Change behaviour from delete cascade to restrict and rework service deleting logic",
                    UnitType = UnitType.Task,
                    Key = new Guid("a7d245d0-3280-4ef5-9acb-6787bc194db7")
                },
                new Unit()
                {
                    Id = 4,
                    Name = "Api for donations",
                    Description = "Plug in PayPal",
                    UnitType = UnitType.Task,
                    Key = new Guid("90992949-51c7-4ad1-aa92-086a1c57ba5d")
                },
                new Unit()
                {
                    Id = 5,
                    Name = "Testing content",
                    Description = "Config docker compose with dotnet and postgres image and write integration tests for content with",
                    UnitType = UnitType.Task,
                    Key = new Guid("3310e655-5b08-493c-972c-13f668b5c57e")
                },

                #endregion

                #region Projects

                new Unit()
                {
                    Id = 6,
                    Name = "TaskManager",
                    Description = "Система отслеживания заданий. Выдача задания менеджером. Статус задания, согласно рабочему процессу. Процент выполнения. Почтовые уведомления клиентам системы. Управление пользователями и их ролями.",
                    UnitType = UnitType.Project,
                    Key = new Guid("bff26a36-6cb5-4cef-a7c4-939f6eaf76ca")
                }

                #endregion
            };
        }

        public List<Tag> GeTags()
        {
            return new List<Tag>()
            {
                new Tag()
                {
                    Id = 1,
                    TextValue = "InProgress"
                },
                new Tag()
                {
                    Id = 2,
                    TextValue = "Self-test"
                },
                new Tag()
                {
                    Id = 3,
                    TextValue = "Adminka"
                },
                new Tag()
                {
                    Id = 4,
                    TextValue = "Done"
                },
                new Tag()
                {
                    Id = 5,
                    TextValue = "Blocked"
                },
                new Tag()
                {
                    Id = 6,
                    TextValue = "CodeReview"
                },
                new Tag()
                {
                    Id = 7,
                    TextValue = "Frontend"
                },
                new Tag()
                {
                    Id = 8,
                    TextValue = "Backend"
                },
                new Tag()
                {
                    Id = 9,
                    TextValue = "Backlog"
                },
                new Tag()
                {
                    Id = 10,
                    TextValue = "Testing"
                },
            };
        }

        #endregion

        #region Tasks

        public List<Task> GeTasks()
        {
            return new List<Task>()
            {
                new Task()
                {
                    Id = 1,
                    ProjectId = 1,
                    UnitId = 1
                },
                new Task()
                {
                    Id = 2,
                    ProjectId = 1,
                    AssignedId = 1,
                    UnitId = 2
                },
                new Task()
                {
                    Id = 3,
                    ProjectId = 1,
                    AssignedId = 1,
                    UnitId = 3
                },
                new Task()
                {
                    Id = 4,
                    ProjectId = 1,
                    AssignedId = 2,
                    UnitId = 4
                },
                new Task()
                {
                    Id = 5,
                    ProjectId = 1,
                    AssignedId = 2,
                    UnitId = 5
                },
                new Task()
                {
                    Id = 6,
                    ProjectId = 1,
                    AssignedId = 2,
                    UnitId = 5
                },
            };
        }

        public List<TermInfo> GeTermInfos()
        {
            return new List<TermInfo>()
            {
                new TermInfo()
                {
                    Id = 1,
                    UnitId = 1,
                    StartTs = DateTimeOffset.Now,
                    DueTs = new DateTimeOffset(2020, 05, 25, 12, 40, 40, new TimeSpan(-2, 0, 0)),
                    Status = Status.Open
                },
                new TermInfo()
                {
                    Id = 2,
                    UnitId = 2,
                    StartTs = DateTimeOffset.Now,
                    DueTs = new DateTimeOffset(2020, 05, 30, 12, 40, 40, new TimeSpan(-2, 0, 0)),
                    Status = Status.InProgress
                },
                new TermInfo()
                {
                    Id = 3,
                    UnitId = 3,
                    StartTs = DateTimeOffset.Now,
                    DueTs = new DateTimeOffset(2020, 05, 27, 12, 40, 40, new TimeSpan(-2, 0, 0)),
                    Status = Status.InProgress
                },
                new TermInfo()
                {
                    Id = 4,
                    UnitId = 4,
                    StartTs = DateTimeOffset.Now,
                    DueTs = new DateTimeOffset(2020, 05, 26, 12, 40, 40, new TimeSpan(-2, 0, 0)),
                    Status = Status.InProgress
                },
                new TermInfo()
                {
                    Id = 5,
                    UnitId = 5,
                    StartTs = DateTimeOffset.Now,
                    DueTs = new DateTimeOffset(2020, 05, 23, 12, 40, 40, new TimeSpan(-2, 0, 0)),
                    Status = Status.Closed
                },
                new TermInfo()
                {
                    Id = 6,
                    UnitId = 6,
                    StartTs = DateTimeOffset.Now,
                    DueTs = null,
                    Status = Status.InProgress
                }
            };
        }
        public List<TagOnTask> GeTagOnTasks()
        {
            return new List<TagOnTask>()
            {
                //Backend, blocked, backlog
                new TagOnTask()
                {
                    TaskId = 1,
                    TagId = 5
                },
                new TagOnTask()
                {
                    TaskId = 1,
                    TagId = 8
                },
                new TagOnTask()
                {
                    TaskId = 1,
                    TagId = 9
                },

                //Backend, inProgress
                new TagOnTask()
                {
                    TaskId = 2,
                    TagId = 1
                },
                new TagOnTask()
                {
                    TaskId = 2,
                    TagId = 8
                },

                //Backend, codeReview
                new TagOnTask()
                {
                    TaskId = 3,
                    TagId = 6
                },
                new TagOnTask()
                {
                    TaskId = 3,
                    TagId = 8
                },

                //Backend, done
                new TagOnTask()
                {
                    TaskId = 4,
                    TagId = 4
                },
                new TagOnTask()
                {
                    TaskId = 4,
                    TagId = 8
                },

                //Testing, inProgress
                new TagOnTask()
                {
                    TaskId = 5,
                    TagId = 1
                },
                new TagOnTask()
                {
                    TaskId = 5,
                    TagId = 10
                },
            };
        }

        public List<RelationShip> GetRelationShips()
        {
            return new List<RelationShip>()
            {
                
            };
        }

        #endregion

        #region Projects
        public List<Project> GetProjects()
        {
            return new List<Project>()
            {
                new Project()
                {
                    Id = 1,
                    ProjectManagerId = 3,
                    UnitId = 6,
                    Members = 3
                }
            };
        }
        public List<ProjectMember> GetProjectMembers()
        {
            return new List<ProjectMember>()
            {
                //Developer
                new ProjectMember()
                {
                    Id = 1,
                    UserId = 1,
                    ProjectId = 1
                },
                //TeamLead
                new ProjectMember()
                {
                    Id = 2,
                    UserId = 2,
                    ProjectId = 1
                },
                //ProjectManager
                new ProjectMember()
                {
                    Id = 3,
                    UserId = 3,
                    ProjectId = 1
                }
            };
        }

        #endregion

    }
}
