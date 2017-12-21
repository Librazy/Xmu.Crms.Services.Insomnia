using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool DeleteSeminarGroupMemberBySeminarGroupId(long seminarGroupId)
        {
            _db.SeminarGroupMember.Where(s => s.SeminarGroup.Id == seminarGroupId).ToList()
                .ForEach(member => _db.SeminarGroupMember.Remove(member));
            return true;
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
            if (isExist != null)
            {
                throw new InvalidOperationException();
            }
            _db.SeminarGroupMember.Add(new SeminarGroupMember
            {
                SeminarGroup = group,
                Student = student
            });
            _db.SaveChanges();
            var seminargroup = _db.SeminarGroupMember.Where(g => g.SeminarGroup.Id == groupId)
                .SingleOrDefault(s => s.Student.Id == userId);
            if (seminargroup != null)
            {
                return seminargroup.Id;
            }
            return -1;
            //throw new NotImplementedException();
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
           
            List<UserInfo> users = new List<UserInfo>();
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
            List<SeminarGroup> groups = new List<SeminarGroup>();
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
            List<SeminarGroup> groups = new List<SeminarGroup>();
            _db.SeminarGroup.Where(s => s.Seminar.Id == seminarId).ToList()
                .ForEach(group => groups.Add(group));
            _db.SaveChanges();
            return groups;
            //throw new NotImplementedException();
        }

        public bool DeleteSeminarGroupBySeminarId(long seminarId)
        {
            if (seminarId < 0)
            {
                throw new ArgumentException();
            }
            _db.SeminarGroup.Where(s => s.Seminar.Id == seminarId)
                .ToList().ForEach(group => _db.SeminarGroup.Remove(group));
            _db.SaveChanges();
            return true;
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
            _db.SeminarGroup.Add(seminarGroup);
            var group = _db.SeminarGroup.SingleOrDefault(s => s.Seminar.Id == seminarId);
            _db.SaveChanges();
            if (group != null)
            {
                return group.Id;
            }
            return -1;
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
            _db.SeminarGroupMember.Add(seminarGroupMember);
            var member = _db.SeminarGroupMember.SingleOrDefault(s => s.SeminarGroup.Id == groupId);
            _db.SaveChanges();
            if (member != null)
            {
                return member.Id;
            }
            return -1;
            //throw new NotImplementedException();
        }

        public bool DeleteSeminarGroupByGroupId(long seminarGroupId)
        {
            if (seminarGroupId < 0)
            {
                throw new ArgumentException();
            }
            _db.SeminarGroup.Where(s => s.Id == seminarGroupId).ToList()
                .ForEach(seminarGroup => _db.SeminarGroup.Remove(seminarGroup));
            _db.SaveChanges();
            return true;
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
            _db.SaveChanges();
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
            _db.SaveChanges();
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
            List<UserInfo> members = new List<UserInfo>();
            _db.SeminarGroupMember.Where(s => s.SeminarGroup.Seminar.Id == seminarId)
                .Select(s => s.Student)
                .ToList().ForEach(member => members.Add(member));
            
            _db.SaveChanges();
            throw new NotImplementedException();
        }

        public SeminarGroup GetSeminarGroupById(long seminarId, long userId)
        {
            if (userId < 0 || seminarId < 0)
            {
                throw new ArgumentException();
            }
            var seminarmember = _db.SeminarGroupMember.Where(s => s.Student.Id == userId)
                .SingleOrDefault(sg => sg.SeminarGroup.Seminar.Id == seminarId);
            _db.SaveChanges();
            return seminarmember.SeminarGroup;
            //throw new NotImplementedException();
        }

        public List<SeminarGroup> ListGroupByTopicId(long topicId)
        {
            if (topicId < 0)
            {
                throw new ArgumentException();
            }
            List<SeminarGroup> groups = new List<SeminarGroup>();
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
            _db.SaveChanges();
            throw new NotImplementedException();
        }

        public bool DeleteTopicByGroupId(long groupId)
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
            return true;
            //throw new NotImplementedException();
        }

        public bool AssignLeaderById(long groupId, long userId)
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
            if (group.Leader != null)
            {
                throw new InvalidOperationException();
            }
            group.Leader = user;
            _db.SaveChanges();
            return true;
            //throw new NotImplementedException();
        }

        public bool ResignLeaderById(long groupId, long userId)
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
            return true;
            //throw new NotImplementedException();
        }
    }
}
