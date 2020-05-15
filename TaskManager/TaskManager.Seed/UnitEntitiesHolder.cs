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
                    UnitId = 1,
                    Name = "CRUD API creating and deleting users",
                    Description = "Create user api, spam list and blocking users",
                    UnitType = UnitType.Task,
                    CreatorId = 2,
                    Key = new Guid("dea2c6f6-3064-40fb-9f75-8e695939e839")
                },
                new Unit()
                {
                    UnitId = 2,
                    Name = "Api for email subscriptions",
                    Description = "Modify database, add email service for client sales",
                    UnitType = UnitType.Task,
                    CreatorId = 2,
                    Key = new Guid("814d9772-ef7c-4eb9-a932-18dc89d4a0b4")
                },
                new Unit()
                {
                    UnitId = 3,
                    Name = "Delete cascade",
                    Description = "Change behaviour from delete cascade to restrict and rework service deleting logic",
                    UnitType = UnitType.Task,
                    CreatorId = 2,
                    Key = new Guid("a7d245d0-3280-4ef5-9acb-6787bc194db7")
                },
                new Unit()
                {
                    UnitId = 4,
                    Name = "Api for donations",
                    Description = "Plug in PayPal",
                    UnitType = UnitType.Task,
                    CreatorId = 2,
                    Key = new Guid("90992949-51c7-4ad1-aa92-086a1c57ba5d")
                },
                new Unit()
                {
                    UnitId = 5,
                    Name = "Testing content",
                    Description = "Config docker compose with dotnet and postgres image and write integration tests for content with",
                    UnitType = UnitType.Task,
                    CreatorId = 2,
                    Key = new Guid("3310e655-5b08-493c-972c-13f668b5c57e")
                },

                #endregion

                #region Projects

                new Unit()
                {
                    UnitId = 20,
                    Name = "TaskManager",
                    Description = "Система отслеживания заданий. Выдача задания менеджером. Статус задания, согласно рабочему процессу. Процент выполнения. Почтовые уведомления клиентам системы. Управление пользователями и их ролями.",
                    UnitType = UnitType.Project,
                    CreatorId = 3,
                    Key = new Guid("bff26a36-6cb5-4cef-a7c4-939f6eaf76ca")
                },

                #endregion

                #region Subtasks

                new Unit()
                {
                    UnitId = 25,
                    Name = "Create postgres image",
                    Description = null,
                    UnitType = UnitType.SubTask,
                    CreatorId = 2,
                    Key = new Guid("32ae9833-13f7-4350-a68e-70e0bfeeca30")
                },
                new Unit()
                {
                    UnitId = 26,
                    Name = "Create dotnet image",
                    Description = null,
                    UnitType = UnitType.SubTask,
                    CreatorId = 2,
                    Key = new Guid("02d0d799-c713-4d50-997a-c4b116192153")
                },
                #endregion

                #region Comments

                new Unit()
                {
                    UnitId = 40,
                    Name = "add doc document with api desc",
                    Description = null,
                    UnitType = UnitType.Comment,
                    CreatorId = 2,
                    Key = new Guid("2da24682-8c31-4a23-b1e4-f979e8f80805")
                },
                new Unit()
                {
                    UnitId = 41,
                    Name = "we use postgres 11",
                    Description = null,
                    UnitType = UnitType.Comment,
                    CreatorId = 2,
                    Key = new Guid("d719805a-5c72-4473-8e6a-16b23120e185")
                },
                new Unit()
                {
                    UnitId = 42,
                    Name = "Ok",
                    Description = null,
                    UnitType = UnitType.Comment,
                    CreatorId = 1,
                    Key = new Guid("2e5bc155-4842-4bf3-94de-36199204d917")
                },
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
                }
            };
        }

        public List<TermInfo> GeTermInfos()
        {
            return new List<TermInfo>()
            {
                // Tasks
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
                // Projects
                new TermInfo()
                {
                    Id = 20,
                    UnitId = 20,
                    StartTs = DateTimeOffset.Now,
                    DueTs =  new DateTimeOffset(2020, 06, 23, 12, 40, 40, new TimeSpan(-2, 0, 0)),
                    Status = Status.InProgress
                },
                // SubTasks
                new TermInfo()
                {
                    Id = 25,
                    UnitId = 25,
                    StartTs = DateTimeOffset.Now,
                    DueTs = null,
                    Status = Status.InProgress
                },
                new TermInfo()
                {
                    Id = 26,
                    UnitId = 26,
                    StartTs = DateTimeOffset.Now,
                    DueTs = null,
                    Status = Status.Closed
                },

                // Comments
                new TermInfo()
                {
                    Id = 40,
                    UnitId = 40,
                    StartTs = DateTimeOffset.Now,
                    DueTs = null,
                    Status = Status.None
                },
                new TermInfo()
                {
                    Id = 41,
                    UnitId = 41,
                    StartTs = DateTimeOffset.Now,
                    DueTs = null,
                    Status = Status.None
                },
                new TermInfo()
                {
                    Id = 42,
                    UnitId = 42,
                    StartTs = DateTimeOffset.Now,
                    DueTs = null,
                    Status = Status.None
                },
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
                //SubTasks
                new RelationShip()
                {
                    UnitId = 25,
                    ParentUnitId = 5
                },
                new RelationShip()
                {
                    UnitId = 26,
                    ParentUnitId = 5
                },
                //Comments
                new RelationShip()
                {
                    UnitId = 40,
                    ParentUnitId = 4
                },
                new RelationShip()
                {
                    UnitId = 41,
                    ParentUnitId = 5
                },
                new RelationShip()
                {
                    UnitId = 42,
                    ParentUnitId = 5
                },
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
                    UnitId = 20,
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
                    Id = 10,
                    UserId = 2,
                    ProjectId = 1
                },
                //ProjectManager
                new ProjectMember()
                {
                    Id = 15,
                    UserId = 3,
                    ProjectId = 1
                }
            };
        }

        #endregion

    }
}
