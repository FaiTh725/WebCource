using Authorize.Application.Contacts.User;
using MediatR;

namespace Authorize.Application.Queries.User.GetById
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public long Id { get; set; }
    }
}
