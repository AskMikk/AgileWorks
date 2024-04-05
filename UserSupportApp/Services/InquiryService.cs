using System.Collections.Concurrent;
using UserSupportApp.Interfaces;
using UserSupportApp.Models;

namespace UserSupportApp.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly ConcurrentDictionary<int, Inquiry> _inquiries = new();
        private int _nextId = 1;

        public List<Inquiry> GetAllActiveInquiries() =>
            _inquiries.Values.Where(i => !i.IsResolved).OrderByDescending(i => i.ResolutionDeadline).ToList();

        public void AddInquiry(Inquiry inquiry)
        {
            inquiry.Id = _nextId++;
            _inquiries.TryAdd(inquiry.Id, inquiry);
        }

        public void MarkInquiryAsResolved(int id)
        {
            if (_inquiries.TryGetValue(id, out var inquiry))
            {
                inquiry.IsResolved = true;
            }
        }
    }
}