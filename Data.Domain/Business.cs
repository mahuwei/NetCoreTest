using MediatR;

namespace Project.Domain {
    public class Business : Entity, IRequest<Response> {
        public string No { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}