using Microsoft.AspNetCore.Mvc;
using Moq;
using UserSupportApp.Controllers;
using UserSupportApp.Interfaces;
using UserSupportApp.Models;
using Xunit;

namespace UserSupportApp.Tests.Controllers;

public class InquiriesControllerTests
{
    private readonly Mock<IInquiryService> _serviceMock;
    private readonly InquiriesController _controller;

    public InquiriesControllerTests()
    {
        _serviceMock = new Mock<IInquiryService>();
        _controller = new InquiriesController(_serviceMock.Object);
    }

    [Fact]
    public void Index_ShouldReturnViewWithInquiries()
    {
        _serviceMock.Setup(s => s.GetAllActiveInquiries()).Returns(GetTestInquiries());
        var result = _controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Inquiry>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public void Create_POST_ShouldRedirectToIndexOnSuccess()
    {
        var inquiry = new Inquiry { Description = "New Inquiry", ResolutionDeadline = DateTime.Now.AddDays(1) };
        var result = _controller.Create(inquiry);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public void Resolve_ShouldRedirectToIndex()
    {
        var result = _controller.Resolve(1);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        _serviceMock.Verify(s => s.MarkInquiryAsResolved(1), Times.Once);
    }

    private List<Inquiry> GetTestInquiries()
    {
        return new List<Inquiry>
        {
            new Inquiry { Id = 1, Description = "Test Inquiry 1", ResolutionDeadline = DateTime.Now.AddDays(1) },
            new Inquiry { Id = 2, Description = "Test Inquiry 2", ResolutionDeadline = DateTime.Now.AddDays(2) }
        };
    }
}