using System;
using System.Collections.Generic;
using System.Linq;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;
namespace Xmu.Crms.Services.Insomnia
{
    public class GroupService: ISeminarGroupService
    {
        private readonly CrmsContext _db;

        public GroupService(CrmsContext db)
        {
            _db = db;
        }

        public void DeleteSeminarGroupMemberBySeminarGroupId(long seminarGroupId)
        {
            _db.RemoveRange(_db.SeminarGroupMember.Where(s => s.SeminarGroup.Id == seminarGroupId));
            _db.SaveChanges();
        }

        public long InsertSeminarGroupMemberById(long userId, long groupId)
        {
            if (userId < 0 && groupId < 0)
            {
                throw new ArgumentException();
            }
            var group=_db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
            var student = _db.UserInfo.SingleOrDefault(u => u.Id == userId);
            
            if (student == null)
            {
                throw new UserNotFoundException();
            }
            var isExist = _db.SeminarGroupMember.Where(sg => sg.SeminarGroup.Id == groupId)
                .Where(s => s.Student.Id == userId);
            if (isExist.Any())
            {
                throw new InvalidOperationException();
            }
            var seminargroup = _db.SeminarGroupMember.Add(new SeminarGroupMember
            {
                SeminarGroup = group,
                Student = student
            });
            _db.SaveChanges();
           
            return seminargroup.Entity.Id;
        }

        public List<UserInfo> ListSeminarGroupMemberByGroupId(long groupId)
        {
            if (groupId < 0)
            {
                throw new ArgumentException();
            }
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
           
            var users = new List<UserInfo>();
            _db.SeminarGroupMember.Where(s => s.SeminarGroup.Id == groupId)
                .Select(s => s.Student)
                .ToList()
                .ForEach(user => users.Add(user));
            _db.SaveChanges();
            return users;
            //throw new NotImplementedException();
        }

        public List<SeminarGroup> ListSeminarGroupIdByStudentId(long userId)
        {
            if (userId < 0)
            {
                throw new ArgumentException();
            }
            var user = _db.SeminarGroup.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new GroupNotFoundException();
            }
            var groups = new List<SeminarGroup>();
            _db.SeminarGroupMember.Where(s => s.Student.Id == userId)
                .Select(s => s.SeminarGroup)
                .ToList()
                .ForEach(group => groups.Add(group));
            _db.SaveChanges();
            return groups;
            //throw new NotImplementedException();
        }

        public long GetSeminarGroupLeaderByGroupId(long groupId)
        {
            if (groupId < 0)
            {
                throw new ArgumentException();
            }
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
            _db.SaveChanges();
            return group.Leader.Id;
            //throw new NotImplementedException();
        }

        public List<SeminarGroup> ListSeminarGroupBySeminarId(long seminarId)
        {
            if (seminarId < 0)
            {
                throw new ArgumentException();
            }
            var seminar = _db.Seminar.SingleOrDefault(s => s.Id == seminarId);
            if (seminar == null)
            {
                throw new SeminarNotFoundException();
            }
            var groups = new List<SeminarGroup>();
            _db.SeminarGroup.Where(s => s.Seminar.Id == seminarId).ToList()
                .ForEach(group => groups.Add(group));
            _db.SaveChanges();
            return groups;
            //throw new NotImplementedException();
        }

        public void DeleteSeminarGroupBySeminarId(long seminarId)
        {
            if (seminarId < 0)
            {
                throw new ArgumentException();
            }
            _db.SeminarGroup.Where(s => s.Seminar.Id == seminarId)
                .ToList().ForEach(group => _db.SeminarGroup.Remove(group));
            _db.SaveChanges();
            //throw new NotImplementedException();
        }
        
        public long InsertSeminarGroupBySeminarId(long seminarId, SeminarGroup seminarGroup)
        {
            if (seminarId < 0)
            {
                throw new ArgumentException();
            }
            var seminarinfo = _db.Seminar.SingleOrDefault(s => s.Id == seminarId);
            seminarGroup.Seminar = seminarinfo;
            var group = _db.SeminarGroup.Add(seminarGroup);
            _db.SaveChanges();
            return group.Entity.Id;
            //throw new NotImplementedException();
        }

        public long InsertSeminarGroupMemberByGroupId(long groupId, SeminarGroupMember seminarGroupMember)
        {
            if (groupId < 0)
            {
                throw new ArgumentException();
            }
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            seminarGroupMember.SeminarGroup = group;
            var member = _db.SeminarGroupMember.Add(seminarGroupMember);
            _db.SaveChanges();
            return member.Entity.Id;
            //throw new NotImplementedException();
        }

        public void DeleteSeminarGroupByGroupId(long seminarGroupId)
        {
            if (seminarGroupId < 0)
            {
                throw new ArgumentException();
            }
            _db.SeminarGroup.RemoveRange(_db.SeminarGroup.Where(s => s.Id == seminarGroupId));
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public SeminarGroup GetSeminarGroupByGroupId(long groupId)
        {
            if (groupId < 0)
            {
                throw new ArgumentException();
            }
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
            return group;
            //throw new NotImplementedException();
        }

        public long GetSeminarGroupLeaderById(long userId, long seminarId)
        {
            if (userId < 0 || seminarId < 0)
            {
                throw new ArgumentException();
            }
            var seminarmember = _db.SeminarGroupMember.Where(s => s.Student.Id == userId)
                .SingleOrDefault(sg => sg.SeminarGroup.Seminar.Id == seminarId);
            if (seminarmember != null)
            {
                return seminarmember.SeminarGroup.Leader.Id;
            }
            return -1;
            //throw new NotImplementedException();
        }

        public bool AutomaticallyGrouping(long seminarId, long classId)
        {
            if (seminarId < 0 || classId < 0)
            {
                throw new ArgumentException();
            }
            var seminar = _db.Seminar.SingleOrDefault(s => s.Id == seminarId);
            if (seminar == null)
            {
                throw new SeminarNotFoundException();
            }
            var classes = _db.ClassInfo.SingleOrDefault(c => c.Id == classId);
            if (classes == null)
            {
                throw new ClassNotFoundException();
            }
            var members = new List<UserInfo>();
            _db.CourseSelection.Where(c => c.ClassInfo.Id == classId)
                .Select(c => c.Student)
                .ToList().ForEach(member => members.Add(member));
            var count = 0;
            UserInfo[] memArrays = { };
            members.ForEach(member => memArrays[count++] = member);
            int looptime = memArrays.Length / 2;
            var tick = DateTime.Now.Ticks;
            var ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            while (looptime >= 0)
            {
                var ran1 = ran.Next(0, memArrays.Length);
                var ran2 = ran.Next(0, memArrays.Length);
                var temp = memArrays[ran1];
                memArrays[ran1] = memArrays[ran2];
                memArrays[ran2] = temp;
                looptime--;
            }
            int countgroup = memArrays.Length / 5 + 1;
            for (int i = 0; i < countgroup; i++)
            {
                _db.SeminarGroup.Add(new SeminarGroup
                {
                    Seminar = seminar,
                    ClassInfo = classes
                });
                var group = _db.SeminarGroup.Where(s => s.Seminar.Id == seminarId)
                    .SingleOrDefault(s => s.ClassInfo.Id == classId);
                for (int j = 0; j < 5; j++)
                {
                    var usertemp = memArrays[i * 5 + j];
                    _db.SeminarGroupMember.Add(new SeminarGroupMember
                    {
                        SeminarGroup = group,
                        Student = usertemp
                    });
                }
            }
            _db.SaveChanges();
            return true;
            //throw new NotImplementedException();
        }

        public SeminarGroup GetSeminarGroupById(long seminarId, long userId)
        {
            if (userId < 0 || seminarId < 0)
            {
                throw new ArgumentException();
            }
            var seminarmember = _db.SeminarGroupMember.Where(s => s.Student.Id == userId)
                .SingleOrDefault(sg => sg.SeminarGroup.Seminar.Id == seminarId);
            if (seminarmember == null)
            {
                throw new InvalidOperationException();
            }
            return seminarmember.SeminarGroup;
            //throw new NotImplementedException();
        }

        public List<SeminarGroup> ListGroupByTopicId(long topicId)
        {
            if (topicId < 0)
            {
                throw new ArgumentException();
            }
            var groups = new List<SeminarGroup>();
            _db.SeminarGroupTopic.Where(s => s.Topic.Id == topicId)
                .Select(sg => sg.SeminarGroup)
                .ToList().ForEach(sg => groups.Add(sg));
            _db.SaveChanges();
            return groups;
            //throw new NotImplementedException();
        }

        public string InsertTopicByGroupId(long groupId, long topicId)
        {
            if (groupId < 0 || topicId < 0)
            {
                throw new ArgumentException();
            }
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
            var topic = _db.Topic.SingleOrDefault(t => t.Id == topicId);
            _db.SeminarGroupTopic.Add(new SeminarGroupTopic
            {
                Topic = topic,
                SeminarGroup = group
            });
            _db.SaveChanges();
            return "";
            //throw new NotImplementedException();
        }

        public void DeleteTopicByGroupId(long groupId)
        {
            if (groupId < 0)
            {
                throw new ArgumentException();
            }
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
            _db.SeminarGroupTopic.Where(s => s.SeminarGroup.Id == groupId).ToList()
                .ForEach(sgt => _db.SeminarGroupTopic.Remove(sgt));
            _db.SaveChanges();
        }

        public void AssignLeaderById(long groupId, long userId)
        {
            if (groupId < 0 || userId < 0)
            {
                throw new ArgumentException();
            }
            var user = _db.UserInfo.SingleOrDefault(u => u.Id == userId);
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
            if (group.Leader != null)
            {
                throw new InvalidOperationException();
            }
            group.Leader = user ?? throw new UserNotFoundException();
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public void ResignLeaderById(long groupId, long userId)
        {
            if (groupId < 0 || userId < 0)
            {
                throw new ArgumentException();
            }
            var user = _db.UserInfo.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }
            if (group.Leader != user)
            {
                throw new InvalidOperationException();
            }
            group.Leader = null;
            _db.SaveChanges();
        }
    }
}
