using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }

        public List<UserInfo> ListSeminarGroupMemberByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public List<SeminarGroup> ListSeminarGroupIdByStudentId(long userId)
        {
            throw new NotImplementedException();
        }

        public long GetSeminarGroupLeaderByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public List<SeminarGroup> ListSeminarGroupBySeminarId(long seminarId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSeminarGroupBySeminarId(long seminarId)
        {
            throw new NotImplementedException();
        }

        public long InsertSeminarGroupBySeminarId(long seminarId, SeminarGroup seminarGroup)
        {
            throw new NotImplementedException();
        }

        public long InsertSeminarGroupMemberByGroupId(long groupId, SeminarGroupMember seminarGroupMember)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSeminarGroupByGroupId(long seminarGroupId)
        {
            throw new NotImplementedException();
        }

        public SeminarGroup GetSeminarGroupByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public long GetSeminarGroupLeaderById(long userId, long seminarId)
        {
            throw new NotImplementedException();
        }

        public bool AutomaticallyGrouping(long seminarId, long classId)
        {
            throw new NotImplementedException();
        }

        public SeminarGroup GetSeminarGroupById(long seminarId, long userId)
        {
            throw new NotImplementedException();
        }

        public List<SeminarGroup> ListGroupByTopicId(long topicId)
        {
            throw new NotImplementedException();
        }

        public string InsertTopicByGroupId(long groupId, long topicId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTopicByGroupId(long groupId)
        {
            throw new NotImplementedException();
        }

        public bool AssignLeaderById(long groupId, long userId)
        {
            throw new NotImplementedException();
        }

        public bool ResignLeaderById(long groupId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
