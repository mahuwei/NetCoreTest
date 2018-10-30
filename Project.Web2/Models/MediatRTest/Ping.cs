using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Project.Web2.Models.MediatRTest {
    public class Ping : IRequest<Pong> {
        public string Message { get; set; }
    }

    public class Pinged : INotification { }

    public class Pong : IRequest {
        public string Message { get; set; }
    }

    //public class PingHandler : IRequestHandler<Ping, Pong> {
    //    //private readonly TextWriter _writer;

    //    public PingHandler() {
    //        //_writer = writer;
    //    }

    //    public Task<Pong> Handle(Ping request,
    //        CancellationToken cancellationToken) {
    //        //await _writer.WriteLineAsync($"--- Handled Ping: {request.Message}");
    //        return Task.FromResult(new Pong {Message = request.Message + " Pong"});
    //    }
    //}

    public class PingHandler2 : IRequestHandler<Ping, Pong> {
        //private readonly TextWriter _writer;

        public PingHandler2() {
            //_writer = writer;
        }

        public Task<Pong> Handle(Ping request,
            CancellationToken cancellationToken) {
            //await _writer.WriteLineAsync($"--- Handled Ping: {request.Message}");
            return Task.FromResult(new Pong {Message = request.Message + " Pong2"});
        }
    }

    //public class Ponged : INotification { }

    //public class PingedHandler : INotificationHandler<Pinged> {
    //    private readonly TextWriter _writer;

    //    public PingedHandler(TextWriter writer) {
    //        _writer = writer;
    //    }

    //    public Task Handle(Pinged notification,
    //        CancellationToken cancellationToken) {
    //        return _writer.WriteLineAsync("Got pinged async.");
    //    }
    //}

    //public class PongedHandler : INotificationHandler<Ponged> {
    //    private readonly TextWriter _writer;

    //    public PongedHandler(TextWriter writer) {
    //        _writer = writer;
    //    }

    //    public Task Handle(Ponged notification,
    //        CancellationToken cancellationToken) {
    //        return _writer.WriteLineAsync("Got ponged async.");
    //    }
    //}

    //public class
    //    ConstrainedPingedHandler<TNotification> : INotificationHandler<TNotification>
    //    where TNotification : Pinged {
    //    private readonly TextWriter _writer;

    //    public ConstrainedPingedHandler(TextWriter writer) {
    //        _writer = writer;
    //    }

    //    public Task Handle(TNotification notification,
    //        CancellationToken cancellationToken) {
    //        return _writer.WriteLineAsync("Got pinged constrained async.");
    //    }
    //}

    //public class PingedAlsoHandler : INotificationHandler<Pinged> {
    //    private readonly TextWriter _writer;

    //    public PingedAlsoHandler(TextWriter writer) {
    //        _writer = writer;
    //    }

    //    public Task Handle(Pinged notification,
    //        CancellationToken cancellationToken) {
    //        return _writer.WriteLineAsync("Got pinged also async.");
    //    }
    //}
}