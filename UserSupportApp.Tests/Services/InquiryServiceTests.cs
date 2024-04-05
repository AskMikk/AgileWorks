namespace UserSupportApp.Tests.Services;

using System;
using Xunit;
using UserSupportApp.Models;
using UserSupportApp.Services;

public class InquiryServiceTests
{
    private readonly InquiryService _service = new();

    [Fact]
    public void AddInquiry_ShouldAddInquiry()
    {
        var inquiry = new Inquiry { Description = "Test Description", ResolutionDeadline = DateTime.Now.AddDays(1) };
        _service.AddInquiry(inquiry);

        var result = _service.GetAllActiveInquiries();

        Assert.Contains(result, i => i.Description == "Test Description");
    }

    [Fact]
    public void GetAllActiveInquiries_ShouldReturnOnlyActive()
    {
        var resolvedInquiry = new Inquiry { Description = "Resolved", ResolutionDeadline = DateTime.Now.AddDays(1), IsResolved = true };
        _service.AddInquiry(resolvedInquiry);

        var activeInquiry = new Inquiry { Description = "Active", ResolutionDeadline = DateTime.Now.AddDays(1) };
        _service.AddInquiry(activeInquiry);

        var result = _service.GetAllActiveInquiries();

        Assert.DoesNotContain(result, i => i.IsResolved);
        Assert.Contains(result, i => i.Description == "Active");
    }


    [Fact]
    public void MarkInquiryAsResolved_ShouldMarkInquiryResolved()
    {
        var inquiry = new Inquiry { Description = "To Resolve", ResolutionDeadline = DateTime.Now.AddDays(1) };
        _service.AddInquiry(inquiry);
        _service.MarkInquiryAsResolved(inquiry.Id);

        var result = _service.GetAllActiveInquiries();

        Assert.DoesNotContain(result, i => i.Description == "To Resolve");
    }
}
