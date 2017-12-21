using System;
using System.Collections.Generic;
using System.Linq;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.Insomnia
{
    public class GroupService : ISeminarGroupService
    {
        private readonly CrmsContext _db;

        public GroupService(CrmsContext db) => _db = db;

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

            var group = _db.SeminarGroup.SingleOrDefault(s => s.Id == groupId);
            if (group == null)
            {
                throw new GroupNotFoundException();
            }

            var student = _db.UserInfo.SingleOrDefault(u => u.Id == userId);

            if (student == null)
            {
                throw new UserNotFoundException();
            }

            var isExist = _db.SeminarGroupMember.Where(sg => sg.SeminarGroup.Id == groupId && sg.Student.Id == userId);
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
            //throw new NotImplementedException();
        }

        public List<UserInfo> ListSeminarGroupMemberByGroupId(long groupId) => throw new NotImplementedException();

        public List<SeminarGroup> ListSeminarGroupIdByStudentId(long userId) => throw new NotImplementedException();

        public long GetSeminarGroupLeaderByGroupId(long groupId) => throw new NotImplementedException();

        public List<SeminarGroup> ListSeminarGroupBySeminarId(long seminarId) => throw new NotImplementedException();

        public void DeleteSeminarGroupBySeminarId(long seminarId)
        {
            throw new NotImplementedException();
        }

        public long InsertSeminarGroupBySeminarId(long seminarId, SeminarGroup seminarGroup) =>
            throw new NotImplementedException();

        public long InsertSeminarGroupMemberByGroupId(long groupId, SeminarGroupMember seminarGroupMember) =>
            throw new NotImplementedException();

        public void DeleteSeminarGroupByGroupId(long seminarGroupId)
        {
            throw new NotImplementedException();
        }

        public SeminarGroup GetSeminarGroupByGroupId(long groupId) => throw new NotImplementedException();

        public long GetSeminarGroupLeaderById(long userId, long seminarId) => throw new NotImplementedException();

        public bool AutomaticallyGrouping(long seminarId, long classId) => throw new NotImplementedException();

        public SeminarGroup GetSeminarGroupById(long seminarId, long userId) => throw new NotImplementedException();

        public List<SeminarGroup> ListGroupByTopicId(long topicId) => throw new NotImplementedException();

        public string InsertTopicByGroupId(long groupId, long topicId) => throw new NotImplementedException();

        public void AssignLeaderById(long groupId, long userId)
        {
            throw new NotImplementedException();
        }

        public void ResignLeaderById(long groupId, long userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTopicByGroupId(long groupId) => throw new NotImplementedException();
    }
}