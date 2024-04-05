using Microsoft.AspNetCore.Mvc;
using UserSupportApp.Interfaces;
using UserSupportApp.Models;

namespace UserSupportApp.Controllers
{
    public class InquiriesController : Controller
    {
        private readonly IInquiryService _inquiryService;

        public InquiriesController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

        public IActionResult Index()
        {
            var inquiries = _inquiryService.GetAllActiveInquiries();
            return View(inquiries);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inquiry inquiry)
        {
            if (!ModelState.IsValid)
            {
                return View(inquiry);
            }
            _inquiryService.AddInquiry(inquiry);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Resolve(int id)
        {
            _inquiryService.MarkInquiryAsResolved(id);
            return RedirectToAction(nameof(Index));
        }
    }
}