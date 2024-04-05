using UserSupportApp.Models;

namespace UserSupportApp.Interfaces
{
    public interface IInquiryService
    {
        List<Inquiry> GetAllActiveInquiries();
        void AddInquiry(Inquiry inquiry);
        void MarkInquiryAsResolved(int id);
    }
}